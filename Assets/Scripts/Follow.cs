using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    RectTransform rect;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void FixedUpdate()
    {
        Vector3 playerPos = GameManager.instance.player.transform.position;
        playerPos.x = playerPos.x - 2f;
        playerPos.y = playerPos.y - 8f;
        rect.position = Camera.main.WorldToScreenPoint(playerPos);
    }
}
