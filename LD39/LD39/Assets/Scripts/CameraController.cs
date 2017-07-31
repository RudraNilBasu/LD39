using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

    [SerializeField]
    Vector3 startPosition;

    Animation anim;
	// Use this for initialization
	void Awake () {
        anim = GetComponent<Animation>();
	}

    void Start()
    {
        transform.position = startPosition;

        anim.Play("CameraStart");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
	}
}
