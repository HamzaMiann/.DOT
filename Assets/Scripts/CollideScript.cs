using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideScript : MonoBehaviour {

    public GameObject explosion;
    public GameObject platform;

    private Vector2 previousForce;
    private Rigidbody2D body;

    public Vector2 origin;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        //origin = transform.position;
    }

    // Update is called once per frame
    void Update () {
        //previousForce = body.velocity;
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Dot")
        {
            if (collision.gameObject.GetComponent<CircleCollider2D>().radius > 0.1f)
            {
                GameObject e = Instantiate(explosion);
                Vector2 explosionLocation = (collision.gameObject.transform.position + transform.position) / 2.0f;
                e.transform.position = explosionLocation;
                
                Vector2 thirdVertex = new Vector2(explosionLocation.x, origin.y);

                float distanceFromOrigin = Vector2.Distance(origin, explosionLocation);

                float a = Vector2.Distance(explosionLocation, thirdVertex);
                float c = distanceFromOrigin;

                float angle = Mathf.Rad2Deg * Mathf.Asin(a / c);

                if (thirdVertex.y > explosionLocation.y)
                    angle = -angle;

                GameObject p = Instantiate(platform);
                p.transform.localScale = new Vector2(distanceFromOrigin, 0.2f);
                p.transform.position = (explosionLocation + origin) / 2.0f;
                p.transform.rotation = Quaternion.Euler(0, 0, angle);

                p.GetComponent<PlatformLifeTime>().size = distanceFromOrigin;

                //Debug.Log("a = " + a + " c = " + c + " asin = " + angle);

                if (GameManager.catchPlatform != null)
                    Destroy(GameManager.catchPlatform);
                GameManager.catchPlatform = p;
                
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
            //else
            //{
            //    Destroy(collision.gameObject);
            //    GameManager.SpawnQueue++;
            //    body.AddForce(previousForce);
            //}
        }
    }

}
