using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// The main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Goes to the difficulty menu
    /// </summary>
    public void GoToDifficultyMenu()
    {

    }

    /// <summary>
    /// Starts a two player game
    /// </summary>
    public void StartTwoPlayerGame()
    {
        MenuManager.GoToMenu(MenuName.Gameplay);
        AudioManager.Play(AudioClipName.Click);
    }

    /// <summary>
    /// Shows the help menu
    /// </summary>
    public void ShowHelp()
    {
        MenuManager.GoToMenu(MenuName.Help);
        AudioManager.Play(AudioClipName.Click);
    }

    /// <summary>
    /// Exits the game
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}
