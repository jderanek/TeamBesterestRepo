using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [HideInInspector]
    public GameObject monsterInHand;
    public bool isHoldingObject;
    public GameObject[] possibleMonsters;
    //public GameObject monster;
    [HideInInspector]
    public GameObject monsterInstance;
    public GameObject heldObject;

    public GameObject resume;
    public List<GameObject> currentResumes;
    public int activeResume;
    private GameObject resumeButton;
    public Transform resumeSpawn;
    private bool resumeOpen = false;

    //public List<GameObject> monsterCollection = new List<GameObject>();

	public Text currencyText;
	public int currentCurrency;

    public bool doingSetup;
    public bool interviewing = false;

    public Slider cycleSlider;
    public GameObject cycleImage;
    public float cycleTimer = 100f;
    public float cycleDelay = 2f;

	//Day counter to increase week
	public int days = 0;

    public bool inConstructionMode;

    public GameObject constructionButton;
    public GameObject roomButton;

    public GameObject interviewButtons;
    public GameObject interviewImage;
    public GameObject interviewBackground;
    public GameObject interviewExit;

    public GameObject spawnRoom;
    public GameObject[] heroes;

    private IEnumerator spawnHeroTimer;

    public GameObject[,] roomList;

	private bool paused = true;
	private int currentTime = 100;
	public float timeSpeed = 3.0f;
	public Text timeUnitText;
	public Text pauseButtonText;

    // Use this for initialization
    void Awake()
    {
        //var coroutine = SpawnHeroes(5f);
        //StartCoroutine(coroutine);
        roomList = new GameObject[10, 10];
		currentCurrency = 0;
		UpdateCurrency ();
        currentResumes.Add(Instantiate(resume, resumeSpawn.position, Quaternion.identity));
        currentResumes.Add(Instantiate(resume, resumeSpawn.position, Quaternion.identity));
        currentResumes.Add(Instantiate(resume, resumeSpawn.position, Quaternion.identity));
        foreach (GameObject resume in currentResumes)
        {
            monsterInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SpawnMonster(resume);
            resume.GetComponent<ResumeScript>().monster = monsterInstance;
            monsterInstance.SetActive(false);
            var monsterInstanceScript = monsterInstance.GetComponent<MonsterScript>();

            //resume pictures
            resume.transform.Find("Resume Picture").transform.Find("Resume Image box").GetComponent<SpriteRenderer>().sortingLayerName = "Resume";
            resume.transform.Find("Resume Picture").transform.Find("Resume Image box").transform.Find("enemy image").GetComponent<SpriteRenderer>().sortingLayerName = "Resume";

            // Resume Text
            resume.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(0).GetComponent<Text>().text = monsterInstanceScript.monsterName;
            resume.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(2).GetComponent<Text>().text = monsterInstanceScript.monsterType;
            resume.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(4).GetComponent<Text>().text = "Average Health " + monsterInstanceScript.averageHealth;
            resume.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(5).GetComponent<Text>().text = "Average Damage " + monsterInstanceScript.averageDamage;
            resume.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(6).GetComponent<Text>().text = "Requested Salary: $" + monsterInstanceScript.requestedSalary;

            resume.SetActive(false);
        }
    }

	void Start() {
		var rooms = GameObject.FindGameObjectsWithTag("Room");
		foreach (var room in rooms) {
			room.GetComponent<RoomScript>().Initialize();
		}
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

		/*
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
*/

        if (interviewing)
        {
            interviewButtons.SetActive(true);
            interviewImage.SetActive(true);
            interviewBackground.SetActive(true);
            interviewExit.SetActive(true);
            constructionButton.SetActive(false);
            if (resume != null)
            {
                resume.SetActive(false);
            }
        }
        else
        {
            interviewButtons.SetActive(false);
            interviewImage.SetActive(false);
            interviewBackground.SetActive(false);
            interviewExit.SetActive(false);
            constructionButton.SetActive(true);
            if (resume != null)
            {
                resume.SetActive(true);
            }
        }
    }

    public GameObject SpawnMonster(GameObject resume)
    {
        GameObject newMonster = Instantiate(possibleMonsters[Random.Range(0, possibleMonsters.Length)], resume.transform.position, Quaternion.identity);
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
                otherObject.GetComponent<MonsterScript>().myRoom.GetComponent<RoomScript>().roomThreat -= otherObject.GetComponent<MonsterScript>().threatValue;
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

		//Script to run dayhandler and weekhandler
		days += 1;
		DayHandler();
		if (days == 7) {
			WeekHandler();
			days = 0;
		}
    }

    public void HireButton()
    {
        monsterInstance = currentResumes[activeResume].GetComponent<ResumeScript>().monster;
        currentResumes[activeResume].SetActive(false);
        //monsterInstance = GameObject.FindGameObjectWithTag("ResumeButton").GetComponent<HiringUIScript>().monsterInstance;
        //Destroy(GameObject.FindGameObjectWithTag("Resume")); //.SetActive(false);
        //GameObject.Find("Hiring Button").GetComponent<HiringUIScript>().resumeUp = false;

        PickUpObject(monsterInstance);
    }

    public void OpenApplications()
    {
        resumeOpen = !resumeOpen;
        if (resumeOpen)
        {
            currentResumes[0].SetActive(true);
            activeResume = 0;
        }
        else
            currentResumes[activeResume].SetActive(false);
    }

    public void NextApplication()
    {
        if (activeResume < currentResumes.Count - 1)
        {
            currentResumes[activeResume].SetActive(false);
            activeResume++;
            currentResumes[activeResume].SetActive(true);
        }
        else
        {
            currentResumes[activeResume].SetActive(false);
            activeResume = 0;
            currentResumes[activeResume].SetActive(true);
        }
    }

    public void PreviousApplication()
    {
        if (activeResume > 0)
        {
            currentResumes[activeResume].SetActive(false);
            activeResume--;
            currentResumes[activeResume].SetActive(true);
        }
        else
        {
            currentResumes[activeResume].SetActive(false);
            activeResume = currentResumes.Count - 1;
            currentResumes[activeResume].SetActive(true);
        }
        
    }

	/*
    private IEnumerator SpawnHeroes(float spawnTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            Instantiate(heroes[Random.Range(0, heroes.Length)], spawnRoom.transform.position, Quaternion.identity);
        }
    }
    */

	private void SpawnHeroes(float spawnTime)
	{
		//Instantiate(heroes[Random.Range(0, heroes.Length)], spawnRoom.transform.position, Quaternion.identity);
	}

	public void TogglePlay() {
		if (paused) {
			var coroutine = Play ();
			StartCoroutine (coroutine);
			pauseButtonText.text = "Pause";

		} else {
			StopAllCoroutines();
			pauseButtonText.text = "Play";
		}
		paused = !paused;
		print (paused);
	}

	public IEnumerator Play() {
		while (true) {
			yield return new WaitForSeconds(timeSpeed);
			PassTime (1);
		}
	}

	public void PassTime(int timeToPass) {
        for (int i = timeToPass; i > 0; i--)
        {
            print("tick tock");
            currentTime--;
            timeUnitText.text = currentTime.ToString();

            foreach (GameObject Monster in GameObject.FindGameObjectsWithTag("Monster"))
            {
                Monster.GetComponent<MonsterScript>().Attack();
            }
            foreach (GameObject Hero in GameObject.FindGameObjectsWithTag("Hero"))
            {
                Hero.GetComponent<HeroScript>().Attack();
                Hero.GetComponent<HeroScript>().CheckCurrentRoom();
            }
            Instantiate(heroes[Random.Range(0, heroes.Length)], spawnRoom.transform.position, Quaternion.identity);
        }
    }

    public void ToggleConstruction()
    {
        inConstructionMode = !inConstructionMode;
        roomButton.SetActive(inConstructionMode);

        foreach (GameObject room in roomList)
        {
           if (room != null)
            {
               room.GetComponent<RoomScript>().ActivateButtons(inConstructionMode);
            }
        }
    }


	public void GoldGainedOnDeath (int goldValue)
	{
		currentCurrency += goldValue;
		UpdateCurrency();
	}

	public void UpdateCurrency()
	{
		currencyText.text = "Gold: " + currentCurrency;
	}


	//New day handler to apply effects on start of day
	public void DayHandler() {
		foreach (GameObject room in roomList) {
			foreach (GameObject monster in room.GetComponent<RoomScript>().roomMembers) {
				MonsterScript monScript = monster.GetComponent<MonsterScript> ();

				if (monScript != null) {
					if (monScript.personality != null) {
						monScript.personality.ApplyDayEffects (monScript);
					}
				}
			}
		}
	}

	//New week handler to apply effects on start of week
	public void WeekHandler() {
		foreach (GameObject room in roomList) {
			foreach (GameObject monster in room.GetComponent<RoomScript>().roomMembers) {
				MonsterScript monScript = monster.GetComponent<MonsterScript> ();

				if (monScript != null) {
					if (monScript.personality != null) {
						monScript.personality.ApplyWeekEffects (monScript);
					}
				}

				monScript.hasFought = false;
			}
		}
	}
}
