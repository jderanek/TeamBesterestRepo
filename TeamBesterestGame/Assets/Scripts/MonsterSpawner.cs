using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public bool MonsterGrabbed;
    private GameObject MonsterInstance;
    public GameObject Monster;

    private GameObject Resume;

	// Use this for initialization
	void Start ()
    {
        MonsterGrabbed = false;
	}
	
	void Update ()
    {
        Resume = GameObject.FindGameObjectWithTag("Resume");

		/*if (MonsterInstance != null && MonsterGrabbed == true)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z - Camera.main.transform.position.z;
            MonsterInstance.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }

        if (Input.GetMouseButtonDown(1) && MonsterGrabbed == true)
        {
            MonsterGrabbed = false;
            Resume.SetActive(true);
        }*/
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            MonsterGrabbed = true;
            MonsterInstance = Instantiate(Monster, gameObject.transform.position, Quaternion.identity);

            Resume.SetActive(false);
        }
    }
}
