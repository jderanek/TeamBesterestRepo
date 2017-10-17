using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public bool MonsterGrabbed;
    private GameObject MonsterInstance;
    public GameObject Monster;

    private GameObject Resume;

    private GameObject ResumeButton;

	// Use this for initialization
	void Start ()
    {
        MonsterGrabbed = false;
	}
	
	void Update ()
    {
        Resume = GameObject.FindGameObjectWithTag("Resume");
        ResumeButton = GameObject.FindGameObjectWithTag("ResumeButton");
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ResumeButton.GetComponent<HiringUIScript>().MonsterInstance.SetActive(true);
            //Resume.SetActive(false);
            Destroy(Resume);
        }
    }
}
