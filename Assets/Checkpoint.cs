using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public bool isActivated = false;
    int numberOfActivations = 0;
    WaveWandMinigameStep step;

    float cooldown = 1f;
    float timer;

    // Start is called before the first frame update
    void Start()
    {
        timer = cooldown;
        step = FindObjectOfType<WaveWandMinigameStep>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isActivated)
        {
            numberOfActivations += 1;
            if (this.gameObject.tag != "Checkpoint1")
            {
                isActivated = true;
                step.IncrementCheckpoint();
            }
            else
            {
                if(numberOfActivations == 2)
                {
                    isActivated = true;
                    step.IncrementCheckpoint();
                }
            }
            
        }
        
        Debug.Log("Entered collision: " + collision.gameObject.tag);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Exited collision: " + collision.gameObject.tag);
    }

}
