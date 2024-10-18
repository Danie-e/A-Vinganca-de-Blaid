using UnityEngine;

public class GameMenager : MonoBehaviour
{
    public static GameMenager Instance;

    public AudioManager AudioManager;
    public InputMenager inputMenager { get; private set; }

    private void Awake()
    {
        if (Instance != null)
            Destroy(this.gameObject);
        Instance = this;
        inputMenager = new InputMenager();
    }

}
