using UnityEngine;

public class Blank : MonoBehaviour
{
    public RectTransform rectTransform;
    public bool isFilled;

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
