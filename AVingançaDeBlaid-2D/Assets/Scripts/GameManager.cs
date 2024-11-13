using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Managers")]
    public AudioManager AudioManager;
    public UIManager UIManager;

    public InputManager InputManager { get; private set; }
    [Header("Dynamic Game Object")]
    [SerializeField] private GameObject BossDoor;
    [SerializeField] private PlayerBehavior player;
    [SerializeField] private BossBehavior boss;
    [SerializeField] private BossFigthTrigger bossFigthTrigger;

    private int totalKeys;
    private int keysLeftToCollect = 0;

    private void Awake()
    {
        if (Instance != null) Destroy(this.gameObject);
        Instance = this;

        InputManager = new InputManager();

        totalKeys = FindObjectsOfType<CollectableKey>().Length;
        UIManager.UpdatekeysLeftText(totalKeys, keysLeftToCollect);

        bossFigthTrigger.OnPlayerEnterBossFigth += ActivateBossBehavior;

        player.GetComponent<Health>().OnDead += HandleGameOver;
        boss.GetComponent<Health>().OnDead += HandleVictory;
    }

    public void UpdateKeysLeft()
    {
        keysLeftToCollect++;
        UIManager.UpdatekeysLeftText(totalKeys, keysLeftToCollect);
        CheckAllKeysCollected();
    }

    private void CheckAllKeysCollected()
    {
        if (keysLeftToCollect == totalKeys)
        {
            Destroy(BossDoor);
        }
    }

    public PlayerBehavior GetPlayer() => player;

    private void ActivateBossBehavior()
    {
        boss.StartChasing();
    }

    public void UpdateLives(int amount)
    {
        UIManager.UpdateLivesText(amount);
    }

    private void HandleGameOver()
    {
        UIManager.OpenGameOverPanel();
    }

    private void HandleVictory()
    {
        UIManager.OpenVictoryText();
        StartCoroutine(GoToCreditsScene());
    }

    private IEnumerator GoToCreditsScene()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("Credits");
    }
}
