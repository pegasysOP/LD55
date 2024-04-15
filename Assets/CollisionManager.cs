using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    ArrangeFlowerMinigameStep flowerMinigameStep;
    // Start is called before the first frame update
    void Start()
    {
        flowerMinigameStep = FindObjectOfType<ArrangeFlowerMinigameStep>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "PetalBrown")
        {
            flowerMinigameStep.IncrementPetalsMatched(collision.gameObject);
            Debug.Log("Triggered by PetalBrown");
        }
        if (collision.tag == "PetalBlue")
        {
            flowerMinigameStep.IncrementPetalsMatched(collision.gameObject);
            Debug.Log("Triggered by PetalBlue");
        }
    }

}
