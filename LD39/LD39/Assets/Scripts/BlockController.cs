using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class BlockController : MonoBehaviour {

    Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionExit2D(Collision2D coll)
    {
        if ((coll.gameObject.tag == "Player") || (coll.gameObject.tag == "Zombie")) {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
