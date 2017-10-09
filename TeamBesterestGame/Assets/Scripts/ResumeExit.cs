using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeExit : MonoBehaviour {

    private GameObject Resume;

	// Use this for initialization
	void Awake ()
    {
        Resume = GameObject.FindGameObjectWithTag("Resume");
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(Resume);
        }
    }
}
