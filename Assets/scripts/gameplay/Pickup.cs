using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// This class defines the behavior of freezer ball
public class Pickup : Ball
{
    // Freeze duration in seconds
    float FreezeSec;
    // Speedup duration in seconds
    float SpeedupSec;
    // Speedup factor
    float SpeedupFac;
    // Initialize event for freezer
    FreezeEvent freezeEvent = new FreezeEvent();
    // Initialize event for speedup
    SpeedupEvent speedupEvent = new SpeedupEvent();


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (currentBall == BallType.Freezer)
        {
            FreezeSec = ConfigurationUtils.FreezeDuration;
            EventManager.AddFreezeInvoker(this);
        }
        if (currentBall == BallType.Speedup)
        {
            SpeedupSec = ConfigurationUtils.SpeedupDuration;
            SpeedupFac = ConfigurationUtils.SpeedupFactor;
            EventManager.AddSpeedupInvoker(this);
        }
    }

    // This method define the functionality of freezer balls
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("LeftPaddle") ||
            collision.gameObject.CompareTag("RightPaddle"))
        {
            if (currentBall == BallType.Freezer)
            {
                // If picked up by right paddle freeze left paddle
                if (collision.gameObject.CompareTag("LeftPaddle"))
                {
                    // Call freeze method in paddle
                    freezeEvent.Invoke(ScreenSide.Right, FreezeSec);
                    // Ball.RedrawBall()
                    RedrawBall();
                }
                // If picked up by Left paddle, freeze right paddle
                else if (collision.gameObject.CompareTag("RightPaddle"))
                {
                    freezeEvent.Invoke(ScreenSide.Left, FreezeSec);
                    RedrawBall();
                }
            }
            if (currentBall == BallType.Speedup)
            {
                speedupEvent.Invoke(SpeedupFac,SpeedupSec);
                RedrawBall();    
            }
        }
    }


    public void AddFreezeEventListener(UnityAction<ScreenSide,float> listener)
    {
        freezeEvent.AddListener(listener);
    }
    public void AddSpeedupEventListener(UnityAction<float,float> listener)
    {
        speedupEvent.AddListener(listener);
    }
}
