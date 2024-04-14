using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SprinklePowderMinigameStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;

    private float timerDuration = 10f;
    private float timer;

    [SerializeField] GameObject TimerGO;

    [SerializeField] GameObject powderGO;

    public override bool StartMinigameStep()
    {
        Debug.Log("Sprinkle Powder Minigame step started");
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
        timer -= Time.deltaTime;
        TimerGO.GetComponent<Slider>().value = Mathf.CeilToInt(timer);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyPowder();
            Debug.Log("Sprinkle Powder Minigame step complete");
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }

        if (timer <= 0)
        {
            DestroyPowder();
            OnMinigameStepOver.Invoke(this, MedalType.None);
        }

        HandleInput();

    }

    void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse input detected");
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            Instantiate(powderGO, mousePos, Quaternion.identity);
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
