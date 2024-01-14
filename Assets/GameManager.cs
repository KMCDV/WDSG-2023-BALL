using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event EventHandler<LevelLoadRequestedEventArgs> LevelLoadRequested; 
    public static void RequestLevelLoad(string levelName) => LevelLoadRequested?.Invoke(null, new LevelLoadRequestedEventArgs(levelName));

    public static event EventHandler MainMenuLoadRequested;
    public static void RequestMainMenuLoad() => MainMenuLoadRequested?.Invoke(null, EventArgs.Empty);

    public string MainMenuSceneName;
    public string CurrentLevelName => SceneManager.GetActiveScene().name;
    public static GameManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            RequestMainMenuLoad();
        }
    }

    private void OnEnable()
    {
        LevelLoadRequested += OnLevelLoadRequested;
        MainMenuLoadRequested += LoadMainMenu;
    }
    
    private void OnDisable()
    {
        LevelLoadRequested -= OnLevelLoadRequested;
        MainMenuLoadRequested -= LoadMainMenu;
    }
    
    private void OnLevelLoadRequested(object sender, LevelLoadRequestedEventArgs e)
    {
        SceneManager.LoadScene(e.LevelName);
    }
    
    private void LoadMainMenu(object p_sender, EventArgs p_eventArgs)
    {
        SceneManager.LoadScene(MainMenuSceneName);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}

public class LevelLoadRequestedEventArgs : EventArgs
{
    public string LevelName { get; private set; }

    public LevelLoadRequestedEventArgs(string levelName)
    {
        LevelName = levelName;
    }
}
