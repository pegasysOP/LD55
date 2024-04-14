using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillMouldMinigameStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;

    public Slider flowRateSlider;
    public Slider filledAmountSlider;

    private float timerDuration = 10f;
    private float timer;

    [SerializeField] GameObject TimerGO;

    public override bool StartMinigameStep()
    {
        Debug.Log("Fill Mould minigame started");
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = timerDuration;
        TimerGO.GetComponent<Slider>().maxValue = timerDuration;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnMinigameStepOver.Invoke(this, MedalType.Silver);
        }

        filledAmountSlider.value += flowRateSlider.value * Time.deltaTime;
        timer -= Time.deltaTime;
        TimerGO.GetComponent<Slider>().value = Mathf.CeilToInt(timer);

        if (filledAmountSlider.value == filledAmountSlider.maxValue)
        {
            //Minigame should fail if the heat completely dissapears or reaches the maximum
            OnMinigameStepOver.Invoke(this, MedalType.None);
        }

        if(timer <= 0)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Gold);
        }
    }

}
