using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : AutoSingleton<GameManager>
{
    public static SCENE_NAME LOADED_SCENE { get; private set; }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("GameManager: Saving game (K key pressed)...");
            SaveManager.SaveGame();
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            Debug.Log("GameManager: Loading game (L key pressed)...");
            SaveManager.LoadGame();
        }
    }


    private void OnEnable()
    {
        Debug.Log("GameManager: OnEnable()");
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnDisable()
    {
        Debug.Log("GameManager: OnDisable()");
        SceneManager.activeSceneChanged -= OnSceneChanged;
    }
    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        // Try to parse to SCENE_NAME Enum (case-insensitive)
        if (Enum.TryParse(newScene.name, ignoreCase: true, out SCENE_NAME sceneEnum))
        {
            LOADED_SCENE = sceneEnum;
        }
        else
        {
            LOADED_SCENE = SCENE_NAME.UNKNOWN;
        }

        switch (LOADED_SCENE)
        {
            case SCENE_NAME.MAINMENU:
                Debug.Log("GameManager: Scene is MAINMENU!");
                // TODO: Add MainMenu specific logic here
                break;
            case SCENE_NAME.BATTLE:
                Debug.Log("GameManager: Scene is BATTLE!");
                BoardManager board = FindObjectOfType<BoardManager>();
                if (board != null)
                {
                    Debug.Log("GameManager: BoardManager.InitializeBoard() now!");
                    board.InitializeBoard();
                }
                else
                {
                    Debug.LogWarning("GameManager: BoardManager not found!");
                }
                break;
            case SCENE_NAME.UNKNOWN:
            default:
                Debug.LogError("GameManager: Scene is UNKNOWN or not handled!");
                break;
        }
    }
    // --- ENUMS ---
    public enum OWNER
    {
        NONE,
        PLAYER,
        NPC
    }
    public enum SCENE_NAME
    {
        UNKNOWN,
        MAINMENU,
        BATTLE
    }
}
