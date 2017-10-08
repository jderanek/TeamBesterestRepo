using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    private bool MonsterGrabbed;
    private GameObject MonsterInstance;
    public GameObject Monster;

	// Use this for initialization
	void Start ()
    {
        MonsterGrabbed = false;
	}
	
	void Update ()
    {
		if (MonsterInstance != null && MonsterGrabbed == true)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z - Camera.main.transform.position.z;
            MonsterInstance.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }

        if (Input.GetMouseButtonDown(1) && MonsterGrabbed == true)
        {
            MonsterGrabbed = false;
        }
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MonsterGrabbed = true;

            MonsterInstance = Instantiate(Monster, gameObject.transform.position, Quaternion.identity);
        }
    }
}
