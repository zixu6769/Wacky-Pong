using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    // Invoker support
    static Ball ScoreInvoker;
    static Paddle HitInvoker;
    static Ball BallLostinvoker;
    static Ball BallDeadinvoker;
    // Listener support
    static UnityAction<ScreenSide, int> ScoreListener;
    static UnityAction<ScreenSide, int> HitListener;
    static UnityAction BallLostlistener;
    static UnityAction BallDeadlistener;
    // Freezer support
    static List<Pickup> Freezeinvoker = new List<Pickup>();
    static List<UnityAction<ScreenSide, float>> Freezelistener =
        new List<UnityAction<ScreenSide, float>>();
    // Speedup support
    static List<Pickup> Speedupinvoker = new List<Pickup>();
    static List<UnityAction<float, float>> Speeduplistener =
        new List<UnityAction<float, float>>();
    // Winner support
    static List<HUD> Winnerinvoker = new List<HUD>();
    static List<UnityAction<ScreenSide>> Winnerlistener =
        new List<UnityAction<ScreenSide>>();

    // Score board
    public static void AddScoreInvoker(Ball script)
    {
        ScoreInvoker = script;
        if(ScoreListener != null)
        {
            ScoreInvoker.AddScoreEventListener(ScoreListener);
        }
    }
    public static void AddScoreListener(UnityAction<ScreenSide, int> handler)
    {
        ScoreListener = handler;
        if (ScoreInvoker != null)
        {
            ScoreInvoker.AddScoreEventListener(ScoreListener);
        }
    }

    // Hits count
    public static void AddHitInvoker(Paddle script)
    {
        HitInvoker = script;
        if (HitListener != null)
        {
            HitInvoker.AddHitsEventListener(HitListener);
        }
    }
    public static void AddHitListener(UnityAction<ScreenSide, int> handler)
    {
        HitListener = handler;
        if (HitInvoker != null)
        {
            HitInvoker.AddHitsEventListener(HitListener);
        }
    }

    // Ball gone invisiable
    public static void AddBallLostInvoker(Ball script)
    {
        BallLostinvoker = script;
        if (BallLostlistener != null)
        {
            BallLostinvoker.AddBallLostEventListener(BallLostlistener);
        }
    }
    public static void AddBallLostListener(UnityAction handler)
    {
        BallLostlistener = handler;
        if (BallLostinvoker != null)
        {
            BallLostinvoker.AddBallLostEventListener(BallLostlistener);
        }
    }

    // Ball lasts long enough
    public static void AddBallDeadInvoker(Ball script)
    {
        BallDeadinvoker = script;
        if (BallDeadlistener != null)
        {
            BallDeadinvoker.AddBallDeadEventListener(BallDeadlistener);
        }
    }
    public static void AddBallDeadListener(UnityAction handler)
    {
        BallDeadlistener = handler;
        if (BallDeadinvoker != null)
        {
            BallDeadinvoker.AddBallDeadEventListener(BallDeadlistener);
        }
    }

    // Freeze
    public static void AddFreezeInvoker(Pickup invoker)
    {
        Freezeinvoker.Add(invoker);
        foreach (UnityAction<ScreenSide, float> listener in Freezelistener)
        {
            invoker.AddFreezeEventListener(listener);
        }
    }
    public static void AddFreezeListener(UnityAction<ScreenSide, float> listener)
    {
        Freezelistener.Add(listener);
        foreach (Pickup invoker in Freezeinvoker)
        {
            invoker.AddFreezeEventListener(listener);
        }
    }

    // Speedup
    public static void AddSpeedupInvoker(Pickup invoker)
    {
        Speedupinvoker.Add(invoker);
        foreach (UnityAction<float, float> listener in Speeduplistener)
        {
            invoker.AddSpeedupEventListener(listener);
        }
    }
    public static void AddSpeedupListener(UnityAction<float, float> listener)
    {
        Speeduplistener.Add(listener);
        foreach (Pickup invoker in Speedupinvoker)
        {
            invoker.AddSpeedupEventListener(listener);
        }
    }


    // Winner
    public static void AddWinnerInvoker(HUD invoker)
    {
        Winnerinvoker.Add(invoker);
        foreach (UnityAction<ScreenSide> listener in Winnerlistener)
        {
            invoker.AddWinnerEventListener(listener);
        }
    }
    public static void AddWinnerListener(UnityAction<ScreenSide> listener)
    {
        Winnerlistener.Add(listener);
        foreach (HUD invoker in Winnerinvoker)
        {
            invoker.AddWinnerEventListener(listener);
        }
    }
}
