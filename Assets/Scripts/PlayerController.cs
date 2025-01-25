using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : Controller {

    private void Start() {
        //check if we have a GameManager
        if (GameManager.Instance != null) {
            //check if player slot is empty
            if (GameManager.Instance.player == null) {
                //make self player
                GameManager.Instance.player = this;
            }
        }
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

    public void ProcessInputs(InputAction.CallbackContext context) {
        if (pawn != null) {
            //read value from input manager
            Vector2 moveValue = context.ReadValue<Vector2>();
            //if forwards/backwards
            if (Mathf.Abs(moveValue.y) > 0) {
                pawn.MoveVertical(moveValue.y);
                pawn.MoveHorizontal(moveValue.x);
                pawn.RotatePawn(0f);
            }
            //rotation if not moving
            else if (moveValue.y == 0) {
                pawn.MoveVertical(moveValue.y);
                pawn.MoveHorizontal(0f);
                pawn.RotatePawn(moveValue.x);
            }
        }
    }
}
