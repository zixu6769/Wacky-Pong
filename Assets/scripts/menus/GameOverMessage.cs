using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

/// <summary>
/// The game over message
/// </summary>
public class GameOverMessage : MonoBehaviour
{
    [SerializeField]
    Text messageText;

    /// <summary>
    /// Use this for initialization
    /// </summary>
    void Start()
    {
        Time.timeScale = 0;
        messageText = GameObject.FindGameObjectWithTag("WinnerText").GetComponent<Text>();
    }

    public void Hide()
    {
        Time.timeScale = 1;
        Destroy(gameObject);
        MenuManager.GoToMenu(MenuName.Main);
        AudioManager.Play(AudioClipName.Click);
    }

    public void SetWinner(ScreenSide winner)
    {
        if (winner == ScreenSide.Left)
        {
            messageText.text = "Game Over!\n\nLeft player won!";
        }
        else
        {
            messageText.text = "Game Over!\n\nRight player won!";
        }
    }
}
