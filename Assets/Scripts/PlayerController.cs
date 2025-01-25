using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller {

    public InputAction move;

    private void Start() {
        //check if we have a GameManager
        if (GameManager.Instance != null) {
            //check if player slot is empty
            if (GameManager.Instance.player == null) {
                //make self player
                GameManager.Instance.player = this;
            }
        }
        move.Enable();
    }
    public void OnDestroy() {
        //check if we have a GameManager
        if (GameManager.Instance != null) {
            //check if player exists
            if (GameManager.Instance.player != null) {
                //remove self as player
                GameManager.Instance.player = null;
            }
        }
    }

    private void Update() {
        ProcessInputs();
    }

    private void ProcessInputs() {
        if (pawn != null) {
            Vector2 moveValue = move.ReadValue<Vector2>();
            if (Mathf.Abs(moveValue.y) > 0) {
                pawn.MoveVertical(moveValue.y);
                pawn.MoveHorizontal(0f);
                pawn.RotatePawn(moveValue.x);
            }
            else if (moveValue.y == 0) {
                pawn.MoveVertical(moveValue.y);
                pawn.MoveHorizontal(moveValue.x);
                pawn.RotatePawn(0f);
            }
        }
    }
}
