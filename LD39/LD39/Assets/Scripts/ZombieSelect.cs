using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieSelect : MonoBehaviour {

    bool selectionMode;

    //GameObject[] zombies;

    [SerializeField]
    List<GameObject> zmb;

    [SerializeField]
    GameObject spark;

    GameObject selectedZombie;

	// Use this for initialization
	void Start () {
        selectionMode = false;
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            collectDeactivateZombies();
            selectionMode = true;
            selectZombie(zmb[0]);
        }
        if (selectionMode && Input.GetKeyDown(KeyCode.X))
        {
            selectNextZombie();
        }
        if (selectionMode && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            activateZombie();
        }
    }

    void collectDeactivateZombies()
    {
        //zombies = GameObject.FindGameObjectsWithTag("Zombie");
        zmb = new List<GameObject>(GameObject.FindGameObjectsWithTag("Zombie"));

        for (int i = 0; i < zmb.Count; i++) {
            if (!zmb[i].GetComponent<PlayerController>().isDeactivated())
                zmb.RemoveAt(i);
        }
    }

    void selectNextZombie()
    {
        for (int i = 0; i < zmb.Count; i++)
        {
            if (zmb[i].GetComponent<ZombieController>().isZombieSelected()) {
                deselectZombie(zmb[i]);
                selectZombie(zmb[(i + 1) % zmb.Count]);
                break;
            }
        }
    }

    void activateZombie()
    {
        deselectZombie(selectedZombie);
        selectedZombie.GetComponent<PlayerController>().activate();
        selectionMode = false;

        GameObject _spark = (GameObject)Instantiate(spark, selectedZombie.transform.position, Quaternion.identity);
        StartCoroutine(Kill(_spark));
    }

    void selectZombie(GameObject _zombie)
    {
        selectedZombie = _zombie;
        _zombie.GetComponent<ZombieController>().Select();
    }

    void deselectZombie(GameObject _zombie)
    {
        _zombie.GetComponent<ZombieController>().Deselect();
    }

    IEnumerator Kill(GameObject _spark)
    {
        yield return new WaitForSeconds(1.0f);
        Destroy(_spark);
    }
}
