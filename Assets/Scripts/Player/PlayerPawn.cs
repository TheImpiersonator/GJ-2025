using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/*=====| NOTES | ====== 
    (1/25/2025)
    - Wanting to test out how it feels to restrain the vertical movement during strafes to better define the horizontal movement
    |__ Edits made in Update() | Created strafeMove() method | Made new float for restraining speed when strafing]
 */

[RequireComponent(typeof(CharacterController))]
public class PlayerPawn : Pawn {

    private CharacterController unityController;
    private Vector3 vertVector;
    private Vector3 horizVector;
    private Vector3 turnVector;

    [Tooltip ("Restrains vertical movement by percentage during strafes")]
    [SerializeField] float strafeRestraint; //Added for hidden strafing speed mechaninc

    void Start() {
        unityController = GetComponent<CharacterController>();
    }

    void Update() {
        //normalized total movment direction
        Vector3 moveVector = Vector3.Normalize(vertVector + horizVector);

        //__TWO MOVEMENT SITUATIONS {Non-Strafe & Strafe}__
        //|__CHECK: There is Horizontal Input
        if (horizVector.magnitude > 0) {
            strafeMove();
        }
        //|__ELSE: Move Normally
        else{
            //move player
            unityController.Move(moveVector * moveSpeed * Time.deltaTime);
        }

        
        //rotate player
        transform.Rotate(turnVector * turnSpeed * Time.deltaTime);
    }

    void strafeMove() {
        //Get the adjusted speed for vertical movement
        float adjSpeed = moveSpeed * strafeRestraint;
        // Normalize horizontal movement to maintain direction
        Vector3 normalizedHorizontal = horizVector.normalized;
        // Move player using the horizontal input at full speed
        unityController.Move(normalizedHorizontal * moveSpeed * Time.deltaTime);
        // Apply vertical movement with restraint
        unityController.Move(vertVector * adjSpeed * Time.deltaTime);
    }

    public override void MoveVertical(float moveDir) {
        //using forward direciton * decided movement float
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
