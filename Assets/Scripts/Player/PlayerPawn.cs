using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*=====| NOTES | ====== 
    (1/25/2025)
    - Wanting to test out how it feels to restrain the vertical movement during strafes to better define the horizontal movement
    |__ Edits made in Update() | Created strafeMove() method | Made new float for restraining speed when strafing]
    - Changed back to old method, implemented simpler math for strafe addition.
        still had to use an if-else because unity is dumb and apparently hates simple vector calculations.
 */

[RequireComponent(typeof(CharacterController))]
public class PlayerPawn : Pawn {
    //built in character controller
    private CharacterController unityController;
    [ SerializeField] HealthComponent healthSystem;
    [SerializeField] SodaQueueSystem sodaQueue;

    //vectors for player movement
    private Vector3 vertVector;
    private Vector3 horizVector;
    private Vector3 turnVector;
    //vectors for the movement smoothing
    private Vector3 currentSpeed;
    private Vector3 currentVelocity;

    [Tooltip("Restrains vertical movement by percentage during strafes")]
    [SerializeField] float strafeRestraint; //Added for hidden strafing speed mechaninc
    [Tooltip("Amount of time for character to accelerate to full speed")]
    [SerializeField] float accelTime;

    void Start()
    {
        unityController = GetComponent<CharacterController>();

        //If PLayer dies, End the Game
        healthSystem.OnDeath += GameManager.Instance.Start_GameOver;

    }

    void Update() {
        //vector for pawn movement
        Vector3 moveVector = Vector3.zero;

        //reduce vertical move vector when strafing
        if (horizVector.magnitude > 0) {
            //scaling vector by restraint multiplier
            Vector3 newVec = vertVector * strafeRestraint;
            //normalized total movment direction (scaled movement)
            moveVector = Vector3.Normalize(newVec + horizVector);
        }
        else {
            //normalized total movment direction (raw movment)
            moveVector = Vector3.Normalize(vertVector + horizVector);
        }

        //smoothing the movement vector
        currentSpeed = Vector3.SmoothDamp(currentSpeed, moveVector, ref currentVelocity, accelTime);

        //move player
        unityController.SimpleMove(currentSpeed * moveSpeed);

        //rotate player
        transform.Rotate(turnVector * turnSpeed * Time.deltaTime);
    }

    public override void MoveVertical(float moveDir) {
        //using forward direciton * provided movement float
        vertVector = moveDir * transform.forward;
    }

    public override void MoveHorizontal(float moveDir) {
        //using right direciton * provided input float
        horizVector = moveDir * transform.right;
    }

    public override void RotatePawn(float rotDir) {
        turnVector = new Vector3(0, rotDir, 0);
    }
    public override void Dash(Vector2 direction) {
        //not done yet
    }

    public override void RotateTowards(Vector3 targetPosition) {
        //shouldnt be used for player
        throw new System.NotImplementedException();
    }

    public override void Teleport(Vector3 newLocation) {
        //have to disable the player controller before and after the teleport, otherwise it will override the change
        unityController.enabled = false;
        this.transform.position = newLocation;
        unityController.enabled = true;
    }

    public void UseSoda() {
        Debug.Log("Using Soda w/ count " + sodaQueue.gameObject);
        if (GetComponent<SodaQueueSystem>().get_Queue().Count > 0)
        {
            Debug.Log("PLEASE WORK OMG PLES");
            GetComponent<SodaQueueSystem>().get_Queue()[0].Shoot();
            GetComponent<SodaQueueSystem>().RemoveSoda();
        }
        else { Debug.LogWarning("No Soda available"); }
    }
}
