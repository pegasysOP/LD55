using System;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Slider slider;

    private Action onComplete;
    private float time;
    private bool running;
 
    public void StartTimer(float duration, Action onComplete = null)
    {
        this.onComplete = onComplete;

        time = duration;
        slider.maxValue = duration;

        running = true;
    }

    public void StopTimer()
    {
        running = false;
    }

    void Update()
    {
        if (!running)
            return;

        time -= Time.deltaTime;
        slider.value = time;

        if (time <= 0)
        {
            running = false;
            time = 0;
            
            if (onComplete != null)
                onComplete();
        }
    }
}
