using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : AutoSingleton<GameManager>
{
    public static string FirstLoadedSceneName { get; private set; }
    private void OnEnable()
    {
        Debug.Log("GameManager OnEnable triggered");
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDisable()
    {
        Debug.Log("GameManager OnDisable triggered");
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        Debug.Log("GameManager OnSceneChanged triggered");
        FirstLoadedSceneName = newScene.name;
        BoardManager board = FindObjectOfType<BoardManager>();
        if (board != null)
        {
            Debug.Log("GameManager BoardManager.InitializeBoard now!");
            board.InitializeBoard();
        }
        else
        {
            Debug.LogWarning("BoardManager not found!");
        }
    }

}
