using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int hintsToFind = 20;
    public GameObject hintPrefab;
    private List<GameObject> hints = new List<GameObject>();

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
        /* Random hints positions */
        
        int x = 0;
        int z = 0;
        for (int i = 0; i < hintsToFind; i++) {
            //Vector3 hintPosition = Random(new Vector3(490,0,490), new Vector3(510,0,510));
            Vector3 hintPosition = Random(new Vector3(x,0,z), new Vector3(x+50,0,z+50));
            x+=50;
            z+=50;
            CreateHint(hintPosition);
        }
        
       // Vector3 hintPosition = Random(new Vector3(0,0,50), new Vector3(0,0,50));
        //CreateHint(hintPosition);
        
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
