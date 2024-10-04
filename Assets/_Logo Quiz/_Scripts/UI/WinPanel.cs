using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinPanel : Singleton<WinPanel>
{
    public Button closeButton;
    public Button restartButton;
    public Button nextStageButton;
    public int id;

    private void OnEnable()
    {
        closeButton.onClick.AddListener(ClosePanel);
        restartButton.onClick.AddListener(RestartStage);
        
        if (id >= GameManager.Instance.data.gameData.Count - 1)
        {
            nextStageButton.gameObject.SetActive(false);
        }
        else
        {
            nextStageButton.onClick.AddListener(LoadNextStage);
        }
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
        if (id > PlayerPrefs.GetInt(DataKey.Max_Stage))
        {
            PlayerPrefs.SetInt(DataKey.Max_Stage, id);
        }
        
        PlayerPrefs.SetInt(DataKey.Cur_Stage, id + 1);
        ClosePanel();
        
        StageManager.Instance.ResetPanel();
        ChooseStageManager.Instance.gameObject.SetActive(false);
        this.PostEvent(EventID.On_Load_Stage, id + 1);
    }
}
