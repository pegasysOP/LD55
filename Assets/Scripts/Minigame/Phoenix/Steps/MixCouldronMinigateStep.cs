using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixCouldronMinigateStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;

    [SerializeField] Timer timer;
    private float timerDuration = 10f;

    public float stirringRadius = 50f; // Adjust the radius of the circular stirring motion
    private Vector3 stirringCenter; // The center of the circular stirring motion
    private float lastAngle; // The angle of the last mouse position relative to the stirring center

    private bool isStirring = false;

    [SerializeField] GameObject CauldronContentsGO;


    public override bool StartMinigameStep()
    {
        Debug.Log("Mix Couldron Minigame step started");
        timer.StartTimer(timerDuration, OnTimerFinished);
        stirringCenter = new Vector3 (0, 0, 0);
        return true;
    }

    private void OnTimerFinished()
    {
        Debug.Log("Mix Couldron Minigame step ended");
        OnMinigameStepOver.Invoke(this, MedalType.None);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Left mouse button pressed
        {
            isStirring = true;
        }
        else if (Input.GetMouseButtonUp(0)) // Left mouse button released
        {
            isStirring = false;
        }

        // Stir the pot if the left mouse button is held down
        if (isStirring)
        {
            StirPot();
        }
    }

    void StirPot()
    {
        // Get the mouse position in world space
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePos.z = transform.position.z; // Ensure the mouse position is on the same plane as the pot

        // Calculate the direction from the stirring center to the mouse position
        Vector3 direction = worldMousePos - stirringCenter;

        // Calculate the current angle of the mouse position relative to the stirring center
        float currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.Log("CurrentAngle: " + currentAngle);

        // Check if the mouse is within the stirring radius
        if (direction.magnitude <= stirringRadius)
        {
            // Calculate the angle difference between the current and last mouse positions
            float angleDifference = Mathf.DeltaAngle(lastAngle, currentAngle);

            if(angleDifference < 0)
            {
                // Rotate the pot based on the angle difference
                CauldronContentsGO.transform.Rotate(Vector3.forward, angleDifference);
            }
            

            // Update the last angle for the next frame
            lastAngle = currentAngle;
        }
        else
        {
            // Reset the last angle if the mouse is outside the stirring radius
            lastAngle = 0f;
        }
    }
}
