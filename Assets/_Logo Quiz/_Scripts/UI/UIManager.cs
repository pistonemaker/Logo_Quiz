using UnityEngine.UI;

public class UIManager : Singleton<UIManager>
{
    public Image logoImage; 
    public WinPanel winPanel;

    private void OnEnable()
    {
        logoImage.sprite = StageManager.Instance.data.logo;
        winPanel.gameObject.SetActive(false);
        EventDispatcher.Instance.RegisterListener(EventID.On_Player_Win, OnPlayerWin);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.On_Player_Win, OnPlayerWin);
    }

    private void OnPlayerWin(object param)
    {
        winPanel.gameObject.SetActive(true);
    }
}
