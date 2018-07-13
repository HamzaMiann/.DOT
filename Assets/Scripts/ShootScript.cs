using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootScript : MonoBehaviour {

    public GameObject throwable;
    public float forceAmount = 10f;

    private MovementScript move;

	// Use this for initialization
	void Start () {
        move = GetComponent<MovementScript>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(1))
        {
            Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 CurrentPos = this.transform.position;
            Vector2 forceVector = Indentize(MousePos - CurrentPos) * forceAmount;
            GameObject obj = Instantiate(throwable);
            obj.GetComponent<Rigidbody2D>().AddForce(forceVector);
            obj.transform.position = this.transform.position;
            obj.GetComponent<CollideScript>().origin = move.groundCheck.position;
        }
	}

    private Vector2 Indentize(Vector2 vec)
    {
        float divider;
        if (vec.x > vec.y)
            divider = vec.x;
        else
            divider = vec.y;
        return new Vector2(vec.x / divider, vec.y / divider);
    }
}
