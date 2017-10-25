using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public bool doingSetup;

    public Slider cycleSlider;
    public GameObject cycleImage;
    public float cycleTimer = 100f;
    public float cycleDelay = 2f;

    public bool inConstructionMode;

    public GameObject heresTheFuckingButton;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //placement start
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
        //placement end

        cycleSlider.value = cycleTimer;

        if (cycleTimer <= 0)
        {
            doingSetup = true;
            cycleImage.SetActive(true);
            Invoke("NewCycle", 2);
        }
        else
        {
            //Time.timeScale = 1;
            cycleTimer -= Time.deltaTime;
        }
    }

    public GameObject SpawnMonster(GameObject resume)
    {
        GameObject newMonster = Instantiate(monster, resume.transform.position, Quaternion.identity);
        newMonster.SetActive(false);
        return newMonster;

		monsterCollection.Add (newMonster); // not working
		Debug.Log ("monsterCollection Size = " + monsterCollection.Count);
    }

    public void PickUpObject(GameObject otherObject)
    {
        if (otherObject.GetComponent<MonsterScript>().myRoom != null)
        {
            print("remove from room");
            otherObject.GetComponent<MonsterScript>().myRoom.GetComponent<RoomScript>().roomMembers.Remove(otherObject.gameObject);
        }
        isHoldingObject = true;
        heldObject = otherObject;
        otherObject.SetActive(true);
    }

    public void NewCycle()
    {
        doingSetup = false;
        cycleImage.SetActive(false);
        cycleTimer = 100f;
    }

    public void ToggleConstruction()
    {
        inConstructionMode = !inConstructionMode;
        heresTheFuckingButton.SetActive(inConstructionMode);
    }
}
