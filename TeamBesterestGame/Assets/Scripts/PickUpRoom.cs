using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpRoom : MonoBehaviour {

    private bool inHand = false;
    public bool inConstructionMode;
    private GameManager gameManager;

    private int roundedX;
    private int roundedY;

    public RoomScript roomScript;

	// Use this for initialization
	void Awake ()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        roomScript = gameObject.GetComponent<RoomScript>();
    }
	
	// Update is called once per frame
	void Update () {

        inConstructionMode = gameManager.inConstructionMode;

        if (inHand)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z - Camera.main.transform.position.z;
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            roundedX = Mathf.RoundToInt(transform.position.x);
            roundedY = Mathf.RoundToInt(transform.position.y);
            transform.position = new Vector3(
                roundedX,
                roundedY,
                0f); //Mathf.Round(transform.position.z));
            if (Input.GetMouseButtonDown(1))
            {
                inHand = false;
                gameManager.roomList[roundedX, roundedY] = gameObject;
                roomScript.myX = roundedX;
                roomScript.myY = roundedY;
                roomScript.UpdateNeighbors();
            }
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && inConstructionMode)
        {
            inHand = true;
            roomScript.ClearNeighbors();
            gameManager.roomList[roundedX, roundedY] = null;
        }
    }
}
