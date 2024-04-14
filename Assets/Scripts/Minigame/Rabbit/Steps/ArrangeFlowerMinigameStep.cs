using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrangeFlowerMinigameStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;

    public override bool StartMinigameStep()
    {
        Debug.Log("Arrange flower minigame started");
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
            Debug.Log("Arrange flower Minigame step complete");
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }
    }
}
