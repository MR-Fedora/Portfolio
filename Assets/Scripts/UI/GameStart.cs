using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public void GameMainStart()
    {
        GameManager.instance.GameStart();
    }
    public void GameReTry()
    {
        GameManager.instance.GameReTry();
    }
}
