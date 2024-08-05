using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage ", menuName = "Data / Stage Data")]
public class StageData : ScriptableObject
{
    public int blankNumber;
    public Sprite logo;
    public string answer;
    public List<Sprite> alphabets;
}
