using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MixCouldronMinigateStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;

    [SerializeField] Timer timer;
    [SerializeField] private float timerDuration = 10f;

    public float stirringRadius = 500000f; // Adjust the radius of the circular stirring motion
    private Vector3 stirringCenter; // The center of the circular stirring motion
    private float lastAngle; // The angle of the last mouse position relative to the stirring center

    private float lastTime;
    private float currentTime;

    float currentSpeed;

    private bool isStirring = false;

    [SerializeField] GameObject CauldronContentsGO;
    [SerializeField] GameObject arrowGO;

    [SerializeField] Slider StirCompletionSlider;

    [Header("Speed Stuff")]
    [SerializeField] Image sliderBackground;
    [SerializeField] Color fastColour;
    [SerializeField] Color goodColour;
    [SerializeField] Color slowColour;


    public override bool StartMinigameStep()
    {
        Debug.Log("Mix Couldron Minigame step started");
        timer.StartTimer(timerDuration, OnTimerFinished);
        stirringCenter = new Vector3 (0, 0, 0);
        currentSpeed = 0;
        return true;
    }

    private void OnTimerFinished()
    {
        Debug.Log("Mix Couldron Minigame step ended");

        if(StirCompletionSlider.value == StirCompletionSlider.maxValue)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Jade);
        }
        else if (StirCompletionSlider.value > 9f)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Gold);
        }
        else if (StirCompletionSlider.value > 8f)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Silver);
        }
        else if (StirCompletionSlider.value > 7f)
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
        currentTime = Time.time;
        lastTime = Time.time;
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
            if(arrowGO.activeInHierarchy == true)
            {
                arrowGO.SetActive(false);
            }
            StirPot();
        }

        if (StirCompletionSlider.value == StirCompletionSlider.maxValue)
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

    void StirPot()
    {
        // Get the mouse position in world space
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldMousePos.z = transform.position.z; // Ensure the mouse position is on the same plane as the pot

        // Calculate the direction from the stirring center to the mouse position
        Vector3 direction = worldMousePos - stirringCenter;

        // Calculate the current angle of the mouse position relative to the stirring center
        float currentAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //Debug.Log("CurrentAngle: " + currentAngle);

        // Calculate the angle difference between the current and last mouse positions
        float angleDifference = Mathf.DeltaAngle(lastAngle, currentAngle);

        if(angleDifference < 0)
        {
            // Rotate the pot based on the angle difference
            CauldronContentsGO.transform.Rotate(Vector3.forward, angleDifference);
        }
        currentTime = Time.time;
        currentSpeed =  CalculateRotationSpeed(currentAngle, lastAngle, Time.deltaTime);

        if (currentSpeed <= 180) // slow
        {
            sliderBackground.color = slowColour;
        }
        else if(currentSpeed < 360) // good
        {
            StirCompletionSlider.value += 5 * Time.deltaTime;
            sliderBackground.color = goodColour;
        }
        else
        {
            sliderBackground.color = fastColour;
        }

        // Update the last angle for the next frame
        lastAngle = currentAngle;
        lastTime = currentTime;
    }

    public float CalculateRotationSpeed(float initialAngle, float finalAngle, float deltaTime)
    {
        if (deltaTime == 0f)
            return 0f; // Return 0 if the time delta is 0 to avoid division by zero

        // Ensure angles are in the range of 0 to 360 degrees
        initialAngle = WrapAngle(initialAngle);
        finalAngle = WrapAngle(finalAngle);

        float angleDifference;

        // Handle wrap-around from 360 back to 0 degrees
        if (Mathf.Abs(finalAngle - initialAngle) > 180f)
        {
            // Determine the shortest direction of rotation
            if (finalAngle > initialAngle)
                angleDifference = (360f - finalAngle) + initialAngle;
            else
                angleDifference = (360f - initialAngle) + finalAngle;

            // Negate the angle difference to get the correct direction
            angleDifference = -angleDifference;
        }
        else
        {
            angleDifference = finalAngle - initialAngle;
        }

        // Calculate rotation speed in degrees per second
        float rotationSpeed = angleDifference / deltaTime;

        return rotationSpeed;
    }

    private float WrapAngle(float angle)
    {
        angle %= 360f;
        if (angle < 0f)
            angle += 360f;
        return angle;
    }
}
