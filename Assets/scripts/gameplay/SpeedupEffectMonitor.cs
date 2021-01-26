using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedupEffectMonitor : MonoBehaviour
{
    float speedupFactor = 1;
    Timer speedupMonitorTimer;

    // Start is called before the first frame update
    void Start()
    {
        speedupMonitorTimer = gameObject.AddComponent<Timer>();
        speedupMonitorTimer.AddTimerFinishedEventListener(EndSpeedupMonitor);
        EventManager.AddSpeedupListener(HandleSpeedupEffectActivatedEvent);
    }

    /// Handles the speedup effect activated event
    void HandleSpeedupEffectActivatedEvent(float factor, float duration)
    {
        if (speedupMonitorTimer.Running)
        {
            speedupMonitorTimer.AddTime = duration;
        }
        else
        {
            speedupFactor = factor;
            speedupMonitorTimer.Duration = duration;
            speedupMonitorTimer.Run();
        }
    }

    void EndSpeedupMonitor()
    {
        speedupMonitorTimer.Stop();
        speedupFactor = 1;
    }

    public bool SpeedupActivated
    {
        get { return speedupMonitorTimer.Running; }
    }
    public float SpeedupTimeLeft
    {
        get { return speedupMonitorTimer.SecondsLeft; }
    }
    public float SpeedupFactor
    {
        get { return speedupFactor; }
    }
}
