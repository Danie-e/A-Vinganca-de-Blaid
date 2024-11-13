using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class BossFigthTrigger : MonoBehaviour
{
    public event Action OnPlayerEnterBossFigth;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnterBossFigth?.Invoke();
        }
    }
}
