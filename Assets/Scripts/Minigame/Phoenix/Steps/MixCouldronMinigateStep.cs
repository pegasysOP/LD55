using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixCouldronMinigateStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;

    [SerializeField] Timer timer;
    private float timerDuration = 10f;

    public override bool StartMinigameStep()
    {
        Debug.Log("Mix Couldron Minigame step started");
        timer.StartTimer(timerDuration, OnTimerFinished);
        return true;
    }

    private void OnTimerFinished()
    {
        Debug.Log("Mix Couldron Minigame step ended");
        OnMinigameStepOver.Invoke(this, MedalType.None);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
