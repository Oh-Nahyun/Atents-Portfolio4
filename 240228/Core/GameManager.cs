using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    Player player;

    public Player Player
    {
        get
        {
            if (player == null)
                player = FindAnyObjectByType<Player>();
            return player;
        }
    }

    WorldManager worldManager;
    public WorldManager World => worldManager;

    protected override void OnPreInitialize()
    {
        base.OnPreInitialize();
        worldManager = GetComponent<WorldManager>();
    }

    protected override void OnInitialize()
    {
        player = FindAnyObjectByType<Player>();
    }

    /*
    bool isClear = false;
    public Action onGameClear;

    public void GameClear()
    {
        if (!isClear)
        {
            onGameClear?.Invoke();
            isClear = true;
        }
    }

    bool isOver = false;
    public Action onGameOver;

    public void GameOver()
    {
        if (!isOver)
        {
            onGameOver?.Invoke();
            isOver = true;
        }
    }
    */
}
