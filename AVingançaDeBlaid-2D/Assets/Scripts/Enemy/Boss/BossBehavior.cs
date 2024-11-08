using UnityEngine;

public class BossBehavior : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    private Rigidbody2D rigidbody;
    private Transform playerPosition;

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
    }
}
