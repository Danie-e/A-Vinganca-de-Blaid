using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMenager : MonoBehaviour
{
    private PlayerControls playerControls;
    public float Movement => playerControls.GamePlay.Moviment.ReadValue<float>();

    public event Action OnJump;
    public event Action OnAttack;
    public InputMenager()
    {
        playerControls = new PlayerControls();
        playerControls.GamePlay.Enable();

        playerControls.GamePlay.Jump.performed += OnJumpPerformed;
        playerControls.GamePlay.Atack.performed += OnAttackPerformed;

    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        OnAttack?.Invoke();
    }

    private void OnJumpPerformed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }
}
