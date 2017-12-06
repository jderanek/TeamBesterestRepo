using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {

    public bool inConstructionMode;
	public bool doorClosed = true;

	public GameObject door;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        inConstructionMode = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().inConstructionMode;
    }

    private void OnMouseOver()
    {
		if (Input.GetMouseButtonDown (1) && inConstructionMode) 
		{
			doorClosed = !doorClosed;
			door.gameObject.SetActive(doorClosed);
		} 
	
    }

}
