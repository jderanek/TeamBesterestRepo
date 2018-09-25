using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButtonAnimator : MonoBehaviour {

    public GameManager gameManager;

    public Sprite playButton;
    public Sprite playButtonDepressed;
    public Sprite pauseButton;
    public Sprite pauseButtonDepressed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        if (gameManager.paused)
        {
            GetComponent<Image>().sprite = playButtonDepressed;
        }
        else
        {
            GetComponent<Image>().sprite = pauseButtonDepressed;
        }
    }
    public void OnMouseUp()
    {
        if (gameManager.paused)
        {
            GetComponent<Image>().sprite = pauseButton;
        }
        else
        {
            GetComponent<Image>().sprite = playButton;
        }
    }
}
