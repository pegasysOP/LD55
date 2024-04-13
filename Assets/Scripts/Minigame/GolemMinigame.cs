using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GolemMinigame : Minigame
{
    public override event EventHandler<MinigameScore> OnMinigameOver;

    public override bool StartMinigame()
    {
        throw new System.NotImplementedException();

        MinigameStep step;

        //Get each minigame from the list of steps
        while(steps.Count > 0)
        {
            step = steps[0];
            steps.RemoveAt(0);

            //Start each minigame step
            step.StartMinigameStep();

            //Wait for score from minigame step

            //Start next minigame step 

            //After all steps, calculate final score

            //Invoke OnMinigameOver and pass this final score back
        }
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
            //End minigame
            OnMinigameOver.Invoke(this, MinigameScore.Bronze);
    }
}
