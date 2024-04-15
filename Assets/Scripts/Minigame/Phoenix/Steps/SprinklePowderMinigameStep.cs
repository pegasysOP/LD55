using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class SprinklePowderMinigameStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;

    private float timerDuration = 10f;

    [SerializeField] Timer timer;

    private float sprinkleDelay = 0.05f;
    private float sprinkleTimer;

    [SerializeField] GameObject TimerGO;

    [SerializeField] GameObject powderGO;

    [SerializeField] Slider PowderFilledSlider;

    [SerializeField] GameObject containerGO;
    [SerializeField] GameObject guidelineGO;

    bool moveLeft;

    public override bool StartMinigameStep()
    {
        Debug.Log("Sprinkle Powder Minigame step started");
        timer.StartTimer(timerDuration, OnTimerFinished);
        sprinkleTimer = sprinkleDelay;
        return true;
    }

    private void OnTimerFinished()
    {
        //Get the slider value and score accordingly 
        DestroyPowder();

        if (PowderFilledSlider.value > 55 && PowderFilledSlider.value < 65)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Jade);
        }
        else if (PowderFilledSlider.value > 45 && PowderFilledSlider.value < 55)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Gold);
        }
        else if (PowderFilledSlider.value > 35 && PowderFilledSlider.value < 45)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Silver);
        }
        else if (PowderFilledSlider.value > 25 && PowderFilledSlider.value < 35)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }
        else
        {
            OnMinigameStepOver.Invoke(this, MedalType.None);
        } 
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
        DestroyPowderOnExit();
        //MoveBowl();
    }

    void MoveBowl()
    {
        if (moveLeft)
        {
            if(containerGO.transform.position.x > -5)
            {
                containerGO.transform.Translate(new Vector3(-2 * Time.deltaTime, 0, 0));
                guidelineGO.transform.Translate(new Vector3(-2 * Time.deltaTime, 0, 0));
            }
            else
            {
                moveLeft = false;
            }
        }
        else
        {
            if (containerGO.transform.position.x < 5)
            {
                containerGO.transform.Translate(new Vector3(2 * Time.deltaTime, 0, 0));
                guidelineGO.transform.Translate(new Vector3(2 * Time.deltaTime, 0, 0));
            }
            else
            {
                moveLeft = true;
            }
        }
    }

    void HandleInput()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DestroyPowder();
            Debug.Log("Sprinkle Powder Minigame step complete");
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }
        #endif
        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            mousePos.x += Random.Range(-0.5f, 0.5f);
            mousePos.y += Random.Range(-0.5f, 0.5f);

            if(sprinkleTimer <= 0)
            {
                PowderFilledSlider.value += 1;
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

    void DestroyPowderOnExit()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("Powder");

        foreach (GameObject gameObject in gameObjects)
        {
            if(gameObject.transform.position.y < -5)
            {
                Debug.Log("Destroyed");
                Destroy(gameObject);
                PowderFilledSlider.value -= 1;
            }    
        }
    }
}
