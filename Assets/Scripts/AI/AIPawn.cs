using UnityEngine;

[RequireComponent(typeof(CharacterController))] //Required to have a Character Controller
public class AIPawn : Pawn
{
    //built in character controller
    private CharacterController unityController;
    //vectors for pawn movement
    private Vector3 vertVector;
    private Vector3 horizVector;
    private Quaternion turnVector;

    [SerializeField] int scoreReward = 0; //Score for killing the enemy

    private void Awake()
    {
        HealthComponent healthSystem = GetComponent<HealthComponent>();

        healthSystem.OnDeath += GrantReward;
        healthSystem.OnDeath += Despawn;
    }
    void Start()
    {
        unityController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        //normalized total movment direction
        Vector3 moveVector = Vector3.Normalize(vertVector + horizVector);

        //move pawn
        unityController.SimpleMove(moveVector * moveSpeed);

        //rotate pawn
        transform.rotation = turnVector;
    }

    /*KINEMATICS*/
    public override void MoveHorizontal(float moveDir)
    {
        //using forward direciton * provided movement float
        vertVector = moveDir * transform.forward;
    }

    public override void MoveVertical(float moveDir)
    {
        //using right direciton * provided input float
        horizVector = moveDir * transform.right;
    }
    public override void RotateTowards(Vector3 targetPosition)
    {
        //vector pointing at target
        Vector3 vectorToTarget = targetPosition - transform.position;
        //making Quaternion out of vector
        Quaternion targetRotation = Quaternion.LookRotation(vectorToTarget, Vector3.up);
        //rotation for pawn to move to
        turnVector = Quaternion.RotateTowards(transform.rotation, targetRotation, turnSpeed * Time.deltaTime);
    }

    public override void Dash(Vector2 direction)
    {
        //not used in AI
        throw new System.NotImplementedException();
    }
    public override void RotatePawn(float rotDir)
    {
        //not used for AI
        throw new System.NotImplementedException();
    }

    public override void Teleport(Vector3 newLocation)
    {
        //wont be used since only player teleports
        throw new System.NotImplementedException();
    }

    /* =====| Actions |=====*/
    /* Despawns/Destroys the pawn */
    void Despawn()
    {
        Destroy(gameObject); //Destroy self
    }
    /*Grant the reward of the pawn to the Game Manager's score*/
    void GrantReward()
    {
        GameManager.Instance.AdjustScore(scoreReward);
    }
}
