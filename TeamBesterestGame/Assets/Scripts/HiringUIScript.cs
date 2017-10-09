using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiringUIScript : MonoBehaviour {

    public GameObject Resume;
    public Transform ResumeSpawn;
    private bool ResumeUp;

	// Use this for initialization
	void Start ()
    {
        ResumeUp = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && ResumeUp == false)
        {
            ResumeUp = true;
            Instantiate(Resume, ResumeSpawn.position, Quaternion.identity);
        }
    }
}
