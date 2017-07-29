using UnityEngine;
using System.Collections;

public class ZombieController : MonoBehaviour {

    bool isSelected;

    [SerializeField]
    SpriteRenderer gfx;

	// Use this for initialization
	void Start () {
        isSelected = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Select()
    {
        isSelected = true;
        //gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        gfx.color = Color.blue;
    }

    public void Deselect()
    {
        isSelected = false;
        //gameObject.GetComponent<SpriteRenderer>().color = Color.cyan;
        gfx.color = Color.red;
    }

    public bool isZombieSelected()
    {
        return isSelected;
    }
}
