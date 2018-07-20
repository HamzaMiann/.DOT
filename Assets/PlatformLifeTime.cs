using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLifeTime : MonoBehaviour {


    public float size = 0f;

    private float timePassed;
    private float maxTime;

    private MeshRenderer mesh;
    private float colourX;
    private float colourY;
    private float colourZ;

	// Use this for initialization
	void Start () {
        timePassed = 0f;
        maxTime = scaleFactor(size);
        mesh = GetComponent<MeshRenderer>();
        colourX = mesh.materials[0].color.r;
        colourY = mesh.materials[0].color.g;
        colourZ = mesh.materials[0].color.b;
	}
	
	// Update is called once per frame
	void Update () {
        timePassed += Time.deltaTime;
        mesh.material.color = new Color(1, 1, 1, (maxTime - timePassed) / maxTime);
        //Debug.Log("Max Time = " + maxTime + " Time = " + timePassed);
        if (timePassed >= maxTime)
            Destroy(gameObject);
        Debug.Log((maxTime - timePassed) / maxTime);
	}

    float scaleFactor(float distance)
    {
        float factor = 10 / distance;//distance * -.5f + 1;
        if (factor < 0)
            factor = 0f;
        return factor;
    }
}
