using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMenager : MonoBehaviour
{
    private PlayerControls playerControls;
    public float Movement => playerControls.GamePlay.Moviment.ReadValue<float>();

    public event Action OnJump;

    public InputMenager()
    {
        playerControls = new PlayerControls();
        playerControls.GamePlay.Enable();

        playerControls.GamePlay.Jump.performed += OnJumpPerformed;
    }


    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnJump.Invoke();
    }
}
