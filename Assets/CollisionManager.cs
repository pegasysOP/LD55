using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
        if(collision.tag == "PetalBrown")
        {
            Debug.Log("Triggered by PetalBrown");
        }
        if (collision.tag == "PetalBlue")
        {
            Debug.Log("Triggered by PetalBlue");
        }
    }
}
