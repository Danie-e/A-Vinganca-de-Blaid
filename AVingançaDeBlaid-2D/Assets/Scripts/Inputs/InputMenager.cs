using UnityEngine;

public class InputMenager : MonoBehaviour
{
    private PlayerControls playerControls;
    public float Movement => playerControls.GamePlay.Moviment.ReadValue<float>();

    public InputMenager()
    {
        playerControls = new PlayerControls();
        playerControls.GamePlay.Enable();
    }
}
