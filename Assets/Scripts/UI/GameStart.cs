using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour
{
    public void GameMainStart(int id)
    {
        GameManager.instance.GameStart(id);
    }
    public void GameReTry()
    {
        GameManager.instance.GameReTry();
    }
}
