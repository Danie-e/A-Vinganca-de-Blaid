using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    private IsGroundedChecker isGroundedChecker;
    private Animator animator;
    private Health playerHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        isGroundedChecker = GetComponent<IsGroundedChecker>();
        playerHealth = GetComponent<Health>();

        playerHealth.OnHurt += PlayerHurtAnim;
        playerHealth.OnDead += PlayerDeadAnim;

        GameMenager.Instance.inputMenager.OnAttack += PlayerAttackAnim;
    }

    private void Update()
    {
        bool isMoving = GameMenager.Instance.inputMenager.Movement != 0;
        animator.SetBool("isMoving", isMoving);

        animator.SetBool("isJumped", !isGroundedChecker.IsGrounded());
    }

    private void PlayerAttackAnim()
    {
        animator.SetTrigger("attack");
    }

    private void PlayerDeadAnim()
    {
        animator.SetTrigger("dead");
    }

    private void PlayerHurtAnim()
    {
        animator.SetTrigger("hurt");
    }
}
