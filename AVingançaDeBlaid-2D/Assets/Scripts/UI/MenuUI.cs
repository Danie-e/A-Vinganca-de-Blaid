using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUI : MonoBehaviour
{
    [SerializeField] private GameObject optionsPanel;

    [Header("Menu UI proprerties")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    private void OnEnable()
    {
        startButton.onClick.AddListener(GoToGameplayScene);
        optionsButton.onClick.AddListener(OpenOptionsMenu);
        exitButton.onClick.AddListener(ExitGame);
    }

    private void GoToGameplayScene()
    {
        GameMenager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
        SceneManager.LoadScene("Gameplay");
    }
    private void OpenOptionsMenu()
    {
        GameMenager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);
        optionsPanel.SetActive(true);
    }
    private void ExitGame()
    {
        GameMenager.Instance.AudioManager.PlaySFX(SFX.ButtonClick);

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

}
