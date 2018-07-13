using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslationScript : MonoBehaviour {

    private float m;
    private float b;

    //public float timeToLive = 50f;
    public float maxSize = 10f;
    public float minSize = 0.1f;
    public float jitter = 0.02f;

    private float lowerX;
    private float upperX;

    private float lowerY = -5f;
    private float upperY = 5f;

    private Vector3 target;
    private float lowerBound;
    private float step;
    private float size;

    private bool startFromTop;
    private LineRenderer lineRenderer;
    private CircleCollider2D collider;
    
    // Use this for initialization
    void Start () {
        startFromTop = pickChoice();

        m = Random.Range(-4, 4);
        b = Random.Range(-10, 10);

        lowerX = (lowerY - b) / m;
        upperX = (upperY - b) / m;

        float minY = f(lowerX);
        Vector3 LowerVector = new Vector3(lowerX, minY, 0);

        float maxY = f(upperX);
        Vector3 UpperVector = new Vector3(upperX, maxY, 0);

        //lowerBound = lowerX;// Random.Range(lowerX, lowerX + (upperX - lowerX)/2);
        step = 0.01f;//(upperX - lowerX) / timeToLive;
        
        if (m < 0 && startFromTop)
        {
            transform.position = UpperVector;
            target = LowerVector;
        }
        if (m > 0 && startFromTop)
        {
            step = -step;
            transform.position = UpperVector;
            target = LowerVector;
        }
        if (m < 0 && !startFromTop)
        {
            step = -step;
            transform.position = LowerVector;
            target = UpperVector;
        }
        if (m > 0 && !startFromTop)
        {
            transform.position = LowerVector;
            target = UpperVector;
        }

        size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector3(0, 0, 0);

        lowerBound = transform.position.x;

        lineRenderer = GetComponent<LineRenderer>();
        collider = GetComponent<CircleCollider2D>();
    }
	
	// Update is called once per frame
	void Update () {
        lowerBound += step;
        float jitterX = Random.Range(-jitter, jitter);
        float jitterY = Random.Range(-jitter, jitter);
        transform.position = new Vector3(lowerBound + jitterX, f(lowerBound) + jitterY, 0);
        if ((lowerBound > 10f || lowerBound < -10f || f(lowerBound) > 6f || f(lowerBound) < -6f))
        {
            GameManager.SpawnQueue++;
            Destroy(gameObject);
        }

        Vector2 CameraPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float s = scaleFactor(Vector2.Distance(CameraPoint, transform.position)) * size;
        transform.localScale = new Vector3(s, 0, s);
        collider.radius = s;

        if (s < 0.1f)
        {
            collider.enabled = false;
        }
        else
            collider.enabled = true;
        
        
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, CameraPoint);
        Color c = new Color(1, 1, 1, 1.5f * s);
        lineRenderer.startColor = c;
        lineRenderer.endColor = c;
    }

    private void OnDrawGizmos()
    {
        RenderPath();
    }

    void RenderPath()
    {
        float minY = f(lowerX);
        float maxY = f(upperX);
        Gizmos.DrawLine(new Vector3(lowerX, minY, 0), new Vector3(upperX, maxY, 0));
    }

    float f(float x)
    {
        //Debug.Log("x = " + x + " m = " + m + " b = " + b + " f(x) = " + (m*x + b));
        return m * x + b;
    }

    float scaleFactor(float distance)
    {
        float factor = distance * -.5f + 1;
        if (factor < 0)
            factor = 0f;
        return factor;
    }

    static bool pickChoice()
    {
        System.Random gen = new System.Random();
        int prob = gen.Next(100);
        return prob <= 50;
    }
}
