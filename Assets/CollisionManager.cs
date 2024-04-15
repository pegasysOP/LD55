using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{

    ArrangeFlowerMinigameStep flowerMinigameStep;

    //[SerializeField] bool acceptBrown = false;

    public enum PetalType
    {
        Brown,
        Blue,
        Red
    }

    [SerializeField] PetalType recepticleType = PetalType.Brown;

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
        if(collision.tag == "PetalBrown" && recepticleType == PetalType.Brown)
        {
            flowerMinigameStep.IncrementPetalsMatched(collision.gameObject);
            Debug.Log("Triggered by PetalBrown");
        }
        if (collision.tag == "PetalBlue" && recepticleType == PetalType.Blue)
        {
            flowerMinigameStep.IncrementPetalsMatched(collision.gameObject);
            Debug.Log("Triggered by PetalBlue");
        }
        if (collision.tag == "PetalRed" && recepticleType == PetalType.Red)
        {
            flowerMinigameStep.IncrementPetalsMatched(collision.gameObject);
            Debug.Log("Triggered by PetalRed");
        }
    }

}
