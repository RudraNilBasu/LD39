using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour {

    bool isSelected;

    [SerializeField]
    GameObject arrow;

	// Use this for initialization
	void Start () {
        isSelected = false;

        arrow.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Select()
    {
        isSelected = true;
        arrow.SetActive(true);
        //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        //gfx.color = Color.blue;
        //gfx.sprite = active;
    }

    public void Deselect()
    {
        isSelected = false;
        arrow.SetActive(false);
        //gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        //gfx.color = Color.red;
        //gfx.sprite = deactive;
    }

    public bool isZombieSelected()
    {
        return isSelected;
    }
}
