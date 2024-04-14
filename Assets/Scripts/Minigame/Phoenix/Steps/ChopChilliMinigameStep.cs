using System;
using UnityEngine;
using UnityEngine.UI;

public class ChopChilliMinigameStep : MinigameStep
{
    public override event EventHandler<MedalType> OnMinigameStepOver;

    private float timerDuration = 10f;
    private float timer;

    [SerializeField] Sprite[] chilliSprites;

    private const int numChopsRequired = 3;
    private int numChops = 0;

    private bool isDragging = false;

    [SerializeField] Image chilliImage;

    [SerializeField] GameObject TimerGO;

    public override bool StartMinigameStep()
    {
        Debug.Log("Chop Chilli Minigame step started");
        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        timer = timerDuration;
        TimerGO.GetComponent<Slider>().maxValue = timerDuration;

        chilliImage.sprite = chilliSprites[numChops];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Chop chilli Minigame step complete");
            OnMinigameStepOver.Invoke(this, MedalType.Bronze);
        }

        timer -= Time.deltaTime;
        TimerGO.GetComponent<Slider>().value = Mathf.CeilToInt(timer);

        if (timer <= 0)
        {
            OnMinigameStepOver.Invoke(this, MedalType.None);
        }

        HandleInput();
        
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 mousePos = Input.mousePosition;

            if (ChopChili(mousePos))
            {
                numChops++;

                chilliImage.sprite = chilliSprites[numChops];

                isDragging = false;

                if (numChops >= numChopsRequired)
                {
                    Debug.Log("Chili chopping minigame completed!");
                    OnMinigameStepOver.Invoke(this, MedalType.Gold);
                }
            }
        }

    }

    private bool ChopChili(Vector2 mousePos)
    {
        Vector2 worldPos = Camera.main.ScreenToWorldPoint(mousePos);

        Rect chiliRect = new Rect(transform.position.x - chilliImage.rectTransform.rect.width / 2,
                                  transform.position.y - chilliImage.rectTransform.rect.height / 2,
                                  chilliImage.rectTransform.rect.width,
                                  chilliImage.rectTransform.rect.height);

        return chiliRect.Contains(worldPos);
    }
}
