using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GolemMinigame : Minigame
{
    public override event EventHandler<MinigameScore> OnMinigameOver;

    private MinigameStep minigameStepInstance;


    public override bool StartMinigame()
    {
        Debug.Log("Start Minigame");

        //Get each minigame from the list of steps
        StartMinigameStep();

        return true;
    }

    public void StartMinigameStep()
    {
        //Get the first step 
        MinigameStep step = steps[0];
        steps.RemoveAt(0);

        //Instantiate the step
        minigameStepInstance = Instantiate(step);

        //Add event listener for this step
        minigameStepInstance.OnMinigameStepOver += OnMinigameStepOver;

        //Start each minigame step
        minigameStepInstance.StartMinigameStep();


        //Wait for score from minigame step

        //Start next minigame step 

        //After all steps, calculate final score

        //Invoke OnMinigameOver and pass this final score back
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        //End minigame
       // OnMinigameOver.Invoke(this, MinigameScore.Bronze);
        
    }

    private void OnMinigameStepOver(object sender, MinigameScore score)
    {
        minigameStepInstance.OnMinigameStepOver -= OnMinigameStepOver;
        Destroy(minigameStepInstance);

        //If there are more steps then get the next step and start it 
        if(steps.Count > 0)
        {
            StartMinigameStep();
        }
        else
        {
            //There are no more steps so end the minigame 
            Debug.Log("Minigame over");
            OnMinigameOver.Invoke(this, score);
        }
    }


}
