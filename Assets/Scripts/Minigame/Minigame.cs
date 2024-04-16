using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class Minigame : MonoBehaviour
{
    [SerializeField] protected List<MinigameStep> steps;

    [SerializeField] protected SpriteRenderer medalSprite;
    [SerializeField] protected Sprite failSprite;
    [SerializeField] protected Sprite bronzeSprite;
    [SerializeField] protected Sprite silverSprite;
    [SerializeField] protected Sprite goldSprite;
    [SerializeField] protected Sprite jadeSprite;

    protected List<MedalType> medals;

    protected MinigameStep minigameStepInstance;

    /// <summary>
    /// Thrown when the minigame is over, gives the score achieved
    /// </summary>
    public virtual event EventHandler<MedalType> OnMinigameOver;

    /// <summary>
    /// Starts the minigame
    /// </summary>
    /// <returns>Returns true if started successfully</returns>
    public virtual bool StartMinigame()
    {
        if(medals == null)
        {
            medals = new List<MedalType>();
        }
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

    protected virtual void OnMinigameStepOver(object sender, MedalType score)
    {
        minigameStepInstance.OnMinigameStepOver -= OnMinigameStepOver;
        Destroy(minigameStepInstance.gameObject);

        //If there are more steps then get the next step and start it 
        if (steps.Count > 0)
            StartMinigameStep();
        else
            OnMinigameOver.Invoke(this, score);
    }

    protected Sprite GetMedalSpriteFromScore(MedalType score)
    {
        switch(score)
        {
            case MedalType.Jade:
                return jadeSprite;
            case MedalType.Gold:
                return goldSprite;
            case MedalType.Silver:
                return silverSprite;
            case MedalType.Bronze:
                return bronzeSprite;
            default:
                return failSprite;
        }
    }
}
