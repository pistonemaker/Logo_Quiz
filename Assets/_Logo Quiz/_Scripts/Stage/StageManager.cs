using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : Singleton<StageManager>
{
    public StageData data;
    public Blank blankPrefab;
    public Alphabet alphabetPrefab;
    public HorizontalLayoutGroup blankGroup;
    public GridLayoutGroup alphabetGroup;
    public Image logoImage;

    public List<Blank> blankList;
    public List<Alphabet> alphabetList;
    public string rightAnswer;
    public int ID;

    public void InitStage(int id)
    {
        ID = id;
        WinPanel.Instance.id = ID;
        data = GameManager.Instance.data.gameData[id];
        logoImage.sprite = data.logo;
        rightAnswer = data.answer.ToUpper();
        CreatBlanks();
        CreateAlphabets();
        StartCoroutine(DeactiveLayoutGroup());
    }

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.On_Player_Fill_All_Blanks, CheckIfPlayerFillAllBlanks);
        this.RegisterListener(EventID.On_Load_Stage, (param) => InitStage((int) param));
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.On_Player_Fill_All_Blanks, CheckIfPlayerFillAllBlanks);
        this.RemoveListener(EventID.On_Load_Stage, (param) => InitStage((int) param));
    }

    private void CreatBlanks()
    {
        for (int i = 0; i < data.blankNumber; i++)
        {
            var blank = PoolingManager.Spawn(blankPrefab, transform.position, Quaternion.identity);
            blank.transform.SetParent(blankGroup.GetComponent<RectTransform>());
            blank.transform.localScale = Vector3.one;
            blank.name = "Blank " + (i + 1);
            blankList.Add(blank);
            blank.transform.SetSiblingIndex(i);
        }
    }

    private void CreateAlphabets()
    {
        for (int i = 0; i < data.alphabets.Count; i++)
        {
            var alphabet = PoolingManager.Spawn(alphabetPrefab, transform.position, Quaternion.identity);
            alphabet.id = i;
            alphabet.transform.SetParent(alphabetGroup.GetComponent<RectTransform>());
            alphabet.transform.localScale = Vector3.one;
            alphabet.name = "Alphabet " + (i + 1);
            alphabet.image.sprite = data.alphabets[i];
            alphabetList.Add(alphabet);
            alphabet.transform.SetSiblingIndex(i);
        }
    }

    private IEnumerator DeactiveLayoutGroup()
    {
        yield return null;
        yield return null;
        blankGroup.enabled = false;
        alphabetGroup.enabled = false;
    }

    public void GetFirstNotFilledBlank(Alphabet alphabet)
    {
        for (int i = 0; i < blankList.Count; i++)
        {
            if (!blankList[i].isFilled)
            {
                alphabet.transform.SetParent(blankGroup.transform);
                alphabet.SetTarget(blankList[i]);
                break;
            }
        }
    }

    public void ResetAlphabet(Alphabet alphabet)
    {
        alphabet.transform.SetParent(alphabetGroup.transform);
        alphabet.SetTarget(null);
    }

    public bool IsPlayerFillAllBlanks()
    {
        for (int i = 0; i < blankList.Count; i++)
        {
            if (!blankList[i].isFilled)
            {
                return false;
            }
        }

        return true;
    }

    private void CheckIfPlayerFillAllBlanks(object param)
    {
        if (!IsPlayerFillAllBlanks())
        {
            return;
        }

        string playerAnswer = "";

        for (int i = 0; i < blankList.Count; i++)
        {
            playerAnswer += data.alphabets[blankList[i].alphabetIndex].name;
        }

        var check = String.CompareOrdinal(playerAnswer.ToUpper(), rightAnswer);

        if (check == 0)
        {
            EventDispatcher.Instance.PostEvent(EventID.On_Player_Win, ID);
        }
        else
        {
            for (int i = 0; i < blankList.Count; i++)
            {
                Debug.Log(i);
                if (playerAnswer[i] != rightAnswer[i])
                {
                    alphabetList[blankList[i].alphabetIndex].BackAnchorPosition();
                }
            }
        }
    }

    public void ResetPanel()
    {
        for (int i = 0; i < blankList.Count; i++)
        {
            PoolingManager.Despawn(blankList[i].gameObject);
        }
        
        for (int i = 0; i < alphabetList.Count; i++)
        {
            PoolingManager.Despawn(alphabetList[i].gameObject);
        }
        
        blankList.Clear();
        alphabetList.Clear();
        
        blankGroup.enabled = true;
        alphabetGroup.enabled = true;
    }
}