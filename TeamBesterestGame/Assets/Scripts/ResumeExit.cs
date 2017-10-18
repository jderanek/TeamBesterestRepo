using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeExit : MonoBehaviour {

    private GameObject resume;
    private GameObject resumeButton;

	// Use this for initialization
	void Awake ()
    {
        resumeButton = GameObject.FindGameObjectWithTag("ResumeButton");
	}

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            
            var ResumeButtonScript = resumeButton.GetComponent<HiringUIScript>();
            ResumeButtonScript.resumeUp = false;
            Destroy(ResumeButtonScript.monsterInstance);
            Destroy(resume);
        }
    }

    public void Register(GameObject resistery)
    {
        resume = resistery;
    }
}
