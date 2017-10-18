using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomScript : MonoBehaviour {

    public List<GameObject> roomMembers = new List<GameObject>();
    private GameObject heldObject;

    void Awake()
    {

    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseOver()
    {
        heldObject = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().heldObject;
        if (heldObject.CompareTag("Monster") && Input.GetMouseButtonDown(1)) {
            roomMembers.Add(heldObject);
            print(roomMembers[0].GetComponent<MonsterScript>().monsterName);
        }
    }
}
