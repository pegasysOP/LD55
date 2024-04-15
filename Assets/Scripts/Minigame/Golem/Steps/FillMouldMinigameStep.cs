using System;
using UnityEngine;
using UnityEngine.UI;

public class FillMouldMinigameStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;

    [SerializeField] private Slider flowRateSlider;
    [SerializeField] private Slider filledAmountSlider;
    [SerializeField] private Transform flaskTransform;

    [SerializeField] private Timer timer;
    [SerializeField] float timerDuration = 10f;



    public override bool StartMinigameStep()
    {
        Debug.Log("Fill Mould minigame started");
        timer.StartTimer(timerDuration, OnTimerFinished);
        return true;
    }

    private void OnTimerFinished()
    {
        OnMinigameStepOver.Invoke(this, MedalType.Bronze);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnMinigameStepOver.Invoke(this, MedalType.Silver);
        }

        filledAmountSlider.value += flowRateSlider.value * Time.deltaTime;

        if (filledAmountSlider.value == filledAmountSlider.maxValue)
        {
            //Minigame should fail if the heat completely dissapears or reaches the maximum
            OnMinigameStepOver.Invoke(this, MedalType.None);
        }

        flaskTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, -45 + flowRateSlider.value * 45));
    }
}
