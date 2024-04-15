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
    int numberOfRounds = 2;

    public override bool StartMinigameStep()
    {
        Debug.Log("Chop Chilli Minigame step started");
        timer.StartTimer(timerDuration, OnTimerFinished);
        knifeGO = GameObject.FindGameObjectWithTag("Knife");

        return true;
    }

    private void OnTimerFinished()
    {
        OnMinigameStepOver.Invoke(this, MedalType.None);
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
        //TODO: Calculate score here
        OnMinigameStepOver.Invoke(this, MedalType.Gold);
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
            for (int i = 0; i < guidelines.Length; i++)
            {
                if (Mathf.Abs(knifeGO.transform.position.y - guidelines[i].transform.position.y) <= 0.1f && guidelines[i].activeInHierarchy == true)
                {
                    guidelines[i].SetActive(false);
                    if (i+1 < guidelines.Length)
                    {
                        guidelines[i+1].SetActive(true); 
                    }
                    numChops++;
                    chilliGO.GetComponent<SpriteRenderer>().sprite = chilliSprites[numChops];
                }
            }

            if (numChops >= numChopsRequired)
            {
                numChops = 0;
                roundCounter++;
                //Debug.Log("Chili chopping minigame completed!");
                //OnMinigameStepOver.Invoke(this, MedalType.Gold);
                HandleChilliComplete();
            }
        }
    }
}
