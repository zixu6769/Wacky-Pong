using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Gameplay manager
/// </summary>
public class GameplayManager : MonoBehaviour
{
	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        EventManager.AddWinnerListener(Gameover);
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale > 0)
            {
                MenuManager.GoToMenu(MenuName.Pause);
            }
        }
    }
    void Gameover(ScreenSide side)
    {
        GameObject instance = Instantiate(Resources.Load("gameovermenu")) as GameObject;
        GameOverMessage game = instance.GetComponent<GameOverMessage>();
        game.SetWinner(side);
        AudioManager.Play(AudioClipName.GameOver);
    }
}
