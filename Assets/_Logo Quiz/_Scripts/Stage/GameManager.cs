using System;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : Singleton<GameManager>
{
    public GameData data;

    private void OnEnable()
    {
        EventDispatcher.Instance.RegisterListener(EventID.On_Player_Win, OnPlayerWin);
    }

    private void OnDisable()
    {
        EventDispatcher.Instance.RemoveListener(EventID.On_Player_Win, OnPlayerWin);
    }

    private void OnPlayerWin(object param)
    {
        var curStage = PlayerPrefs.GetInt(DataKey.Cur_Stage);
        PlayerPrefs.SetInt(DataKey.Cur_Stage, curStage + 1);
    }
}
