using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkipButtonAnimator : MonoBehaviour {

    public Sprite skipButton;
    public Sprite skipButtonDepressed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        GetComponent<Image>().sprite = skipButtonDepressed;
    }

    public void OnMouseUp()
    {
        GetComponent<Image>().sprite = skipButton;
    }
}
