using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public InputManager InputManager { get; private set; }
    [Header("Dynamic Game Object")]
    [SerializeField] private GameObject BossDoor;
    [SerializeField] private PlayerBehavior player;

    [Header("Managers")]
    public UIManager UIManager;
    public AudioManager AudioManager;

    private int totalKeys;
    private int keysLeftToCollect = 0;

    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);
        Instance = this;

        totalKeys = FindObjectsOfType<CollectableKey>().Length;
        UIManager.UpdatekeysLeftText(totalKeys, keysLeftToCollect);

        InputManager = new InputManager();
    }

    public void UpdateKeysLeft()
    {
        keysLeftToCollect++;
        UIManager.UpdatekeysLeftText(totalKeys, keysLeftToCollect);
        CheckAllKeysCollected();
    }

    private void CheckAllKeysCollected()
    {
        if (keysLeftToCollect == 5)
        {
            Destroy(BossDoor);
        }
    }
    public void UpdateLives(int amount)
    {
        UIManager.UpdateLivesText(amount);
    }
    public PlayerBehavior GetPlayer() => player;
}
