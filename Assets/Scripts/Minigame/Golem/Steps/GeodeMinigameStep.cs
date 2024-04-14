using System;
using System.Collections;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class GeodeMinigameStep : MinigameStep
{
    [SerializeField] private SpriteRenderer geodeSprite;
    [SerializeField] private Transform geodePivot;
    [SerializeField] private Transform dottedLinePivot;
    [SerializeField] private Transform chiselPivot;
    [SerializeField] private TextMeshProUGUI debugText;

    [Header("Game Paramaters")]
    [SerializeField] private int numberOfRounds;
    [SerializeField] private float angleTolerance;
    [SerializeField] private float geodeRotationSpeed;
    private Coroutine geodeRotationCoroutine;

    public override event EventHandler<MedalType> OnMinigameStepOver;

    private bool trackingMouse;
    private bool chiselInRange;

    private int roundCounter;
    private int missCounter;

    void Update()
    {
        if (trackingMouse)
        {
            HandleChiselMovement();
            chiselInRange = CalculateChiselLineDistance();
            HandleMouseClick();
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Crush Geode Minigame step complete");
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }
#endif
    }

    public override bool StartMinigameStep()
    {
        trackingMouse = false;
        chiselInRange = false;

        roundCounter = 0;
        missCounter = 0;

        StartNewGeode();

        return true;
    }

    private void HandleMinigameComplete()
    {
        int score = 4;

        // each round not complete
        score -= numberOfRounds - roundCounter;

        // each miss
        score -= missCounter;

        Debug.Log($"GEODE COMPLETE, SCORE: {score}, MEDAL: {GetMedalTypeFromScore(score)}");

        OnMinigameStepOver.Invoke(this, GetMedalTypeFromScore(score));
    }

    private MedalType GetMedalTypeFromScore(int score)
    {
        if (score >= 4)
            return MedalType.Jade;
        else if (score >= 3)
            return MedalType.Gold;
        else if (score >= 2)
            return MedalType.Silver;
        else if (score >= 1)
            return MedalType.Bronze;
        else
            return MedalType.None;
    }

    private void StartNewGeode()
    {
        roundCounter++;

        SetLineRotation();
        StartGeodeRotation(geodeRotationSpeed);
        trackingMouse = true;
    }

    private void HandleGeodeComplete()
    {
        StopGeodeRotation();
        trackingMouse = false;

        if (roundCounter >= numberOfRounds)
            HandleMinigameComplete();
        else
            StartNewGeode();
    }

    private void SetLineRotation()
    {
        float angle = Random.Range(0f, 360f);
        dottedLinePivot.Rotate(Vector3.forward, angle, Space.Self);
    }

    private IEnumerator HandleGeodeRotation(float rotationSpeed)
    {
        while (true)
        {
            geodePivot.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime, Space.Self);
            yield return new WaitForEndOfFrame();
        }
    }

    private void StartGeodeRotation(float rotationSpeed)
    {
        geodeRotationCoroutine = StartCoroutine(HandleGeodeRotation(rotationSpeed));
    }

    private void StopGeodeRotation()
    {
        if (geodeRotationCoroutine != null)
            StopCoroutine(geodeRotationCoroutine);
    }

    private void HandleChiselMovement()
    {
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(chiselPivot.position);
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        float angleBetween = Mathf.Atan2(positionOnScreen.y - mouseOnScreen.y, positionOnScreen.x - mouseOnScreen.x) * Mathf.Rad2Deg;
        chiselPivot.rotation = Quaternion.Euler(new Vector3(0f, 0f, angleBetween + 90f)); // not sure what 90 deg offset is from
    }

    private void HandleMouseClick()
    {
        if (!Input.GetMouseButtonDown(0))
            return;

        if (!chiselInRange)
        {
            missCounter++;
            return;
        }

        HandleGeodeComplete();
    }

    private bool CalculateChiselLineDistance()
    {
        bool inRange = false;

        float distance = Mathf.Abs(chiselPivot.eulerAngles.z - dottedLinePivot.eulerAngles.z);

        if (distance > 0 - angleTolerance && distance < 0 + angleTolerance)
            inRange = true;
        else if (distance > 180 - angleTolerance && distance < 180 + angleTolerance)
            inRange = true;
        else if (distance > 360 - angleTolerance && distance < 360 + angleTolerance)
            inRange = true;

        // debug text
        //debugText.text = (Mathf.RoundToInt(distance / 10) * 10).ToString();
        //debugText.color = inRange ? Color.green : Color.white;

        return inRange;
    }
}
