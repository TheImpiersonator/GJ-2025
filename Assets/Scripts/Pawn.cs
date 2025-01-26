using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Pawn : MonoBehaviour
{
    [Tooltip("Force added to player for movement")]
    public float moveSpeed;

    [Tooltip("Speed at which the pawn will rotate")]
    public float turnSpeed;

    public Controller controller;

    public abstract void MoveVertical(float moveDir);
    public abstract void MoveHorizontal(float moveDir);
    public abstract void RotatePawn(float rotDir);
    public abstract void RotateTowards(Vector3 targetPosition);
}
