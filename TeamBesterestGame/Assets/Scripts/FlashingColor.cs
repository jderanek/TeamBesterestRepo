using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlashingColor : MonoBehaviour {

    public float speed;
    private bool increasing = false;
    private Image img;

	// Use this for initialization
	void Start () {
		img = GetComponent<Image>();
       
	}
	
	// Update is called once per frame
	void Update () {
        if (increasing)
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a + speed);
            if (img.color.a >= 0.99f)
            {
                increasing = false;
            }
            
        }
        else
        {
            img.color = new Color(img.color.r, img.color.g, img.color.b, img.color.a - speed);
            if (img.color.a <= 0.01f)
            {
                increasing = true;
            }
        }
        

	}
}
