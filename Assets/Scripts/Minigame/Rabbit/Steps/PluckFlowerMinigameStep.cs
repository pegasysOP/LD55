using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class PluckFlowerMinigameStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;
    
    [SerializeField] Timer timer;
    [SerializeField] float timerDuration = 15f;

    [SerializeField] GameObject[] petals;

    GameObject petal = null;

    bool isDragging = false;

    bool isPlaying = false;

    int petalsToPluck;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip pluckClip;

    [SerializeField] float pitchRange;

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
        #if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Pluck Flower Minigame step complete");
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }
        #endif

        HandlePluckLogic();

        HandleInput();

        if(petalsToPluck == 0)
        {
            StartCoroutine(WaitThenEnd());
        }

    }

    private IEnumerator WaitThenEnd()
    {
        timer.StopTimer();

        yield return new WaitForSeconds(1f);

        OnMinigameStepOver.Invoke(this, MedalType.Jade);
    }

    void HandlePluckLogic()
    {
        if (isDragging)
        {
            petal = DragPetal(Input.mousePosition);
            if (petal != null)
            {
                petal.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));

                if (!isPlaying)
                {
                    audioSource.pitch = 1f + Random.Range(-pitchRange / 2f, pitchRange / 2f);
                    audioSource.PlayOneShot(pluckClip);
                    isPlaying = true;
                }
                
            }
        }
        else
        {
            if (petal != null)
            {
                Debug.Log("Petal: " + petal.name);
                petal.GetComponent<Rigidbody2D>().gravityScale = 0.25f;
                petal.GetComponent<PolygonCollider2D>().enabled = false;
                petalsToPluck -= 1;
                petal = null;
                isPlaying = false;
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
