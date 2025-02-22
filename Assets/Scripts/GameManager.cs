using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else { instance = this; }
    }

    public bool playing;

    private void Start()
    {
        PauseGame();
    }

    private void Update()
    {
        if (playing && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
            UIManager.Instance.TogglePauseMenu(true);
        }
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        playing = true;
    }

    public void PauseGame() 
    {
        playing = false;
        Time.timeScale = 0;
    }

    public void ExitGame() 
    {
        Application.Quit();
    }
}
