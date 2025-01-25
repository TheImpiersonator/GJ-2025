using System.Collections;
using System.Collections.Generic;
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
        Vector3 moveVector = new Vector3(horizVector.x, 0, vertVector.z);
        unityController.Move(moveVector * Time.deltaTime);
        transform.Rotate(turnVector * turnSpeed * Time.deltaTime);
    }

    public override void MoveVertical(float moveDir) {
        vertVector = new Vector3(0, 0, Mathf.Clamp(moveDir, -1, 1));
        vertVector = transform.InverseTransformDirection(vertVector);
    }

    public override void MoveHorizontal(float moveDir) {
        horizVector = new Vector3(Mathf.Clamp(moveDir, -1, 1), 0, 0);
    }

    public override void RotatePawn(float rotDir) {
        turnVector = new Vector3(0, Mathf.Clamp(rotDir, -1, 1), 0);
    }
}
