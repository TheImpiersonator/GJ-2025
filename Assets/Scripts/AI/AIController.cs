using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AIController : Controller
{
    [Tooltip("View angle for the AI view cone")]
    public float viewAngle = 35f;
    [Tooltip("Max distance that the AI can see the player at")]
    public float viewDistance;
    [Tooltip("Current action of the AI's state machine")]
    public enum AIState {Idle, Chase, Patrol, Seek, ChooseTarget};
    public AIState currentState;

    public GameObject target;

    private Vector3 lastTargetPosition;

    void Update() {
        ProcessAI();
    }

    private void ProcessAI() {
        bool moving = false;
        switch (currentState) {
            case AIState.Idle:
                // Check for transitions
                if (CanSee()) {
                    ChangeState(AIState.Chase);
                }
                break;
            case AIState.Chase:
                if(CanSee()) {
                    //rotate to face player
                    pawn.RotateTowards(target.transform.position);
                    //move towards player
                    pawn.MoveHorizontal(1f);
                    moving = true;
                }
                else {
                    lastTargetPosition = target.transform.position;
                    ChangeState(AIState.Patrol);
                }
                break;
            case AIState.Patrol:

                break;
            case AIState.Seek:
                //going to last position player was seen
                if(!CanSee()) {
                    //if reached last seen position
                    if((pawn.transform.position - lastTargetPosition).magnitude < 0.1f) {
                        ChangeState((AIState)AIState.Idle);
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
            case AIState.ChooseTarget:
                //this is really only used at the start, to prevent errors
                TargetPlayer();
                break;
            default:
                ChangeState(AIState.ChooseTarget);
                break;
        }
        if (!moving) {
            pawn.MoveHorizontal(0f);
        }
    }
    public virtual void ChangeState(AIState newState) {
        // Change the current state
        currentState = newState;

    }
    public void TargetPlayer() {
        // If the GameManager exists
        if (GameManager.Instance != null) {
            // And the array of players exists
            if (GameManager.Instance.player != null) {
                //Then target the gameObject of the pawn of the player controller
                target = GameManager.Instance.player.pawn.gameObject;
                ChangeState(AIState.Idle);
            }
        }
    }
    private bool CanSee() {
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
        if (hit.collider != null) {
            if (hit.collider.gameObject == target) {
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
        if (angleToTarget < viewAngle && hasSightLine) {
            return true;
        }
        return false;
    }
}
