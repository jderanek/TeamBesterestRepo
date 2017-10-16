using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNewSquare : MonoBehaviour {

    public GameObject square;
    private GameObject currentSquare;
    private bool inHand;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (inHand)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z - Camera.main.transform.position.z;
            currentSquare.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
            currentSquare.transform.position = new Vector3(
                Mathf.Round(currentSquare.transform.position.x),
                Mathf.Round(currentSquare.transform.position.y),
                Mathf.Round(currentSquare.transform.position.z));
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
            currentSquare = Instantiate(square,
                new Vector3(Random.Range(-5.0f, 10.0f),
                    Random.Range(-5.0f, 10.0f),
                    0.0f),
                Quaternion.identity);
            inHand = true;
        }
    }
}
