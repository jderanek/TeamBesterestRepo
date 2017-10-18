using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResumeScript : MonoBehaviour {

    public GameObject resumeCanvas;
    private bool holdingResume;

	// Use this for initialization
	void Awake ()
    {
        transform.Find("Exit Button").GetComponent<ResumeExit>().Register(this.gameObject);
	}
	
	// Update is called once per frame
	void Update ()
    {
		if (holdingResume) {
            //print("hi");
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z - Camera.main.transform.position.z;
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);

            if (Input.GetMouseButtonDown(1))
            {
                holdingResume = false;
            }
        }
        
	}

    void OnMouseOver ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            print("hi");
            holdingResume = true;
        }

        
    }
}
