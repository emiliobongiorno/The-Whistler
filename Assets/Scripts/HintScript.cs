using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintScript : MonoBehaviour
{
    public Light spotLight;
    public GameObject paperHint;

    void Start()
    {
        spotLight = GetComponentInChildren<Light>();
     
    }

    void Update() 
    {
    }

    public void TurnOffLight() 
    {
        spotLight.intensity = 0.0f;
    }

    public void DestroyPaper() 
    {
        Destroy(paperHint);
    }
/*
    void OnCollisionEnter(Collision col) 
    {
        Debug.Log("Collision with " + col.gameObject.name);
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("This is a Player");
            spotLight.intensity = 0.0f;
        }
    }*/
}
