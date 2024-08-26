using System;
using UnityEngine;

public class ChooseStageManager : Singleton<ChooseStageManager>
{
    public StageButton stageButtonPrefab;
    public Transform stageButtonTrf;

    private void OnEnable()
    {
        CreateStageButton();
    }

    private void CreateStageButton()
    {
        var gameData = GameManager.Instance.data.gameData;
        
        for (int i = 0; i < gameData.Count; i++)
        {
            var stageButton = PoolingManager.Spawn(stageButtonPrefab, transform.position, Quaternion.identity);
            stageButton.transform.SetParent(stageButtonTrf);
            stageButton.transform.localScale = Vector3.one;
            stageButton.image.sprite = gameData[i].logo;
            stageButton.id = i;
        }
    }

    private void OnDisable()
    {
        
    }
}
