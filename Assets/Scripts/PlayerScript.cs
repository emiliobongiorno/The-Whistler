using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerScript : MonoBehaviour
{

   /* public Animator anim;
    public float walkSpeed = 2.0f;
    public float runSpeed = 4.0f;
    private float speed = 2.0f;
    private Rigidbody rb;
*/
    public float range = 5f;
    public Camera cam;

    public TextMeshProUGUI grabHintText;

    public int hintsToFind = 6;
    public TextMeshProUGUI hintText;
    public float showHintTime = 0f;

    public GameObject winScreen;

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
                hintText.text = "Muevete rápido y no te pierdas del camino";
                break;
            case 5:
                hintText.text = "Ten en cuenta las bifuraciones del camino para no perderte al volver";
                break;
            case 4:
                hintText.text = "Debes recoger todas las notas para poder escapar";
                break;
            case 3:
                hintText.text = "Solo quedan dos notas por encontrar";
                break;
            case 2:
                hintText.text = "Para salir a tiempo debes hacerlo en coche. Queda una última nota con la llave del mismo";
                break;
            case 1:
                hintText.text = "Encuentra el carro y sal de aquí!";
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
            Debug.Log("End game: You win");
            Time.timeScale = 0;
            winScreen.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (AreHintsPickedUp())
            {
                Debug.Log("End game: You win");
                Time.timeScale = 0;
                winScreen.SetActive(true);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            break;
        }

    }

    public bool AreHintsPickedUp()
    {
        return hintsToFind == 0;
    }

    
    

}
