using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour {

    public bool monsterGrabbed;
    private GameObject monsterInstance;
    public GameObject monster;

    private GameObject resume;

    private GameObject resumeButton;

	// Use this for initialization
	void Start ()
    {
        monsterGrabbed = false;
	}
	
	void Update ()
    {
        resume = GameObject.FindGameObjectWithTag("Resume");
        resumeButton = GameObject.FindGameObjectWithTag("ResumeButton");
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            resumeButton.GetComponent<HiringUIScript>().monsterInstance.SetActive(true);
            //Resume.SetActive(false);
            Destroy(resume);
        }
    }
}
