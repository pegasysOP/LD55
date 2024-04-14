using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprinklePowderMinigameStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;

    private float timerDuration = 10f;

    [SerializeField] Timer timer;

    private float sprinkleDelay = 0.05f;
    private float sprinkleTimer;

    [SerializeField] GameObject TimerGO;

    [SerializeField] GameObject powderGO;

    public override bool StartMinigameStep()
    {
        Debug.Log("Sprinkle Powder Minigame step started");
        timer.StartTimer(timerDuration, OnTimerFinished);
        sprinkleTimer = sprinkleDelay;
        return true;
    }

    private void OnTimerFinished()
    {
        DestroyPowder();
        OnMinigameStepOver.Invoke(this, MedalType.None);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {   
        HandleInput();
        sprinkleTimer -= Time.deltaTime;
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyPowder();
            Debug.Log("Sprinkle Powder Minigame step complete");
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }
        if (Input.GetMouseButton(0))
        {
            Debug.Log("Mouse input detected");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            if(sprinkleTimer <= 0)
            {
                Instantiate(powderGO, mousePos, Quaternion.identity);
                sprinkleTimer = sprinkleDelay;
            }      
        }
    }

    void DestroyPowder()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Powder");

        foreach (GameObject gameObject in gameObjects)
        {
            Destroy(gameObject);
        }
    }
}
