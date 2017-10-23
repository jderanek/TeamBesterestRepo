using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public GameObject monsterInHand;
    public bool isHoldingObject;
    public GameObject monster;
    [HideInInspector]
    public GameObject heldObject;

    private GameObject resume;
    private GameObject resumeButton;

	public List<GameObject> monsterCollection = new List<GameObject>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isHoldingObject)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z - Camera.main.transform.position.z;
            heldObject.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }

        if (Input.GetMouseButtonDown(1))
        {
            isHoldingObject = false;
        }
    }

    public GameObject SpawnMonster(GameObject resume)
    {
        GameObject newMonster = Instantiate(monster, resume.transform.position, Quaternion.identity);
        newMonster.SetActive(false);
        return newMonster;

		monsterCollection.Add (newMonster);
		Debug.Log ("monsterCollection Size = " + monsterCollection.Count);

    }

    public void PickUpObject(GameObject otherObject)
    {
        isHoldingObject = true;
        heldObject = otherObject;
        otherObject.SetActive(true);
    }
}
