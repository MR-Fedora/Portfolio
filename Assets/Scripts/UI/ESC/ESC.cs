using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ESC : MonoBehaviour
{
    RectTransform rect;
    public bool active=false;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Show()
    {
        active = true;
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        active = false;
        rect.localScale = Vector3.zero;
        GameManager.instance.Resume();
    }
}
