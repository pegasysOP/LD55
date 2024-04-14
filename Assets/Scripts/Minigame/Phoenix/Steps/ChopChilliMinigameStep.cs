using System;
using UnityEngine;

public class ChopChilliMinigameStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;

    public override bool StartMinigameStep()
    {
        Debug.Log("Chop Chilli Minigame step started");
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
            Debug.Log("Chop chilli Minigame step complete");
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }
    }
}
