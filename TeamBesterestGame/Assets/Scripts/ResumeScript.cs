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
        
	}

    void OnMouseOver ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().PickUpObject(this.gameObject);
        }
    }
}
