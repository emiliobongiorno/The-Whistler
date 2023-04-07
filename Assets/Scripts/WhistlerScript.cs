using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WhistlerScript : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public AudioSource whistleSounds;
    public AudioSource looseSound;

    public NavMeshAgent agent;

    public Transform playerOrientation;

    public GameObject looseScreen;
    public GameObject whistlerImage;

    private bool lost = false;

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
        if (distance < 9 && lost == false) {
            lost = true;
            //Loose animation
            PlayerMovement playerMovement = player.GetComponent<PlayerMovement>();
            playerMovement.enabled = false;
            looseSound.Play();
            StartCoroutine(LooseAnimation()); 
        }
    }

    IEnumerator LooseAnimation()
    {
        bool showImage = false;
        for(int i = 0; i < 40; i++)
        {
            showImage = !showImage;
            yield return new WaitForSeconds(0.03f);
            whistlerImage.SetActive(showImage);
        } 
        whistlerImage.SetActive(true);
        Time.timeScale = 0;
        looseScreen.SetActive(true);
        Debug.Log("PERDISTE");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        whistleSounds.Stop();
    }

    private void EnemyAgent()
    {
        agent.SetDestination(player.transform.position);
    }

    private void SetWhistleVolume(float distance)
    {
        
        whistleSounds.volume = distance / 100.0f;
      //  Debug.Log("Whistler volume: " + distance / 100.0f);

    }

    private void SetWhistleSpeed(float distance)
    {
        if (distance > 100) 
        {
            agent.speed = 4;
        }
        else
        {
            agent.speed = 2;
        }
        
        //Debug.Log("Whistler distance: " + distance);
        Debug.Log("Whistler speed: " + agent.speed);
        //Debug.Log("Whistler volume: " + distance / 100.0f);

    }
}
