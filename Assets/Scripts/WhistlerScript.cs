using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhistlerScript : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public AudioSource whistle;

    public NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemyAgent();
        float distance =  Vector3.Distance(player.transform.position, transform.position);
        SetWhistleVolume(distance);
        SetWhistleSpeed(distance);
    }

    private void EnemyAgent()
    {
        agent.SetDestination(player.transform.position);
    }

    private void SetWhistleVolume(float distance)
    {
        
        whistle.volume = distance / 100.0f;
      //  Debug.Log("Whistler volume: " + distance / 100.0f);

    }

    private void SetWhistleSpeed(float distance)
    {
        if (distance > 100) 
        {
            agent.speed = 6;
        }
        else if (distance > 6) 
        {
            agent.speed = 4;
        }
        else {
            agent.speed = 50;
            //Lose animation
        }
        
        //Debug.Log("Whistler distance: " + distance);
        Debug.Log("Whistler speed: " + agent.speed);
        //Debug.Log("Whistler volume: " + distance / 100.0f);

    }
}
