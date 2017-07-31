using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimation : MonoBehaviour {

    SpriteRenderer renderer;

    [SerializeField]
    Sprite playerLeft, playerRight;

    bool isLeft;
	// Use this for initialization
	void Start () {
        renderer = GetComponent<SpriteRenderer>();

        isLeft = false;
        StartCoroutine(loop());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator loop()
    {
        if (isLeft)
        {
            renderer.sprite = playerLeft;
        }
        else {
            renderer.sprite = playerRight;
        }

        isLeft = !isLeft;
        yield return new WaitForSeconds(0.5f);
        StartCoroutine(loop());
    }
}
