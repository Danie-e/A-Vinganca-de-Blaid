using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    [SerializeField]
    private float movieSpead = 10;

    private void Update()
    {
        float movieDirection = GameMenager.Instance.inputMenager.Movement;
        transform.Translate(movieDirection * Time.deltaTime * movieSpead, 0, 0);
    }

}
