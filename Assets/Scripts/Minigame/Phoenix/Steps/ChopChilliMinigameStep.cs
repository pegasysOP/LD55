using System;
using UnityEngine;
using UnityEngine.UI;

public class ChopChilliMinigameStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;

    private float timerDuration = 10f;
    private float timer;

    [SerializeField] GameObject TimerGO;

    public override bool StartMinigameStep()
    {
        Debug.Log("Chop Chilli Minigame step started");
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
            Debug.Log("Chop chilli Minigame step complete");
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }

        timer -= Time.deltaTime;
        TimerGO.GetComponent<Slider>().value = Mathf.CeilToInt(timer);
    }
}
