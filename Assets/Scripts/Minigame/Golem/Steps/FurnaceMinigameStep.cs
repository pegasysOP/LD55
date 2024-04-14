using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceMinigameStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;

    public Slider furnaceHeatSlider;

    private float timerDuration = 10f;
    private float timer;

    [SerializeField] GameObject TimerGO;

    public override bool StartMinigameStep()
    {
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = timerDuration;
        furnaceHeatSlider.value = 7;
        TimerGO.GetComponent<Slider>().maxValue = timerDuration;
    }

    // Update is called once per frame
    void Update()
    {
        furnaceHeatSlider.value -= Time.deltaTime;

        if (furnaceHeatSlider.value == 0 || furnaceHeatSlider.value > furnaceHeatSlider.maxValue - 0.1)
        {
            //Minigame should fail if the heat completely dissapears or reaches the maximum
            OnMinigameStepOver.Invoke(this, MedalType.None);
        }

        timer -= Time.deltaTime;
        TimerGO.GetComponent<Slider>().value = Mathf.CeilToInt(timer);
        if (timer <= 0)
        {
            CalculateScore();
        }
        HandleInput();
        
    }

    void CalculateScore()
    {
        if (furnaceHeatSlider.value < 10 && furnaceHeatSlider.value > 9)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }
        if (furnaceHeatSlider.value < 9 && furnaceHeatSlider.value > 8)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Silver);
        }
        if (furnaceHeatSlider.value < 8 && furnaceHeatSlider.value > 7)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Gold);
        }
        if (furnaceHeatSlider.value < 7 && furnaceHeatSlider.value > 6)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Jade);
        }

        if (furnaceHeatSlider.value < 6 && furnaceHeatSlider.value > 5)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Gold);
        }
        if (furnaceHeatSlider.value < 5 && furnaceHeatSlider.value > 4)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Silver);
        }
        if (furnaceHeatSlider.value < 4 && furnaceHeatSlider.value > 3)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }
        if (furnaceHeatSlider.value < 3 && furnaceHeatSlider.value > 0)
        {
            OnMinigameStepOver.Invoke(this, MedalType.None);
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnMinigameStepOver.Invoke(this, MedalType.Silver);
        }
        if (Input.GetMouseButtonDown(0))
        {
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
