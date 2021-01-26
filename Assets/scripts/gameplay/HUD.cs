using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class HUD : MonoBehaviour
{
    [SerializeField]
    GameObject scoreTextGameObject;
    [SerializeField]
    GameObject leftHitsTextGameObject;
    [SerializeField]
    GameObject rightHitsTextGameObject;
    int leftHit = 0;
    int rightHit = 0;
    int leftScore = 0;
    int rightScore = 0;
    Text leftHitText;
    Text rightHitText;
    Text scoreText;
    const string HitsPrefix = "Hits: ";
    const string ScorePrefix = " : ";
    WinnerEvent winnerEvent = new WinnerEvent();
    // Start is called before the first frame update
    void Start()
    {
        leftHit = rightHit = leftScore = rightScore = 0;
        leftHitText = GameObject.FindGameObjectWithTag("LeftHit").GetComponent<Text>();
        rightHitText = GameObject.FindGameObjectWithTag("RightHit").GetComponent<Text>();
        scoreText = GameObject.FindGameObjectWithTag("Score").GetComponent<Text>();
        EventManager.AddScoreListener(AddPoints);
        EventManager.AddHitListener(AddHits);
        EventManager.AddWinnerInvoker(this);
    }


    /// <summary>
    /// Adds the given points to the given side
    /// </summary>
    /// <param name="side">screen side</param>
    /// <param name="points">points to add</param>
    void AddPoints(ScreenSide side, int points)
    {
        // add points and change text
        if (side == ScreenSide.Left)
        {
            leftScore += points;
            if (leftScore > ConfigurationUtils.WinningScore)
            {
                winnerEvent.Invoke(ScreenSide.Left);
            }
        }
        else
        {
            rightScore += points;
            if (rightScore > ConfigurationUtils.WinningScore)
            {
                winnerEvent.Invoke(ScreenSide.Right);
            }
        }
        scoreText.text = leftScore + ScorePrefix + rightScore;
    }

    /// <summary>
    /// Adds the given hits to the given side
    /// </summary>
    /// <param name="side">screen side</param>
    /// <param name="hits">hits to add</param>
    void AddHits(ScreenSide side, int hits)
    {
        // add hits and change text
        if (side == ScreenSide.Left)
        {
            leftHit += hits;
            leftHitText.text = HitsPrefix + leftHit;
        }
        else
        {
            rightHit += hits;
            rightHitText.text = HitsPrefix + rightHit;
        }
    }

    public void AddWinnerEventListener(UnityAction<ScreenSide> listener)
    {
        winnerEvent.AddListener(listener);
    }

}
