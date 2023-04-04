using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int hintsToFind = 6;
    public GameObject hintPrefab;
    private List<GameObject> hints = new List<GameObject>();
    public TextMeshProUGUI hintText;
    public float showHintTime = 0f;

    void Awake()
    {
        if (GameManager.instance == null)
        {
            GameManager.instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
     
    }

    void Start()
    {
        hintText.enabled = false;
        Time.timeScale = 0;
        /* Random hints positions */
        /*
        int x = 0;
        int z = 0;
        for (int i = 0; i < hintsToFind; i++) {
            Vector3 hintPosition = Random(new Vector3(x,0,z), new Vector3(x+50,0,z+50));
            x+=50;
            z+=50;
            CreateHint(hintPosition);
        }*/
        
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        showHintTime -= Time.deltaTime;
        if (showHintTime <= 0.0f)
        {
            hintText.enabled = false;
        }
    }

    private void CreateHint(Vector3 position) 
    {
        GameObject hint = Instantiate(hintPrefab, position, Quaternion.identity);
        position.y = Terrain.activeTerrain.SampleHeight(hint.transform.position);
        hint.transform.position = position;
        hints.Add(hint);
    }

    public void FindHint()
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

    public bool AreHintsPickedUp()
    {
        return hintsToFind == 0;
    }


    private Vector3 Random(Vector3 min, Vector3 max)
     {
         return new Vector3(UnityEngine.Random.Range(min.x, max.x), UnityEngine.Random.Range(min.y, max.y), UnityEngine.Random.Range(min.z, max.z));
     }

}
