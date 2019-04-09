using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingColor : MonoBehaviour {

    private bool increasing = false;
    public Image img;

	// Use this for initialization
	void Start () {
		img = GetComponent<Image>();
       
	}
	
	// Update is called once per frame
	void Update () {
        /*if (increasing)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a += 1.0);
            //img.color.a += 1.0f;
            if (img.color.a >= 255)
            {
                increasing = false;
            }
        }
        else
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a += 1.0);
            //img.color.a -= 1.0f;
            if (img.color.a <= 0)
            {
                increasing = true;
            }
        }*/
        

	}
}
