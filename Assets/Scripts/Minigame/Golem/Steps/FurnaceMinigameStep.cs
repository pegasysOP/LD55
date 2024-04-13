using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceMinigameStep : MinigameStep
{
    public override event EventHandler<MinigameScore> OnMinigameStepOver;

    public Slider furnaceHeatSlider;

    private float timerDuration = 10f;
    private float timer;

    public override bool StartMinigameStep()
    {
        Debug.Log("Furnace Stoke Minigame step started");
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = timerDuration;
        furnaceHeatSlider.value = 7;
      
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(furnaceHeatSlider.value);
        furnaceHeatSlider.value -= Time.deltaTime;

        if (furnaceHeatSlider.value == 0 || furnaceHeatSlider.value > furnaceHeatSlider.maxValue - 0.1)
        {
            //Minigame should fail if the heat completely dissapears or reaches the maximum
            OnMinigameStepOver.Invoke(this, MinigameScore.None);
        }

        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            //Get the sliders value and score accordingly
            OnMinigameStepOver.Invoke(this, MinigameScore.Bronze);
        }
        HandleInput();
        
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Furnace Stoke Minigame step complete");
            OnMinigameStepOver.Invoke(this, MinigameScore.Silver);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Left mouse button pressed");
            if (furnaceHeatSlider.value + 1 < furnaceHeatSlider.maxValue)
            {
                furnaceHeatSlider.value += 1;
            }
            else
            {
                furnaceHeatSlider.value = furnaceHeatSlider.maxValue;
            }

        }
    }
}
