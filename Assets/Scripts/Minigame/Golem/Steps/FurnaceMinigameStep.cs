using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;
using UnityEngine.UI;

public class FurnaceMinigameStep : MinigameStep
{
    [SerializeField] FurnaceHeatRegion heatRegionPrefab;
    [SerializeField] Transform heatRegionContainer;
    [SerializeField] Slider furnaceHeatSlider;
    [SerializeField] Timer timer;

    [Header("Game Paramaters")]
    [SerializeField] private float timerDuration;
    [SerializeField] private List<Segment> heatRegions;

    public override event EventHandler<MedalType> OnMinigameStepOver;

    public override bool StartMinigameStep()
    {
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateHeatRegions();

        timer.StartTimer(timerDuration, CalculateScore);
        furnaceHeatSlider.value = 7;
    }

    // Update is called once per frame
    void Update()
    {
        furnaceHeatSlider.value -= Time.deltaTime;

        HandleInput();

        if (furnaceHeatSlider.value == 0 || furnaceHeatSlider.value > furnaceHeatSlider.maxValue - 0.1)
        {
            //Minigame should fail if the heat completely dissapears or reaches the maximum
            OnMinigameStepOver.Invoke(this, MedalType.None);
        }
    }

    private void CreateHeatRegions()
    {
        int totalWeight = 0;
        foreach (Segment segment in heatRegions)
            totalWeight += segment.Weight;

        float weightToWidth = heatRegionContainer.GetComponent<RectTransform>().rect.width / (float)totalWeight;

        foreach (Segment segment in heatRegions)
        {
            FurnaceHeatRegion heatRegion = Instantiate(heatRegionPrefab, heatRegionContainer);
            heatRegion.Init(segment.Colour, segment.Weight * weightToWidth);
        }
    }

    void CalculateScore()
    {
        if (furnaceHeatSlider.value < 10 && furnaceHeatSlider.value > 9)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }
        if (furnaceHeatSlider.value < 9 && furnaceHeatSlider.value > 8)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Silver);
        }
        if (furnaceHeatSlider.value < 8 && furnaceHeatSlider.value > 7)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Gold);
        }
        if (furnaceHeatSlider.value < 7 && furnaceHeatSlider.value > 6)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Jade);
        }

        if (furnaceHeatSlider.value < 6 && furnaceHeatSlider.value > 5)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Gold);
        }
        if (furnaceHeatSlider.value < 5 && furnaceHeatSlider.value > 4)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Silver);
        }
        if (furnaceHeatSlider.value < 4 && furnaceHeatSlider.value > 3)
        {
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }
        if (furnaceHeatSlider.value < 3 && furnaceHeatSlider.value > 0)
        {
            OnMinigameStepOver.Invoke(this, MedalType.None);
        }
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnMinigameStepOver.Invoke(this, MedalType.Silver);
        }
        if (Input.GetMouseButtonDown(0))
        {
            if (furnaceHeatSlider.value + 1 < furnaceHeatSlider.maxValue)
            {
                furnaceHeatSlider.value += 1;
            }
            else
            {
                furnaceHeatSlider.value = furnaceHeatSlider.maxValue;
            }
        }
    }

    [System.Serializable]
    public struct Segment
    {
        public Color Colour;
        public int Weight;
    }
}
