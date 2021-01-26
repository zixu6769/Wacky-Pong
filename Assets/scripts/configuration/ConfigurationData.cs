using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// A container for the configuration data
/// </summary>
public class ConfigurationData
{
    #region Fields

    const string ConfigurationDataFileName = "ConfigurationData.csv";

    // configuration data set to default values

    float paddleMoveUnitsPerSecond = 10;
    float ballImpulseForce = 5;
    float ballLifetime = 10;
    float ballWaittime = 1;
    // Scores/Hits
    int standardBallScore = 1;
    int standardBallHit = 1;
    int bonusBallScore = 2;
    int bonusBallHit = 2;
    // Spwan
    int maxSpawnTime = 10;
    int minSpawnTime = 5;
    double standardBallProb = 0.6;
    double bonusBallProb = 0.2;
    double freezerProb = 0.1;
    double speedupProb = 0.1;
    // Effect
    int freezeDuration = 2;
    int speedupDuration = 2;
    int speedupFactor = 2;
    int winningScore = 5;

    #endregion

    #region Properties

    public float PaddleMoveUnitsPerSecond
    {
        get { return paddleMoveUnitsPerSecond; }
    }

    public float BallImpulseForce
    {
        get { return ballImpulseForce; }    
    }

    public float BallLifetime
    {
        get { return ballLifetime; }
    }

    public float BallWaittime
    {
        get { return ballWaittime; }
    }

    public int StandardBallScore
    {
        get { return standardBallScore; }
    }

    public int StandardBallHit
    {
        get { return standardBallHit; }
    }

    public int BonusBallScore
    {
        get { return bonusBallScore; }
    }

    public int BonusBallHit
    {
        get { return bonusBallHit; }
    }

    public int MaxSpawnTime
    {
        get { return maxSpawnTime; }
    }

    public int MinSpawnTime
    {
        get { return minSpawnTime; }
    }

    public double StandardBallProb
    {
        get { return standardBallProb; }
    }

    public double BonusBallProb
    {
        get { return bonusBallProb; }
    }

    public double FreezerProb
    {
        get { return freezerProb; }
    }

    public double SpeedupProb
    {
        get { return speedupProb; }
    }

    public int FreezeDuration
    {
        get { return freezeDuration; }
    }

    public int SpeedupDuration
    {
        get { return speedupDuration; }
    }

    public int SpeedupFactor
    {
        get { return speedupFactor; }
    }

    public int WinningScore
    {
        get { return winningScore; }
    }
    #endregion

    #region Constructor

    /// <summary>
    /// Constructor
    /// Reads configuration data from a file. If the file
    /// read fails, the object contains default values for
    /// the configuration data
    /// </summary>
    public ConfigurationData()
    {
        StreamReader input = null;
        try
        {
            input = File.OpenText(Path.Combine(Application.streamingAssetsPath, ConfigurationDataFileName));
            string names = input.ReadLine();
            string values = input.ReadLine();
            SetConfigurationDataFields(values);
        }
        catch (Exception e)
        {
        }
        finally
        {
            if (input != null)
            {
                input.Close();
            }
        }
    }

    void SetConfigurationDataFields(string csvValues)
    {
        string[] values = csvValues.Split(',');
        paddleMoveUnitsPerSecond = float.Parse(values[0]);
        ballImpulseForce = float.Parse(values[1]);
        ballLifetime = float.Parse(values[2]);
        ballWaittime = float.Parse(values[3]);
        standardBallScore = int.Parse(values[4]);
        standardBallHit = int.Parse(values[5]);
        bonusBallScore = int.Parse(values[6]); 
        bonusBallHit = int.Parse(values[7]); 
        maxSpawnTime = int.Parse(values[8]); 
        minSpawnTime = int.Parse(values[9]);
        standardBallProb = double.Parse(values[10]);
        bonusBallProb = double.Parse(values[11]);
        freezerProb = double.Parse(values[12]);
        speedupProb = double.Parse(values[13]);
        freezeDuration = int.Parse(values[14]);
        speedupDuration = int.Parse(values[15]);
        speedupFactor = int.Parse(values[16]);
        winningScore = int.Parse(values[17]);
    }

    #endregion
}
