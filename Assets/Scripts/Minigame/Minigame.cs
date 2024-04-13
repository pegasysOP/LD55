using System;
using System.Collections.Generic;
using UnityEngine;

public enum MinigameScore
{
    None,
    Bronze,
    Silver,
    Gold,
    Jade
}

public abstract class Minigame : MonoBehaviour
{
    [SerializeField] private List<MinigameStep> steps;

    private MinigameStep minigameStepInstance;

    /// <summary>
    /// Thrown when the minigame is over, gives the score achieved
    /// </summary>
    public event EventHandler<MinigameScore> OnMinigameOver;

    /// <summary>
    /// Starts the minigame
    /// </summary>
    /// <returns>Returns true if started successfully</returns>
    public virtual bool StartMinigame()
    {
        //Get each minigame from the list of steps
        return StartMinigameStep();
    }

    protected virtual bool StartMinigameStep()
    {
        if (steps == null || steps.Count == 0)
            return false;

        //Get the next step 
        MinigameStep step = steps[0];
        steps.RemoveAt(0);

        minigameStepInstance = Instantiate(step, transform);
        minigameStepInstance.OnMinigameStepOver += OnMinigameStepOver;
        minigameStepInstance.StartMinigameStep();

        return true;
    }

    protected virtual void OnMinigameStepOver(object sender, MinigameScore score)
    {
        minigameStepInstance.OnMinigameStepOver -= OnMinigameStepOver;
        Destroy(minigameStepInstance.gameObject);

        //If there are more steps then get the next step and start it 
        if (steps.Count > 0)
            StartMinigameStep();
        else
            OnMinigameOver.Invoke(this, score);
    }
}
