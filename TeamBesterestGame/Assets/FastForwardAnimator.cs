using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastForwardAnimator : MonoBehaviour {

    public GameManager gameManager;
    public Sprite slow;
    public Sprite slowDepressed;
    public Sprite normal;
    public Sprite normalDepressed;
    public Sprite fast;
    public Sprite fastDepressed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnMouseDown()
    {
        if (gameManager.timeSpeed == 3)
        {
            GetComponent<Image>().sprite = normalDepressed;
        }
        else if (gameManager.timeSpeed == 2)
        {
            GetComponent<Image>().sprite = fastDepressed;
        }
        else if (gameManager.timeSpeed == 1)
        {
            GetComponent<Image>().sprite = slowDepressed;
        }
    }

    public void OnMouseUp()
    {
        if (gameManager.timeSpeed == 3)
        {
            GetComponent<Image>().sprite = normal;
        }

        else if (gameManager.timeSpeed == 2)
        {
            GetComponent<Image>().sprite = fast;
        }
        else if (gameManager.timeSpeed == 1)
        {
            GetComponent<Image>().sprite = slow;
        }
    }
}
