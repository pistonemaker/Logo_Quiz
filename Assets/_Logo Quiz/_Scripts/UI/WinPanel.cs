using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : MonoBehaviour
{
    public Button closeButton;
    public Button restartButton;
    public Button nextStageButton;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(ClosePanel);
        restartButton.onClick.AddListener(RestartStage);
        nextStageButton.onClick.AddListener(LoadNextStage);
    }
    
    private void OnDisable()
    {
        closeButton.onClick.RemoveAllListeners();
        restartButton.onClick.RemoveAllListeners();
        nextStageButton.onClick.RemoveAllListeners();
    }

    private void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    private void RestartStage()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    private void LoadNextStage()
    {
        var curStage = PlayerPrefs.GetInt(DataKey.Cur_Stage);
        PlayerPrefs.SetInt(DataKey.Cur_Stage, curStage + 1);
        SceneManager.LoadSceneAsync("Game");
    }
}
