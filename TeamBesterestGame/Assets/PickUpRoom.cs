using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRoom : MonoBehaviour {

    private bool inHand = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (inHand)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z - Camera.main.transform.position.z;
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector3(
                Mathf.Round(transform.position.x),
                Mathf.Round(transform.position.y),
                Mathf.Round(transform.position.z));
            if (Input.GetMouseButtonDown(1))
            {
                inHand = false;
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            inHand = true;
        }
    }
}
