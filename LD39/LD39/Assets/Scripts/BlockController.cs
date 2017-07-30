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
        Debug.Log("Exit");
        if (coll.gameObject.tag == "Player") {
            Debug.Log("Player exitted");
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
