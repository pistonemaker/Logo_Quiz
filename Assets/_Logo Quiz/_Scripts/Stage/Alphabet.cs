using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Alphabet : MonoBehaviour
{
    public int id;
    public RectTransform rectTransform;
    public Image image;
    public Button button;
    public Blank target;
    public Vector2 oldAnchorPos;
    public bool isUsed;

    private void OnEnable()
    {
        isUsed = false;
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
        button = GetComponent<Button>();
        button.onClick.AddListener(MoveAlphabet);
    }

    private void MoveAlphabet()
    {
        if (!isUsed)
        {
            FillBlank();
        }
        else
        {
            BackAnchorPosition();
        }
    }

    private void FillBlank()
    {
        isUsed = true;
        oldAnchorPos = rectTransform.anchoredPosition;
        StageManager.Instance.GetFirstNotFilledBlank(this);
        target.isFilled = true;
        target.alphabetIndex = id;
        rectTransform.anchoredPosition = target.rectTransform.anchoredPosition;
        EventDispatcher.Instance.PostEvent(EventID.On_Player_Fill_All_Blanks);
        
        // rectTransform.DOAnchorPos(target.rectTransform.anchoredPosition, 0.5f).OnComplete(() =>
        // {
        //     StartCoroutine(CheckAnswer());
        // });
    }

    public void BackAnchorPosition()
    {
        isUsed = false;
        target.isFilled = false;
        target.alphabetIndex = -1;
        StageManager.Instance.ResetAlphabet(this);
        rectTransform.DOAnchorPos(oldAnchorPos, 0.5f);
    }

    public void SetTarget(Blank blank)
    {
        target = blank;
    }

    private IEnumerator CheckAnswer()
    {
        yield return new WaitForSeconds(0.5f);
        EventDispatcher.Instance.PostEvent(EventID.On_Player_Fill_All_Blanks);
    }
}
