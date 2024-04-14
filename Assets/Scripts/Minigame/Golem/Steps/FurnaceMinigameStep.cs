using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceMinigameStep : MinigameStep
{
    [SerializeField] private FurnaceHeatRegion heatRegionPrefab;
    [SerializeField] private Transform heatRegionContainer;
    [SerializeField] private Slider heatSlider;
    [SerializeField] private Timer timer;

    [Header("Game Paramaters")]
    [SerializeField] private float timerDuration;
    [SerializeField] private float coolingRate;
    [SerializeField] private float heatingRate;
    [SerializeField] private List<Segment> heatRegions;

    public override event EventHandler<MedalType> OnMinigameStepOver;

    public override bool StartMinigameStep()
    {
        return true;
    }

    private void Start()
    {
        CreateHeatRegions();

        timer.StartTimer(timerDuration, OnTimerEnded);
    }

    // Update is called once per frame
    private void Update()
    {
        HandleCooling();
        HandleHeating();
    }

    private void CreateHeatRegions()
    {
        int totalWeight = 0;
        foreach (Segment segment in heatRegions)
            totalWeight += segment.Width;

        float weightToWidth = heatRegionContainer.GetComponent<RectTransform>().rect.width / (float)totalWeight;

        foreach (Segment segment in heatRegions)
        {
            FurnaceHeatRegion heatRegion = Instantiate(heatRegionPrefab, heatRegionContainer);
            heatRegion.Init(segment.Colour, segment.Width * weightToWidth);
        }

        heatSlider.maxValue = totalWeight;
        heatSlider.value = totalWeight / 2f;
    }

    private void OnTimerEnded()
    {
        OnMinigameStepOver.Invoke(this, CalculateScore());
    }

    private MedalType CalculateScore()
    {
        float total = 0f;

        foreach (Segment segment in heatRegions)
        {
            total += segment.Width;

            if (heatSlider.value <= total)
            {
                Debug.Log($"Furnace step complete, Score: {segment.Score}");
                return segment.Score;
            }
        }

        return MedalType.None;
    }

    private void HandleCooling()
    {
        if (!Input.GetMouseButton(0))
            heatSlider.value -= coolingRate * Time.deltaTime;

        if (heatSlider.value <= 0)
            OnMinigameStepOver.Invoke(this, MedalType.None);
    }

    private void HandleHeating()
    {
        if (Input.GetMouseButton(0))
            heatSlider.value += heatingRate * Time.deltaTime;

        if (heatSlider.value >= heatSlider.maxValue)
            OnMinigameStepOver.Invoke(this, MedalType.None);

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Crush Geode Minigame step complete");
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }
#endif
    }

    [System.Serializable]
    public struct Segment
    {
        public Color Colour;
        public int Width;
        public MedalType Score;
    }
}
