using System;
using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float AttackRanger = 1f;

    private Rigidbody2D rigidbody;
    private Transform playerPosition;

    private bool canAttack = false;
    private bool isFlipped = true;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        playerPosition = GameManager.Instance.GetPlayer().transform;

    }
    public void FollowPlayer()
    {
        Vector2 target = new Vector2(playerPosition.position.x, transform.position.y);
        Vector2 newPos = Vector2.MoveTowards(rigidbody.position, target, moveSpeed * Time.fixedDeltaTime);
        rigidbody.MovePosition(newPos);
        LookAtPlayer();
        CheckPositionFromPlayer();
    }

    private void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > playerPosition.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < playerPosition.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }

    private void CheckPositionFromPlayer()
    {
        float distanceFromPlayer = Vector2.Distance(playerPosition.position, transform.position);
        if (distanceFromPlayer <= AttackRanger)
            canAttack = true;
        else
            canAttack = false;
    }
    public bool GetCanAttack()
    {
        return canAttack;
    }
}
