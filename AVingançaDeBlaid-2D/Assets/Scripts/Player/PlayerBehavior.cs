using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    private InputMenager inputMenager;
    void Start()
    {
        inputMenager = new InputMenager();
    }

    private void Update()
    {
        float movieDirection = inputMenager.Movement;
        transform.Translate(movieDirection, 0, 0);
    }
}
