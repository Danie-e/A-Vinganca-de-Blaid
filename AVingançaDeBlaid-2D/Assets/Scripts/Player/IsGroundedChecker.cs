using UnityEngine;

public class IsGroundedChecker : MonoBehaviour
{
    [SerializeField] private Transform checkPosition;
    [SerializeField] private Vector2 checkerSize;
    [SerializeField] private LayerMask groundLayer;

    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(checkPosition.position, checkerSize, 0f, groundLayer);
    }

    private void OnDrawGizmos()
    {
        if (checkPosition == null) return;
        if (IsGrounded())
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }
        Gizmos.DrawWireCube(checkPosition.position, checkerSize);
    }
}
