using System;
using UnityEngine;

/// <summary>
/// Detects collisions around the player using raycasts.
/// This class is responsible only for detecting contact and notifying
/// listeners when collision states change.
/// 
/// TODO: need to rename function names, likewise colliders should carry some information along with 
/// them such as the raycast info RaycastHit2D
/// These also need to avoid hitting triggers
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class BoxRaycasting : MonoBehaviour
{
   // Events

    public event Action GroundEntered;
    public event Action GroundExited;

    public event Action CeilingEntered;
    public event Action CeilingExited;

    public event Action LeftWallEntered;
    public event Action LeftWallExited;

    public event Action RightWallEntered;
    public event Action RightWallExited;

    // Configuration

    [Header("Debug")]
    [SerializeField]
    private bool showRays = false;

    [Header("Raycast Settings")]
    [SerializeField]
    private float rayDistance = 0.1f;

    // Origins

    [Header("Ground")]
    [SerializeField]
    private Transform bottomLeft;

    [SerializeField]
    private Transform bottomRight;

    [Header("Ceiling")]
    [SerializeField]
    private Transform topLeft;

    [SerializeField]
    private Transform topRight;

    [Header("Left Wall")]
    [SerializeField]
    private Transform leftTop;

    [SerializeField]
    private Transform leftBottom;

    [Header("Right Wall")]
    [SerializeField]
    private Transform rightTop;

    [SerializeField]
    private Transform rightBottom;

    // State

    public bool IsGrounded { get; private set; }

    public bool IsTouchingCeiling { get; private set; }

    public bool IsTouchingLeftWall { get; private set; }

    public bool IsTouchingRightWall { get; private set; }

    private void Update()
    {
        CheckCollisions();
    }

    private void CheckCollisions()
    {
        UpdateGroundState(CheckGround());

        UpdateCeilingState(CheckCeiling());

        UpdateLeftWallState(CheckLeftWall());

        UpdateRightWallState(CheckRightWall());
    }

    // Collision Checks

    private bool CheckGround()
    {
        return Physics2D.Raycast(bottomLeft.position, -bottomLeft.up, rayDistance) ||
               Physics2D.Raycast(bottomRight.position, -bottomRight.up, rayDistance);
    }

    private bool CheckCeiling()
    {
        return Physics2D.Raycast(topLeft.position, topLeft.up, rayDistance) ||
               Physics2D.Raycast(topRight.position, topRight.up, rayDistance);
    }

    private bool CheckLeftWall()
    {
        return Physics2D.Raycast(leftTop.position, -leftTop.right, rayDistance) ||
               Physics2D.Raycast(leftBottom.position, -leftBottom.right, rayDistance);
    }

    private bool CheckRightWall()
    {
        return Physics2D.Raycast(rightTop.position, rightTop.right, rayDistance) ||
               Physics2D.Raycast(rightBottom.position, rightBottom.right, rayDistance);
    }

    // State Updates

    private void UpdateGroundState(bool grounded)
    {
        if (grounded == IsGrounded)
            return;

        IsGrounded = grounded;

        if (grounded)
            GroundEntered?.Invoke();
        else
            GroundExited?.Invoke();
    }

    private void UpdateCeilingState(bool touching)
    {
        if (touching == IsTouchingCeiling)
            return;

        IsTouchingCeiling = touching;

        if (touching)
            CeilingEntered?.Invoke();
        else
            CeilingExited?.Invoke();
    }

    private void UpdateLeftWallState(bool touching)
    {
        if (touching == IsTouchingLeftWall)
            return;

        IsTouchingLeftWall = touching;

        if (touching)
            LeftWallEntered?.Invoke();
        else
            LeftWallExited?.Invoke();
    }

    private void UpdateRightWallState(bool touching)
    {
        if (touching == IsTouchingRightWall)
            return;

        IsTouchingRightWall = touching;

        if (touching)
            RightWallEntered?.Invoke();
        else
            RightWallExited?.Invoke();
    }

    // TODO: Should also have a draw method during run time.
    // This would draw the raycase likewise but highlight it red if colliding with something.

    private void OnDrawGizmos()
    {
        if (!showRays)
            return;

        Gizmos.color = Color.green;

        DrawRay(bottomLeft, -bottomLeft.up);
        DrawRay(bottomRight, -bottomRight.up);

        DrawRay(topLeft, topLeft.up);
        DrawRay(topRight, topRight.up);

        DrawRay(leftTop, -leftTop.right);
        DrawRay(leftBottom, -leftBottom.right);

        DrawRay(rightTop, rightTop.right);
        DrawRay(rightBottom, rightBottom.right);
    }

    private void DrawRay(Transform origin, Vector2 direction)
    {
        if (origin == null)
            return;

        Gizmos.DrawLine(
            origin.position,
            origin.position + (Vector3)(direction.normalized * rayDistance));
    }
}