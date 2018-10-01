using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableScrolling : MonoBehaviour {

    GameObject sceneCamera;

    // Use this for initialization
    void Start () {
        sceneCamera = GameObject.FindGameObjectWithTag("MainCamera");
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void OnMouseEnter()
    {
        sceneCamera.GetComponent<CameraControls>().scrollLock = false;
    }

    public void OnMouseExit()
    {
        sceneCamera.GetComponent<CameraControls>().scrollLock = true;
    }
}
