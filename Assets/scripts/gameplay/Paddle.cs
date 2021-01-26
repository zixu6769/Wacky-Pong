using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Zijun Xu
/// This script defines the behavior of the paddles
/// </summary>
public class Paddle : MonoBehaviour
{
	private Rigidbody2D rb2D;                           //Rigidbody2d of the paddles
    private BoxCollider2D bc2D;                         //BoxCollider2d of the paddles
    [SerializeField]                                    
    ScreenSide side;                             //left,right paddles
    Vector2 newposition;                                //The new position that the paddles move to
    float half_height;                                  //Half of the height of the paddles
    float halfColliderWidth;                            //Half of the width of the paddles
    float BounceAngleHalfRange = 60 * Mathf.PI / 180;   //Bounce angle
    bool frozen = false;                        //Indicate whether the ball is frozen
    Timer frozenTimer;                                  //Timer for how long the ball will be frozen
    HitEvent hitEvent = new HitEvent();                 //Initialize hit count event

    void Start()
	{
        rb2D = GetComponent<Rigidbody2D>();
        bc2D = GetComponent<BoxCollider2D>();
        // Using x as height, since the paddle is rotated 90 degree
        // Using 1/4 as half of the height, since the paddle is scaled 1/2.
        half_height = bc2D.size.x/4;
        // Using y as width, since the paddle is rotated 90 degree
        // Using 1/4 as half of the width, since the paddle is scaled 1/2.
        halfColliderWidth = bc2D.size.y/4;
        // Save the defult position of the paddles
        newposition = transform.position;
        // Add timer for frozen duration
        frozenTimer = gameObject.AddComponent<Timer>();
        frozenTimer.AddTimerFinishedEventListener(Unfreeze);
        // Event handler support
        EventManager.AddHitInvoker(this);
        EventManager.AddFreezeListener(Freeze);
    }
	
    // This method will check the new position of paddles, force them to stay inside screen
    float CalculateCalmpedY(float y)
    {
        if ((y + half_height) >= ScreenUtils.ScreenTop) { y = ScreenUtils.ScreenTop - half_height; }
        if ((y - half_height) <= ScreenUtils.ScreenBottom) { y = ScreenUtils.ScreenBottom + half_height; }
        return y;
    }

    // This method checks whether the collision happeded on the front of the paddles
    bool Frontcollision(Collision2D coll)
    {
        if (side == ScreenSide.Right)
        {
            // Check if the collision happened on the top, bottom, or back
            if (coll.transform.position.x > transform.position.x
            || (transform.position.x - coll.transform.position.x) < halfColliderWidth)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        if (side == ScreenSide.Left)
        {
            // Check if the collision happened on the top, bottom, or back
            if (coll.transform.position.x < transform.position.x
            || (coll.transform.position.x - transform.position.x) < halfColliderWidth)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // Otherwise the collision must happened on the front
        else
        {
            return true;
        }
    }

    // This method is called 50 times per second
    private void FixedUpdate()
    {
        // "LeftPaddle" and "RightPaddle" are defined in input axis
        float verticalInput_left = Input.GetAxis("LeftPaddle");
        float verticalInput_right = Input.GetAxis("RightPaddle");
        // Move the left/right paddle only when it is not frozen
        if (verticalInput_left != 0 & side == ScreenSide.Left & frozen == false)
        {
            newposition.y += verticalInput_left * ConfigurationUtils.PaddleMoveUnitsPerSecond * Time.deltaTime;
        }
        if (verticalInput_right != 0 & side == ScreenSide.Right & frozen == false)
        {
            newposition.y += verticalInput_right * ConfigurationUtils.PaddleMoveUnitsPerSecond * Time.deltaTime;
        }
        // Make sure the paddles stay inside the screen
        newposition.y = CalculateCalmpedY(newposition.y);
        // Move the paddles to the new position
        rb2D.MovePosition(newposition);
    }

    /// <summary>
    /// Detects collision with a ball to aim the ball
    /// </summary>
    /// <param name="coll">collision info</param>

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball") && Frontcollision(coll))
        {
            AudioManager.Play(AudioClipName.PaddleCollide);
            // HUD.AddHits()
            hitEvent.Invoke(side, coll.gameObject.GetComponent<Ball>().Hits);
            // calculate new ball direction
            float ballOffsetFromPaddleCenter =
                coll.transform.position.y - transform.position.y;
            float normalizedBallOffset = ballOffsetFromPaddleCenter /
                half_height;
            float angleOffset = normalizedBallOffset * BounceAngleHalfRange;
            // angle modification is based on screen side
            float angle;
            if (side == ScreenSide.Left)
            {
                angle = angleOffset;
            }
            else
            {
                angle = (float)(Mathf.PI - angleOffset);
            }
            Vector2 direction = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
            // tell ball to set direction to new direction
            Ball ballScript = coll.gameObject.GetComponent<Ball>();
            ballScript.SetDirection(direction);
        }
    }

    // freeze the paddle
    private void Freeze(ScreenSide freezeside, float second)
    {
        if (freezeside == side)
        {
            frozen = true;
            // Add time to the timer if it's already running
            if (frozenTimer.Running)
            {
                frozenTimer.AddTime = second;
            }
            // Set time to the timer if it's not running
            else
            {
                AudioManager.Play(AudioClipName.Freeze);
                frozenTimer.Duration = second;
                frozenTimer.Run();
            }
        }
    }

    // unfreeze the paddle
    void Unfreeze()
    {
        AudioManager.Play(AudioClipName.Freeze);
        frozen = false;
        frozenTimer.Stop();
    }

    // Listener for hit count
    public void AddHitsEventListener(UnityAction<ScreenSide, int> listener)
    {
        hitEvent.AddListener(listener);
    }
}
