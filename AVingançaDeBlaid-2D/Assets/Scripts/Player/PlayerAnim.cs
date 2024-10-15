using System;
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
    }

    private void Update()
    {
        bool isMoving = GameMenager.Instance.inputMenager.Movement != 0;
        animator.SetBool("isMoving", isMoving);

        animator.SetBool("isJumped", !isGroundedChecker.IsGrounded());
    }

    private void PlayerHurtAnim()
    {
        animator.SetTrigger("hurt");
    }
}
