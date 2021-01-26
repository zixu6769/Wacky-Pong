using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A ball spawner
/// </summary>
public class BallSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject StandardBall;
    [SerializeField]
    GameObject BonusBall;
    [SerializeField]
    GameObject Freezer;
    [SerializeField]
    GameObject Speedup;
    Timer randomTimer;              // Random Timer between 5-10 sec
    private BoxCollider2D bc2D;     // BoxCollider2d of the ball
    float collider_length;          // Length of the ball collider
    float collider_height;          // Height of the ball collider
    Vector2 upperleft;              // Vec2 for upperleft connor of the collider
    Vector2 lowerright;             // Vec2 for lowerright connor of the collider  
    bool retrySpawn = true;        // Pending spawn              
	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start()
	{
        // Create a temp ball for measureing its size
        GameObject tempBall = Instantiate(StandardBall) as GameObject;
        // Connect the boxCollider component
        bc2D = tempBall.GetComponent<BoxCollider2D>();
        // Get the length and height of the collider
        collider_length = bc2D.size.x;
        collider_height = bc2D.size.y;
        // Assgin value to the diagonal corners
        // divide 2 to get half length/width, divide 10 because the ball is scaled
        upperleft = new Vector2(-collider_length/20, collider_height/20);
        lowerright = new Vector2(collider_length/20, -collider_height/20);
        // Create and run the random timer
        randomTimer = gameObject.AddComponent<Timer>();
        randomTimer.Duration = RandomTimerDecider();
        randomTimer.Run();
        // Destroy the temp ball
        Destroy(tempBall);
        // Add listeners for events
        EventManager.AddBallLostListener(DrawBall);
        EventManager.AddBallDeadListener(DrawBall);
    }
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update()
	{
		if (randomTimer.Finished)
        {
            retrySpawn = false;
            DrawBall();
            randomTimer.Duration = RandomTimerDecider();
            randomTimer.Run();
        }

        if (retrySpawn)
        {
            DrawBall();
        }
    }

    // This method check the overlaping area and return a boolean
    // indicates whether the drawBall() need to wait
    bool SetPending()
    {
        if (Physics2D.OverlapArea(upperleft, lowerright) == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // Draw a ball at the center
    private void DrawBall()
    {
        double ballDecider;
        ballDecider = Random.Range(0.0f, 1.0f);
        if (SetPending() == true)
        {
            retrySpawn = true;
        }
        else
        {
            retrySpawn = false;
            AudioManager.Play(AudioClipName.BallSpawn);
            if (ballDecider < ConfigurationUtils.StandardBallProb)
            {
                Instantiate<GameObject>(StandardBall, new Vector2(0, 0), Quaternion.identity);
            }
            else if (ballDecider < (ConfigurationUtils.StandardBallProb + ConfigurationUtils.BonusBallProb))
            {
                Instantiate<GameObject>(BonusBall, new Vector2(0, 0), Quaternion.identity);
            }
            else if (ballDecider < (ConfigurationUtils.StandardBallProb + ConfigurationUtils.BonusBallProb + ConfigurationUtils.FreezerProb))
            {
                Instantiate<GameObject>(Freezer, new Vector2(0, 0), Quaternion.identity);
            }
            else
            {
                Instantiate<GameObject>(Speedup, new Vector2(0, 0), Quaternion.identity);
            }
        }
    }

    // This method randomly sign 5-10 sec to the randomTimer
    private int RandomTimerDecider()
    {
        return Random.Range(ConfigurationUtils.MinSpawnTime, ConfigurationUtils.MaxSpawnTime);
    }
}
