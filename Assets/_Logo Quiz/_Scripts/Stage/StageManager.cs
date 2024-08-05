using UnityEngine;
using UnityEngine.UI;

public class StageManager : Singleton<StageManager>
{
    public StageData data;
    public Blank blankPrefab;
    public Alphabet alphabetPrefab;
    public HorizontalLayoutGroup blankGroup;
    public GridLayoutGroup alphabetGroup;

    public void Init()
    {
        
    }

    private void OnEnable()
    {
        CreatBlanks();
        CreateAlphabets();
    }

    private void CreatBlanks()
    {
        for (int i = 0; i < data.blankNumber; i++)
        {
            var blank = PoolingManager.Spawn(blankPrefab, transform.position, Quaternion.identity);
            blank.transform.SetParent(blankGroup.transform);
            blank.transform.localScale = Vector3.one;
            blank.name = "Blank " + (i + 1);
        }
    }

    private void CreateAlphabets()
    {
        for (int i = 0; i < data.alphabets.Count; i++)
        {
            var alphabet = PoolingManager.Spawn(alphabetPrefab, transform.position, Quaternion.identity);
            alphabet.transform.SetParent(alphabetGroup.transform);
            alphabet.transform.localScale = Vector3.one;
            alphabet.name = "Alphabet " + (i + 1);
            alphabet.image.sprite = data.alphabets[i];
        }
    }
}
