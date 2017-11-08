using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRoom : MonoBehaviour {

    private bool inHand = false;
    public bool inConstructionMode;


	// Use this for initialization
	void Awake ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {

        inConstructionMode = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().inConstructionMode;

        if (inHand)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z - Camera.main.transform.position.z;
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            transform.position = new Vector3(
                Mathf.Round(transform.position.x),
                Mathf.Round(transform.position.y),
                0f); //Mathf.Round(transform.position.z));
            if (Input.GetMouseButtonDown(1))
            {
                inHand = false;
                if (this.GetComponent<RoomScript>() != null)
                {
                    //his.GetComponent<RoomScript>().
                }
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && inConstructionMode)
        {
            inHand = true;
        }
    }
}
