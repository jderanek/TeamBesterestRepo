using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [HideInInspector]
    public GameObject monsterInHand;
    public bool isHoldingObject;
    public GameObject monster;
    [HideInInspector]
    public GameObject monsterInstance;
    public GameObject heldObject;

    private GameObject resume;
    private GameObject resumeButton;

    //public List<GameObject> monsterCollection = new List<GameObject>();

    public bool doingSetup;
    public bool interviewing = false;

    public Slider cycleSlider;
    public GameObject cycleImage;
    public float cycleTimer = 100f;
    public float cycleDelay = 2f;

    public bool inConstructionMode;

    public GameObject heresTheFuckingButton;

    public GameObject heresTheOtherFuckingButtons;

    public GameObject spawnRoom;
    public GameObject[] heroes;

    private IEnumerator spawnHeroTimer;

    // Use this for initialization
    void Start()
    {
        var coroutine = SpawnHeroes(5f);
        StartCoroutine(coroutine);
    }

    // Update is called once per frame
    void Update()
    {
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
            heldObject = null;
        }
        //placement end

        cycleSlider.value = cycleTimer;

        if (cycleTimer >= 100)
        {
            doingSetup = true;
            cycleImage.SetActive(true);
            Invoke("NewCycle", 2);
        }

        else
        {
            //Time.timeScale = 1;
            cycleTimer += Time.deltaTime;
        }

        if (interviewing)
        {
            heresTheOtherFuckingButtons.SetActive(interviewing);
        }
    }

    public GameObject SpawnMonster(GameObject resume)
    {
        GameObject newMonster = Instantiate(monster, resume.transform.position, Quaternion.identity);
        newMonster.SetActive(false);
        return newMonster;

        //monsterCollection.Add (newMonster); // not working
        //Debug.Log ("monsterCollection Size = " + monsterCollection.Count);
    }

    public void PickUpObject(GameObject otherObject)
    {
        if (otherObject.GetComponent<MonsterScript>() != null ) {
            if (otherObject.GetComponent<MonsterScript>().myRoom != null)
            {
                otherObject.GetComponent<MonsterScript>().myRoom.GetComponent<RoomScript>().roomMembers.Remove(otherObject.gameObject);
                otherObject.GetComponent<MonsterScript>().myRoom = null;
            }
        }
        
        isHoldingObject = true;
        heldObject = otherObject;
        otherObject.SetActive(true);
    }

    public void NewCycle()
    {
        doingSetup = false;
        cycleImage.SetActive(false);
        cycleTimer = 0f;
    }

    public void HireButton()
    {
        monsterInstance = GameObject.FindGameObjectWithTag("ResumeButton").GetComponent<HiringUIScript>().monsterInstance;
        Destroy(GameObject.FindGameObjectWithTag("Resume")); //.SetActive(false);
        GameObject.Find("Hiring Button").GetComponent<HiringUIScript>().resumeUp = false;
        PickUpObject(monsterInstance);
    }

    private IEnumerator SpawnHeroes(float spawnTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            Instantiate(heroes[Random.Range(0, heroes.Length)], spawnRoom.transform.position, Quaternion.identity);
        }
    }

    public void ToggleConstruction()
    {
        inConstructionMode = !inConstructionMode;
        heresTheFuckingButton.SetActive(inConstructionMode);
    }

   

}
