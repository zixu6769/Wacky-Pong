using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EffectUtils
{
    // Gets the SpeedupEffectMonitor attached to the main camera
    static SpeedupEffectMonitor GetSpeedupEffectMonitor()
    {
        return Camera.main.GetComponent<SpeedupEffectMonitor>();
    }

    // Gets whether or not the speedup effect is active
    public static bool SpeedupActivated
    {
        get { return GetSpeedupEffectMonitor().SpeedupActivated;  }
    }

    // Gets how many seconds are left in the speedup effect
    public static float SpeedupEffectSecondsLeft
    {
        get { return GetSpeedupEffectMonitor().SpeedupTimeLeft; }
    }

    // Gets the speedup factor for the speedup effect
    public static float SpeedupFactor
    {
        get { return GetSpeedupEffectMonitor().SpeedupFactor; }
    }

}
