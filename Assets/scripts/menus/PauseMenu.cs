using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// </summary>
public class PauseMenu : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 0;
    }
    /// <summary>
    /// Resumes the paused game
    /// </summary>
    public void ResumeGame()
    {
        // unpause the game and destroy menu
        Time.timeScale = 1;
        Destroy(gameObject);
        AudioManager.Play(AudioClipName.Click);
    }

    /// <summary>
    /// Quits the paused game
    /// </summary>
    public void QuitGame()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
        AudioManager.Play(AudioClipName.Click);
    }
}
