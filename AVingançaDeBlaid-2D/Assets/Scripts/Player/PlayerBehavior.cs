using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private float movieSpead = 10;

    private InputMenager inputMenager;
    void Start()
    {
        inputMenager = new InputMenager();
    }

    private void Update()
    {
        float movieDirection = inputMenager.Movement * Time.deltaTime * 5;
        transform.Translate(movieDirection, 0, 0);
    }

}
