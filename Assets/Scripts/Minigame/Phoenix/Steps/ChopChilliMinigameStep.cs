using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChopChilliMinigameStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;

    private float timerDuration = 10f;

    [SerializeField] Sprite[] chilliSprites;

    private const int numChopsRequired = 3;
    private int numChops = 0;

    private bool isDragging = false;

    [SerializeField] GameObject chilliGO;

    [SerializeField] GameObject TimerGO;

    [SerializeField] Timer timer;

    GameObject knifeGO;

    [SerializeField] GameObject[] guidelines;

    int roundCounter = 0;
    int numberOfRounds = 3;

    int failedChops = 0;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip bonkClip;
    [SerializeField] private AudioClip chopClip;
    [SerializeField] private AudioClip finalChopClip;

    public override bool StartMinigameStep()
    {
        Debug.Log("Chop Chilli Minigame step started");
        timer.StartTimer(timerDuration, OnTimerFinished);
        knifeGO = GameObject.FindGameObjectWithTag("Knife");

        return true;
    }

    private void OnTimerFinished()
    {
        if(roundCounter >= 2 && failedChops == 0)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Gold);
        }
        else if (roundCounter >= 2 && failedChops > 0)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Silver);
        }
        else if (roundCounter >= 1)
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
        StartNewChilli();

    }

    void StartNewChilli()
    {
        chilliGO.GetComponent<SpriteRenderer>().sprite = chilliSprites[0];
        guidelines[0].SetActive(true);
        guidelines[1].SetActive(false);
        guidelines[2].SetActive(false);
    }

    private void HandleChilliComplete()
    {
        if (roundCounter >= numberOfRounds)
            HandleMinigameComplete();
        else
            StartNewChilli();
    }

    void HandleMinigameComplete()
    {
        if(failedChops == 0)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Jade);
        }
        else
        {
            OnMinigameStepOver.Invoke(this, MedalType.Gold);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Chop chilli Minigame step complete");
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }
        #endif

        HandleInput();

        Vector3 pos = knifeGO.transform.position;
        pos.y = Camera.main.ScreenToWorldPoint(Input.mousePosition).y;

        if(pos.y > 1.9)
        {
            pos.y = 1.9f;
        }

        pos.z = 0;
        Debug.Log(pos.y);
        knifeGO.transform.position = pos;

        Debug.Log(knifeGO.transform.position);
        
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (Mathf.Abs(knifeGO.transform.position.y - guidelines[numChops].transform.position.y) <= 0.1f && guidelines[numChops].activeInHierarchy == true)
            {
                guidelines[numChops].SetActive(false);
                if (numChops + 1 < guidelines.Length)
                {
                    guidelines[numChops + 1].SetActive(true);
                    audioSource.PlayOneShot(chopClip);
                }
                numChops++;
                chilliGO.GetComponent<SpriteRenderer>().sprite = chilliSprites[numChops];
                
            }
            else
            {
               failedChops++;
               audioSource.PlayOneShot(bonkClip);
            }

            if (numChops >= numChopsRequired)
            {
                numChops = 0;
                roundCounter++;
                //Debug.Log("Chili chopping minigame completed!");
                //OnMinigameStepOver.Invoke(this, MedalType.Gold);
                audioSource.PlayOneShot(finalChopClip);
                HandleChilliComplete();
            }
        }
    }
}
