using UnityEngine;

public class AIController : Controller
{
    [Tooltip("View angle for the AI view cone")]
    public float viewAngle = 35f;
    [Tooltip("Max distance that the AI can see the player at")]
    public float viewDistance;
    [Tooltip("Current action of the AI's state machine")]
    public enum AIState { Idle, Chase, Patrol, Seek, ChooseTarget };
    StateMachine<AIState> stateMachine = new StateMachine<AIState>(AIState.ChooseTarget); //Make a state machine with the initial state of finding the player

    public GameObject target;

    private Vector3 lastTargetPosition;
    private Vector3 randomPos = Vector3.zero;

    /*SENSE THRESHOLDS*/
    [SerializeField] float awarenessDistance = 15f;

    void Update()
    {
        if (pawn != null)
        {
            ProcessAI();
        }
    }

    private void ProcessAI()
    {
        switch (stateMachine.GetState())
        {
            case AIState.Idle:
                pawn.MoveHorizontal(0f);
                // Check for transitions
                if (CanSee() && isWithinRange_Transform(target.transform, awarenessDistance))
                {
                    stateMachine.ChangeState(AIState.Chase);
                }
                break;
            case AIState.Chase:
                //rotate to face player
                pawn.RotateTowards(target.transform.position);
                //move towards player
                pawn.MoveHorizontal(1f);

                break;
            /*
        case AIState.Patrol:
            if (pawn.transform.forward == randomPos - pawn.transform.position) {
                randomPos = transform.position + new Vector3(Random.Range(0f, 10f), pawn.transform.position.y, Random.Range(0f, 10f));
            }
            else {
                pawn.RotateTowards(randomPos);
            }
            break;
        case AIState.Seek:
            //going to last position player was seen
            if(!CanSee()) {
                //if reached last seen position
                if((pawn.transform.position - lastTargetPosition).magnitude < 0.1f) {
                    ChangeState((AIState)AIState.Patrol);
                }
                //rotate to face movement
                pawn.RotateTowards(lastTargetPosition);
                //move towards player
                pawn.MoveHorizontal(1f);
                moving = true;
            }
            //chasing player if seen
            else {
                ChangeState(AIState.Chase);
            }
            break;
            */
            case AIState.ChooseTarget:
                //this is really only used at the start, to prevent errors
                TargetPlayer();
                break;
            default:
                stateMachine.ChangeState(AIState.ChooseTarget);
                break;
        }
    }

    public void TargetPlayer()
    {
        // If the GameManager exists
        if (GameManager.Instance != null)
        {
            // And the array of players exists
            if (GameManager.Instance.player != null)
            {

                //Then target the gameObject of the pawn of the player controller
                target = GameManager.Instance.player.pawn.gameObject;
                stateMachine.ChangeState(AIState.Chase);
            }
        }
    }
    private bool CanSee()
    {
        //vector from this to the target
        Vector3 targetVector = target.transform.position - pawn.transform.position;
        //angle between the facing direction and the vector to the target
        float angleToTarget = Vector3.Angle(targetVector, pawn.transform.forward);

        //raycast from AI to player
        //do raycast and assign to bool
        RaycastHit hit;
        bool hasSightLine = false;
        Physics.Raycast(pawn.transform.position, targetVector, out hit, viewDistance);
        //set sight line to true if raycast hits player
        if (hit.collider != null)
        {
            if (hit.collider.gameObject == target)
            {
                hasSightLine = true;
            }
        }
        //debug
        Vector3 rayRight = Quaternion.AngleAxis(viewAngle, transform.up) * pawn.transform.forward * viewDistance;
        Vector3 rayLeft = Quaternion.AngleAxis(-viewAngle, transform.up) * pawn.transform.forward * viewDistance;
        Debug.DrawRay(pawn.transform.position, targetVector);
        Debug.DrawRay(pawn.transform.position, rayRight);
        Debug.DrawRay(pawn.transform.position, rayLeft);

        //if angle is less than view angle & has line of sight
        if (angleToTarget < viewAngle && hasSightLine)
        {
            return true;
        }
        return false;
    }

    //Returns if the distance of a target is within a specific range threshold to this game object
    bool isWithinRange_Transform(Transform targetTransform, float rangeLimit)
    {
        //Get the vector in between the target and this gameobject
        Vector3 distanceVector = targetTransform.position - transform.position;

        return distanceVector.magnitude < rangeLimit; // returns wether the magnitude of the distance vector is within the threshold
    }
    //Generic Function that requests to find if theres a specific component within a threshold distance
    bool isWithinRange_Component<T>(float rangeLimit) where T : Component
    {
        //Get the objects with the components (unsorted) [had to look up what the findobjectsbytype and sortmode was]
        T[] compObjs = FindObjectsByType<T>(FindObjectsSortMode.None);

        //Go throught the list of collected components
        foreach (T component in compObjs)
        {
            //Check if the object with component is within the range limit
            if (isWithinRange_Transform(component.transform, rangeLimit))
            {
                return true; //Return True if so
            }
        }
        return false; //No Component within range
    }

}
