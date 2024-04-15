using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PluckFlowerMinigameStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;
    
    [SerializeField] Timer timer;
    float timerDuration = 10f;

    [SerializeField] GameObject[] petals;

    GameObject petal = null;

    bool isDragging = false;

    int petalsToPluck;

    public override bool StartMinigameStep()
    {
        Debug.Log("Pluck flower minigame started");
        timer.StartTimer(timerDuration, OnTimerFinished);
        petalsToPluck = petals.Length;
        return true;
    }

    void OnTimerFinished()
    {
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
            Debug.Log("Pluck Flower Minigame step complete");
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }

        HandlePluckLogic();

        HandleInput();

        if(petalsToPluck == 0)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Jade);
        }

    }

    void HandlePluckLogic()
    {
        if (isDragging)
        {
            petal = DragPetal(Input.mousePosition);
            if (petal != null)
            {
                petal.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            }
        }
        else
        {
            if (petal != null)
            {
                Debug.Log("Petal: " + petal.name);
                petal.AddComponent<Rigidbody2D>();
                petal.GetComponent<Rigidbody2D>().gravityScale = 0.25f;
                petalsToPluck -= 1;
                petal = null;
            }
        }
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


    private GameObject DragPetal(Vector2 mousePos)
    {
        // Get the rectTransform component of the chili image
        //RectTransform rectTransform = chilliGO.rectTransform;

        // Create a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        // Raycast against the chili image
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);

        // Check if the raycast hit the petal
        if (hit.collider != null)
        {
            return hit.collider.gameObject;
        }

        return null;
    }
}
