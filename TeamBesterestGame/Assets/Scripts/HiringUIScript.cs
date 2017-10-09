using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HiringUIScript : MonoBehaviour {

    public GameObject Resume;
    public Transform ResumeSpawn;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(Resume, ResumeSpawn.position, Quaternion.identity);
        }
    }
}
