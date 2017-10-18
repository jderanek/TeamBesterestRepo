using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [HideInInspector]
    public GameObject monsterInHand;
    public bool isHoldingMonster;
    public GameObject monster;

    private GameObject resume;
    private GameObject resumeButton;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (isHoldingMonster)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z - Camera.main.transform.position.z;
            monster.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }

        if (Input.GetMouseButtonDown(1) && isHoldingMonster)
        {
            isHoldingMonster = false;
        }
    }

    public GameObject SpawnMonster(GameObject resume)
    {
        GameObject newMonster = Instantiate(monster, resume.transform.position, Quaternion.identity);
        newMonster.SetActive(false);
        return newMonster;
    }

    public void PickUpMonster(GameObject monster)
    {
        isHoldingMonster = true;
        monster.SetActive(true);
    }
}
