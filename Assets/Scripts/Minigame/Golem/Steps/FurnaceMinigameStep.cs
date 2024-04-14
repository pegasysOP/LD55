using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceMinigameStep : MinigameStep
{
    [SerializeField] private FurnaceHeatRegion heatRegionPrefab;
    [SerializeField] private Transform heatRegionContainer;
    [SerializeField] private Image furnaceImage;
    [SerializeField] private Slider heatSlider;
    [SerializeField] private Timer timer;

    [Header("Game Paramaters")]
    [SerializeField] private float timerDuration;
    [SerializeField] private float coolingRate;
    [SerializeField] private float heatingRate;
    [SerializeField] private List<Segment> heatRegions;

    [Header("Furnace Images")]
    [SerializeField] private Sprite furnace0;
    [SerializeField] private Sprite furnace1;
    [SerializeField] private Sprite furnace2;
    [SerializeField] private Sprite furnace3;
    [SerializeField] private Sprite furnace4;

    public override event EventHandler<MedalType> OnMinigameStepOver;

    public override bool StartMinigameStep()
    {
        CreateHeatRegions();
        UpdateFurnaceImage();

        timer.StartTimer(timerDuration, OnTimerEnded);

        return true;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleCooling();
        HandleHeating();

        UpdateFurnaceImage();
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
        int total = 0;

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

    private void UpdateFurnaceImage()
    {
        int total = 0;

        for (int i = 0; i < heatRegions.Count; i++)
        {
            total += heatRegions[i].Width;

            if (heatSlider.value <= total)
            {
                if (i <= 1) // this is gross sorry
                    furnaceImage.sprite = furnace0;
                else if (i <= 2)
                    furnaceImage.sprite = furnace1;
                else if (i <= 3)
                    furnaceImage.sprite = furnace2;
                else if (i <= 4)
                    furnaceImage.sprite = furnace3;
                else
                    furnaceImage.sprite = furnace4;

                break;
            }
        }
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
