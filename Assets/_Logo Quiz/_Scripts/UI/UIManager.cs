using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Button backButton;
    public WinPanel winPanel;

    private void OnEnable()
    {
        winPanel.gameObject.SetActive(false);
        backButton.onClick.AddListener(LoadChooseStage);
        EventDispatcher.Instance.RegisterListener(EventID.On_Player_Win, OnPlayerWin);
    }

    private void LoadChooseStage()
    {
        StageManager.Instance.ResetPanel();
        ChooseStageManager.Instance.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        backButton.onClick.RemoveAllListeners();
        EventDispatcher.Instance.RemoveListener(EventID.On_Player_Win, OnPlayerWin);
    }

    private void OnPlayerWin(object param)
    {
        winPanel.gameObject.SetActive(true);
    }
}
