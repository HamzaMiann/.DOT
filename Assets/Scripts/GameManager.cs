using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour{

    public GameObject obj;
    public static int SpawnQueue = 0;
    public int initialQueue = 100;
    private static GameObject objectToSpawn;

    public static GameObject catchPlatform = null;

    private void Start()
    {
        objectToSpawn = obj;
        SpawnQueue = initialQueue;
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
