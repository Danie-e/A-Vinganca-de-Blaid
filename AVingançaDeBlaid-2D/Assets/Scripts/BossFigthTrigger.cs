using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BossFigthTrigger : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            print("Passou pela porta");
        }
    }
}
