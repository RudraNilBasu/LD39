using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    GameObject camera;
	// Use this for initialization
	void Start () {
        camera = GameObject.Find("Camera");

        if (camera == null) {
            Debug.LogError("No Camera found");
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Player") {
            camera.GetComponent<Animation>().Play("CameraEnd");
            StartCoroutine(loadNextScene());
            //Debug.Log("Next Level");
        }
    }

    IEnumerator loadNextScene()
    {
        yield return new WaitForSeconds(0.1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
