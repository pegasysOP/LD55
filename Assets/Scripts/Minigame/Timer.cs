using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float duration;
    private float time;

    private bool isPaused = false;
 

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            time -= Time.deltaTime;
        }
    }

    public void SetTime(float duration)
    {
        this.duration = duration; 
    }

    public float GetTime()
    {
        return duration;
    }

    public void Reset()
    {
        time = duration;
    }

    public void Play()
    {
        isPaused = false;
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void Unpause()
    {
        isPaused = false;
    }

}
