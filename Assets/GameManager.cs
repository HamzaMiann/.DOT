using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{

    public GameObject obj;
    public static int SpawnQueue = 0;
    private static GameObject objectToSpawn;

    private void Start()
    {
        objectToSpawn = obj;
        SpawnQueue = 50;
        createNewObjects();
    }

    private static void createNewObjects()
    {
        for (; SpawnQueue > 0; SpawnQueue--)
        {
            Instantiate(objectToSpawn);
        }
    }

    public void Update()
    {
        createNewObjects();
    }

}
