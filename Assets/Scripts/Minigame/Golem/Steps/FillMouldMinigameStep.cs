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

    [Header("Game Parameters")]
    [SerializeField] float timerDuration = 10f;
    [SerializeField] float overFillAllowance = 0.1f;

    private float fillAmount;

    public override bool StartMinigameStep()
    {
        Debug.Log("Fill Mould minigame started");
        timer.StartTimer(timerDuration, OnTimerFinished);
        return true;
    }

    private void OnTimerFinished()
    {
        OnMinigameStepOver.Invoke(this, GetMedalTypeFromFill(fillAmount));
    }

    void Update()
    {
#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnMinigameStepOver.Invoke(this, MedalType.Silver);
        }
#endif

        fillAmount += flowRateSlider.value * Time.deltaTime;
        filledAmountSlider.value = fillAmount > 1f ? 1f : fillAmount;

        if (fillAmount >= 1 + overFillAllowance)
        {
            //Minigame should fail if the heat completely dissapears or reaches the maximum
            OnMinigameStepOver.Invoke(this, MedalType.None);
        }

        flaskTransform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.Euler(0, 0, -45 + flowRateSlider.value * 45));
    }

    private MedalType GetMedalTypeFromFill(float fill)
    {
        if (fill >= 0.95f)
            return MedalType.Jade;
        else if (fill >= 0.85)
            return MedalType.Gold;
        else if (fill >= 0.75)
            return MedalType.Silver;
        else if (fill >= 0.65)
            return MedalType.Bronze;
        else
            return MedalType.None;
    }
}
