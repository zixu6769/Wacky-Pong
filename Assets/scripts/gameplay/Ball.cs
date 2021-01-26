using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This class defines the behavior of the bouncing ball
public class Ball : MonoBehaviour
{
    private Rigidbody2D rb2D;                                   // Rigidbody2d of the ball
    private BoxCollider2D bc2D;                                 // BoxCollider2d of the ball
    Timer deathTimer;                                           // Ball live duration timer
    Timer waitTimer;                                            // Ball wait duration timer
    Timer speedupTimer;                                         // Ball speedup duration timer
    float currentSpeedupFactor;                                 // Track the current speed factor
    bool playSpeedupSound = false;                              // avoid playing speedup sound if already sped up
    int hits;                                                   // Hit for each collision
    int points;                                                 // Point for each lost ball
    public enum BallType {StandardBall, BonusBall, Freezer, Speedup};   
    public BallType currentBall;                                // Current ball type
    ScoreEvent scoreEvent = new ScoreEvent();                   // Initialize score event handler
    BallLostEvent ballLostEvent = new BallLostEvent();          // Initialize lost ball event
    BallDeadEvent ballDeadEvent = new BallDeadEvent();          // Initialize dead ball event
    public virtual void Start()
	{
        // Connet rigidbody2d to the ball
        rb2D = GetComponent<Rigidbody2D>();
        // Sign score based on BallType
        if (currentBall == BallType.StandardBall)
        {
            hits = ConfigurationUtils.StandardBallHit;
            points = ConfigurationUtils.StandardBallScore;
        }
        else if (currentBall == BallType.BonusBall)
        {
            hits = ConfigurationUtils.BonusBallHit;
            points = ConfigurationUtils.BonusBallScore;
        }
        else
        {
            hits = 0;
            points = 0; 
        }
        // Create and start timers
        deathTimer = gameObject.AddComponent<Timer>();
        deathTimer.Duration = ConfigurationUtils.BallLifetime;
        deathTimer.Run();
        deathTimer.AddTimerFinishedEventListener(RedrawBall);

        waitTimer = gameObject.AddComponent<Timer>();
        waitTimer.Duration = ConfigurationUtils.BallWaittime;
        waitTimer.Run();
        waitTimer.AddTimerFinishedEventListener(StartMoving);

        speedupTimer = gameObject.AddComponent<Timer>();
        speedupTimer.AddTimerFinishedEventListener(SlowdownBalls);

        // Connect the invokers 
        EventManager.AddScoreInvoker(this);
        EventManager.AddBallLostInvoker(this);
        EventManager.AddBallDeadInvoker(this);
        EventManager.AddSpeedupListener(SpeedupBalls);
    }

    // This method return the standard ball score
    public int Hits
    {
        get { return hits; }
    }

    // This method converts degree to radians
    float DegreeToRadians(int angle)
    {
        float result = angle * Mathf.PI / 180;
        return result;
    }

    // This method checks whether the ball is inside the screen
    bool InsideScreen()
    {
        if (gameObject.transform.position.x > ScreenUtils.ScreenRight
        || gameObject.transform.position.x < ScreenUtils.ScreenLeft
        || gameObject.transform.position.y > ScreenUtils.ScreenTop
        || gameObject.transform.position.y < ScreenUtils.ScreenBottom)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    // redraw the ball after 10 secs
    public void RedrawBall()
    {
        // BallSpawner.DrawBall()
        ballDeadEvent.Invoke();
        Destroy(gameObject);
    }

    void SpeedupBalls(float factor, float duration)
    {
        if (playSpeedupSound)
        {
            AudioManager.Play(AudioClipName.Speedup);
        }
        playSpeedupSound = false;
        currentSpeedupFactor = factor;
        rb2D.velocity *= factor;
        if (speedupTimer.Running)
        {
            speedupTimer.AddTime = duration;
        }
        else
        {
            speedupTimer.Duration = duration;
            speedupTimer.Run();
        }
    }

    void SlowdownBalls()
    {
        AudioManager.Play(AudioClipName.Speedup);
        rb2D.velocity /= currentSpeedupFactor;
        playSpeedupSound = true;
        speedupTimer.Stop();
    }

    void StartMoving()
    {
        // Randomly sign 1.0 or 0.0
        float left_right = Random.value;
        // Angle in degree
        float angle;
        // The ball goes to right between (-45,45) degree
        if (left_right > 0.5)
        {
            angle = Random.Range(DegreeToRadians(45), DegreeToRadians(-45));
        }
        // The ball goes to left between (135,225) degree
        else
        {
            angle = Random.Range(DegreeToRadians(135), DegreeToRadians(225));
        }
        Vector2 force = new Vector2(
            (float)Mathf.Cos(angle) * ConfigurationUtils.BallImpulseForce,
            (float)Mathf.Sin(angle) * ConfigurationUtils.BallImpulseForce);

        // adjust as necessary if speedup effect is active
        if (EffectUtils.SpeedupActivated)
        {
            SpeedupBalls(EffectUtils.SpeedupFactor,
                EffectUtils.SpeedupEffectSecondsLeft);
            force *= currentSpeedupFactor;
        }
        else
        {
            playSpeedupSound = true;
        }
        rb2D.AddForce(force, ForceMode2D.Impulse);
        waitTimer.Stop();
    }

    // gives a new velocity to the ball based on where it hit
    public void SetDirection(Vector2 direction)
    {
        // current velocity
        Vector2 velocity = rb2D.velocity;
        // Calculate current speed with sqrt(x^2 + y^2)
        float speed = Mathf.Sqrt(Mathf.Pow(velocity.x, 2) + Mathf.Pow(velocity.y, 2));
        rb2D.velocity = speed * direction;
    }

    // Destroy the ball when it left the screen
    void OnBecameInvisible()
    {
        // Make sure the ball is outside of the screen before redraw it
        if (!deathTimer.Finished && !InsideScreen())
        {
            if (gameObject.transform.position.x < 0)
            {
                // Add score to the right player
                scoreEvent.Invoke(ScreenSide.Right, points);
                // BallSpawner.DrawBall()
                ballLostEvent.Invoke();
            }
            // If the ball goes out on the right side of the screen
            else
            {
                // Add score to the left player
                scoreEvent.Invoke(ScreenSide.Left, points);
                ballLostEvent.Invoke();
            }
            AudioManager.Play(AudioClipName.BallLost);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Ball"))
        {
            AudioManager.Play(AudioClipName.BallCollide);
        }
    }

    // Add listeners
    public void AddScoreEventListener(UnityAction<ScreenSide,int> listener)
    {
        scoreEvent.AddListener(listener);
    }
    public void AddBallLostEventListener(UnityAction listener)
    {
        ballLostEvent.AddListener(listener);
    }
    public void AddBallDeadEventListener(UnityAction listener)
    {
        ballDeadEvent.AddListener(listener);
    }
}

