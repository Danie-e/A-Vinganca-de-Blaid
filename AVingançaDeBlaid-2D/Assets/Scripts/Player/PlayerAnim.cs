using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private IsGroundedChecker isGroundedChecker;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isGroundedChecker = GetComponent<IsGroundedChecker>();
    }

    private void Update()
    {
        bool isMoving = GameMenager.Instance.inputMenager.Movement != 0;
        animator.SetBool("isMoving", isMoving);

        animator.SetBool("isJumped", !isGroundedChecker.IsGrounded());
    }
}
