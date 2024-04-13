using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnaceMinigameStep : MinigameStep
{
    public override event EventHandler<MinigameScore> OnMinigameStepOver;

    public override bool StartMinigameStep()
    {
        Debug.Log("Furnace Stoke Minigame step started");
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Furnace Stoke Minigame step complete");
            OnMinigameStepOver.Invoke(this, MinigameScore.Silver);
        }
    }
}
