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
        SetWhistleVolume();
    }

    private void EnemyAgent()
    {
        agent.SetDestination(player.transform.position);
    }

    private void SetWhistleVolume()
    {
        float distance =  Vector3.Distance(player.transform.position, transform.position);
        whistle.volume = distance / 100.0f;
    }
}
