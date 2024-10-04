using UnityEngine;
using UnityEngine.UI;

public class StageButton : MonoBehaviour
{
    public Image image;
    public Button button;
    public int id;

    private void OnEnable()
    {
        button.onClick.AddListener(LoadStage);
    }

    private void LoadStage()
    {
        PlayerPrefs.SetInt(DataKey.Cur_Stage, id);
        ChooseStageManager.Instance.gameObject.SetActive(false);
        this.PostEvent(EventID.On_Load_Stage, id);
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }
}
