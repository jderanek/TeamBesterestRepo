using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeExit : MonoBehaviour {

    private GameObject Resume;
    private GameObject ResumeButton;

	// Use this for initialization
	void Awake ()
    {
        ResumeButton = GameObject.FindGameObjectWithTag("ResumeButton");
        Resume = GameObject.FindGameObjectWithTag("Resume");
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ResumeButtonScript = ResumeButton.GetComponent<HiringUIScript>();
            ResumeButtonScript.ResumeUp = false;
            Destroy(Resume);
        }
    }
}
