using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    private Animator anim;
    private SpriteRenderer renderer;
    private Rigidbody2D body;

    public float movementForce = 10f;
    public float jumpForce = 2f;

    private bool isGrounded = true;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        renderer = GetComponent<SpriteRenderer>();
        body = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {

        //displayStatus();

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Air") && isGrounded)
            anim.SetBool("Jump", false);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        if (isGrounded && Input.GetKey(KeyCode.W))
        {
            anim.SetBool("Jump", true);
            body.velocity = Vector2.up * jumpForce;
        }

        if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("Walking", true);
            renderer.flipX = false;
            transform.Translate(Vector2.right * movementForce, Space.Self);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("Walking", true);
            renderer.flipX = true;
            transform.Translate(Vector2.left * movementForce, Space.Self);
        }
        else
        {
            anim.SetBool("Walking", false);
        }
	}


    void displayStatus()
    {
        Debug.Log("Jump = " + anim.GetBool("Jump") + " Walking = " + anim.GetBool("Walking"));
    }
}
