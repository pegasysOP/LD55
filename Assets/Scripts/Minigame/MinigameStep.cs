using System;
using UnityEngine;

public abstract class MinigameStep : MonoBehaviour
{
    public Timer timer;
    public MedalType score;

    /// <summary>
    /// Thrown when the minigame is over, gives the score achieved
    /// </summary>
    public abstract event EventHandler<MedalType> OnMinigameStepOver;

    /// <summary>
    /// Starts the minigame
    /// </summary>
    /// <returns>Returns true if started successfully</returns>
    public abstract bool StartMinigameStep();
}