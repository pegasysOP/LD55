using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveWandMinigameStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;
    [SerializeField] Timer timer;
    float timerDuration = 10f;

    bool isDragging = false;

    public GameObject paintSplodge;

    public override bool StartMinigameStep()
    {
        Debug.Log("Wave wand minigame started");
        timer.StartTimer(timerDuration, OnTimerFinished);
        return true;
    }

    void OnTimerFinished()
    {
        GameObject[] toDestroy = GameObject.FindGameObjectsWithTag("Paint");
        foreach (GameObject go in toDestroy)
        {
            Destroy(go);
        }
        OnMinigameStepOver.Invoke(this, MedalType.None);
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
            Debug.Log("Wave wand Minigame step complete");
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }

        HandleInput();

        if (isDragging)
        {
            Draw(Input.mousePosition);
        }
    }

    void Draw(Vector2 mousePosition)
    {
        Vector3 convertedMousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        convertedMousePosition.z = 0;

        Instantiate(paintSplodge, convertedMousePosition , Quaternion.identity);
    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }
}
