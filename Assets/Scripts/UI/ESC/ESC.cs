using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ESC : MonoBehaviour
{
    RectTransform rect;
    public GameObject soundSetting;
    bool on=false;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    public void Show()
    {
        on=true;
        rect.localScale = Vector3.one;
        GameManager.instance.Stop();
    }

    public void Hide()
    {
        on=false;
        rect.localScale = Vector3.zero;
        if (!GameManager.instance.uiLevelUp.on)
        {
            GameManager.instance.Resume();
        }
    }

    private void OnBack(InputValue value)
    {
        if(!on)
        {
            Show();
        }
        else if(on && !soundSetting.activeSelf)
        {
            Hide();
        }
    }
}
