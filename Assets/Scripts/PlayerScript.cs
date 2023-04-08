using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{

    public AudioSource carSound;

    public float range = 5f;
    public Camera cam;

    public TextMeshProUGUI grabHintText;

    public int hintsToFind = 6;
    public TextMeshProUGUI hintText;
    public float showHintTime = 0f;

    public GameObject winScreen;

    public GameObject whistler;

    public GameObject looseScreen;

    void Start()
    {
        hintText.enabled = false;
        Time.timeScale = 0;
        //rb = GetComponent<Rigidbody>();
        grabHintText.enabled = false; 
    }

    // Debería ser FixedUpdate si se usan fisicas, pero no me permite direccionar al jugador a los costados con el cursor
    void Update() 
    {
      // Move();
       PerformRaycast();

       showHintTime -= Time.deltaTime;
        if (showHintTime <= 0.0f)
        {
            hintText.enabled = false;
        }

    }

    void PerformRaycast()
    {
       RaycastHit hit;        
       Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
       if (Physics.Raycast(ray , out hit, range)) {
            if (hit.collider.CompareTag("Hint")) {
                if (Input.GetKeyDown(KeyCode.E)) 
                {
                    PickUpHint(hit);
                    hit.collider.tag = "PickedHint";
                }
                grabHintText.enabled = true; 
            }
            else {
                grabHintText.enabled = false; 
            }
       }
    }

    void PickUpHint(RaycastHit hit)
    {
        ShowHintText();
        HintScript script = hit.transform.gameObject.GetComponent<HintScript>();
        script.TurnOffLight();
        script.DestroyPaper();
        Debug.Log("Hint picked up");
    }

    public void ShowHintText()
    {
        switch(hintsToFind)
        {
            case 6:
                hintText.text = "Muevete rapido y no te pierdas del camino";
                break;
            case 5:
                hintText.text = "Debes recoger todas las notas para poder escapar";
                break;
            case 4:
                hintText.text = "Ten en cuenta las bifuraciones del camino para no perderte al volver";
                break;
            case 3:
                hintText.text = "Solo quedan dos notas por encontrar";
                break;
            case 2:
                hintText.text = "Para salir a tiempo debes hacerlo en coche. Queda una ultima nota con la llave del mismo";
                break;
            case 1:
                hintText.text = "Encuentra el carro y sal de aqui!";
                break;
            default:
                // code block
                break;
        }
        hintText.enabled = true;
        showHintTime = 8.0f;
        hintsToFind -= 1;
        Debug.Log("Hints to find: " + hintsToFind);
    }


    void OnCollisionEnter(Collision col) 
    {
        Debug.Log("Collision with " + col.gameObject.name);
        switch(col.gameObject.tag)
        {
        case "Hint":
            break;
        case "Whistler":
           // Debug.Log("End game: You loose");
            break;
        case "Exit":
            if (AreHintsPickedUp())
            {
                WinGame();
            }
            break;
        case "LooseArea":
            Time.timeScale = 0;
            looseScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            break;
        }
    }

    public void WinGame()
    {
        Debug.Log("End game: You win");
        Time.timeScale = 0;
        winScreen.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        whistler.GetComponent<AudioSource>().Stop();
        carSound.Play();

    }

    public bool AreHintsPickedUp()
    {
        return hintsToFind <= 0;
    }

    
    

}
