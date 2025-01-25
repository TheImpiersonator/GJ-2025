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

    void Start() {
        unityController = GetComponent<CharacterController>();
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
}
