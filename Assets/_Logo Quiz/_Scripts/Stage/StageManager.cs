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

    public List<Blank> blankList;
    public List<Alphabet> alphabetList;
    public string rightAnswer;

    public void Init()
    {
        
    }

    private void OnEnable()
    {
        data = GameManager.Instance.data.gameData[PlayerPrefs.GetInt(DataKey.Cur_Stage)];
        rightAnswer = data.answer.ToUpper();
        CreatBlanks();
        CreateAlphabets();
        StartCoroutine(DeactiveLayoutGroup());
        EventDispatcher.Instance.RegisterListener(EventID.On_Player_Fill_All_Blanks, CheckIfPlayerFillAllBlanks);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.On_Player_Fill_All_Blanks, CheckIfPlayerFillAllBlanks);
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
            EventDispatcher.Instance.PostEvent(EventID.On_Player_Win);
        }
        else
        {
            for (int i = 0; i < blankList.Count; i++)
            {
                if (playerAnswer[i] != rightAnswer[i])
                {
                    alphabetList[blankList[i].alphabetIndex].BackAnchorPosition();
                }
            }
        }
    }
}
