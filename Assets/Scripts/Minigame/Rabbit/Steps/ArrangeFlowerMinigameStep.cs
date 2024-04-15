using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class ArrangeFlowerMinigameStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;

    [SerializeField] Timer timer;
    float timerDuration = 100f;

    bool isDragging = false;

    GameObject petal = null;

    [SerializeField] private int numPetals = 7;
    int currentPetalsMatched = 0;

    public override bool StartMinigameStep()
    {
        Debug.Log("Arrange flower minigame started");
        timer.StartTimer(timerDuration, OnTimerFinished);
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
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Arrange flower Minigame step complete");
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }
        #endif
        HandleInput();
        HandleFlowerDraggingLogic();

        if(currentPetalsMatched == numPetals)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Jade);
        }
    }

    void HandleFlowerDraggingLogic()
    {
        if (isDragging)
        {
            if(petal == null)
            {
                petal = DragPetal(Input.mousePosition);
            }
            
            if (petal != null)
            {
                petal.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            }
        }
        else
        {
            if (petal != null)
            {
                petal = null;
            }
        }
    }

    public void IncrementPetalsMatched(GameObject flower)
    {
        flower.GetComponent<PolygonCollider2D>().enabled = false;
        currentPetalsMatched += 1;
        Debug.Log("Current matched: " + currentPetalsMatched);
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
        if (hit.collider != null && this.tag != "PetalOutline" )
        {
            return hit.collider.gameObject;
        }

        return null;
    }
}
