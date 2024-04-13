using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : Minigame
{
    public override bool StartMinigame()
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        timer.SetTime(5);
        timer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer.GetTime() < 0) 
        {
            //End minigame
        }
    }
}
