using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Provides access to configuration data
/// </summary>
public static class ConfigurationUtils
{
    static ConfigurationData configurationData;
    #region Properties

    /// <summary>
    /// Gets the paddle move units per second
    /// </summary>
    public static float PaddleMoveUnitsPerSecond
    {
        get { return configurationData.PaddleMoveUnitsPerSecond; }
    }

    public static float BallImpulseForce
    {
        get { return configurationData.BallImpulseForce; }
    }

    public static float BallLifetime
    {
        get { return configurationData.BallLifetime; }
    }

    public static float BallWaittime
    {
        get { return configurationData.BallWaittime; }
    }

    public static int StandardBallScore
    {
        get { return configurationData.StandardBallScore; }
    }

    public static int StandardBallHit
    {
        get { return configurationData.StandardBallHit; }
    }

    public static int BonusBallScore
    {
        get { return configurationData.BonusBallScore; }
    }

    public static int BonusBallHit
    {
        get { return configurationData.BonusBallHit; }
    }

    public static int MaxSpawnTime
    {
        get { return configurationData.MaxSpawnTime; }
    }

    public static int MinSpawnTime
    {
        get { return configurationData.MinSpawnTime; }
    }

    public static double StandardBallProb
    {
        get { return configurationData.StandardBallProb; }
    }

    public static double BonusBallProb
    {
        get { return configurationData.BonusBallProb; }
    }

    public static double FreezerProb
    {
        get { return configurationData.FreezerProb; }
    }

    public static double SpeedupProb
    {
        get { return configurationData.SpeedupProb; }
    }

    public static int FreezeDuration
    {
        get { return configurationData.FreezeDuration; }
    }

    public static int SpeedupDuration
    {
        get { return configurationData.SpeedupDuration; }
    }

    public static int SpeedupFactor
    {
        get { return configurationData.SpeedupFactor; }
    }

    public static int WinningScore
    {
        get { return configurationData.WinningScore; }
    }
    #endregion

    public static void Initialize()
    {
        configurationData = new ConfigurationData();
    }
}
