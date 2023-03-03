using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public int hintsToFind = 5;
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
        for (int i = 0; i < hintsToFind; i++) {
            Vector3 hintPosition = Random(new Vector3(-10,0,-10), new Vector3(10,0,10));
            hints.Add(Instantiate(hintPrefab, hintPosition, Quaternion.identity));
        }
        
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
