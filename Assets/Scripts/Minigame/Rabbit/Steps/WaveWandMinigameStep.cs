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

    [SerializeField] GameObject[] checkpoints;

    GameObject[] checkpointsLeft;

    GameObject previousActivated = null;

    private int numCheckpoints;
    private int checkPointsHit;

    public override bool StartMinigameStep()
    {
        Debug.Log("Wave wand minigame started");
        timer.StartTimer(timerDuration, OnTimerFinished);

        Cursor.visible = false;

        return true;
    }

    void OnTimerFinished()
    {
        DestroyPaint();

        Cursor.visible = true;

        OnMinigameStepOver.Invoke(this, MedalType.None);
    }

    void DestroyPaint()
    {
        GameObject[] toDestroy = GameObject.FindGameObjectsWithTag("Paint");
        foreach (GameObject go in toDestroy)
        {
            Destroy(go);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        numCheckpoints = checkpoints.Length;
        checkpointsLeft = checkpoints;
    }

    // Update is called once per frame
    void Update()
    {
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Wave wand Minigame step complete");
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }
        #endif

        HandleInput();

        if (isDragging)
        {
            Draw(Input.mousePosition);
            GameObject go = HitCheckpoint(Input.mousePosition);
            if(go != null)
            {

                if(go.tag == "Checkpoint1")
                {

                }

                for(int i = 0; i < checkpointsLeft.Length; i++)
                {
                    if (go == checkpointsLeft[i])
                    {
                        checkpointsLeft[i] = null;
                    }
                }
                //checkPointsHit += 1;
                go = null;
            }
        }

        if(checkPointsHit == numCheckpoints)
        {
            DestroyPaint();

            Cursor.visible = true;

            OnMinigameStepOver.Invoke(this, MedalType.Jade);
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

    public void IncrementCheckpoint()
    {
        checkPointsHit += 1;
    }

    private GameObject HitCheckpoint(Vector2 mousePos)
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
