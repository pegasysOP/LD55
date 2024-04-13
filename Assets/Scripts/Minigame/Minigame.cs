using System;
using UnityEngine;

public enum MinigameScore
{
    None,
    Bronze,
    Silver,
    Gold,
    Platinum
}

public abstract class Minigame : MonoBehaviour
{
    public Timer timer;

    /// <summary>
    /// Thrown when the minigame is over, gives the score achieved
    /// </summary>
    public abstract event EventHandler<MinigameScore> OnMinigameOver;

    /// <summary>
    /// Starts the minigame
    /// </summary>
    /// <returns>Returns true if started successfully</returns>
    public abstract bool StartMinigame();
}
