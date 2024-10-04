using System;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private float movieSpead = 10;

    private void Start()
    {
        GameMenager.Instance.inputMenager.OnJump += HandleJump;
    }

    private void HandleJump()
    {
        Debug.Log("Estou pulando!");
        Console.WriteLine("Estou pulando!");
    }

    private void Update()
    {
        float movieDirection = GameMenager.Instance.inputMenager.Movement;
        transform.Translate(movieDirection * Time.deltaTime * movieSpead, 0, 0);
    }

}
