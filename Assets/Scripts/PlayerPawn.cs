using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerPawn : Pawn {

    private CharacterController unityController;
    private Vector3 vertVector;
    private Vector3 horizVector;
    private Vector3 turnVector;

    void Start() {
        unityController = GetComponent<CharacterController>();
    }

    void Update() {
        //normalized total movment direction
        Vector3 moveVector = Vector3.Normalize(vertVector + horizVector);
        //move player
        unityController.Move(moveVector * moveSpeed * Time.deltaTime);
        //rotate player
        transform.Rotate(turnVector * turnSpeed * Time.deltaTime);
    }

    public override void MoveVertical(float moveDir) {
        //using forward direciton * provided input float
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
