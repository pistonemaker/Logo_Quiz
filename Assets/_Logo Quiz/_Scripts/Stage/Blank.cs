using UnityEngine;

public class Blank : MonoBehaviour
{
    public RectTransform rectTransform;

    private void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
    }
}
