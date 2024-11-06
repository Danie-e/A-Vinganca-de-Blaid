using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public InputManager InputManager { get; private set; }
    [Header("Dynamic Game Object")]
    [SerializeField] private GameObject BossDoor;

    [Header("Managers")]
    public UIManager UIManager;
    public AudioManager AudioManager;

    private int totalKeys;
    private int keysLeftToCollect;

    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);
        Instance = this;

        totalKeys = FindObjectsOfType<CollectableKey>().Length;
        keysLeftToCollect = totalKeys;
        UIManager.UpdatekeysLeftText(totalKeys, keysLeftToCollect);

        InputManager = new InputManager();
    }

    public void UpdateKeysLeft()
    {
        keysLeftToCollect--;
        UIManager.UpdatekeysLeftText(totalKeys, keysLeftToCollect);
        CheckAllKeysCollected();
    }

    private void CheckAllKeysCollected()
    {
        if (keysLeftToCollect <= 0)
        {
            Destroy(BossDoor);
        }
    }
    public void UpdateLives(int amount)
    {
        UIManager.UpdateLivesText(amount);
    }
}
