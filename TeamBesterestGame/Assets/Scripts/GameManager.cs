using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //Monster Stuff
    [HideInInspector]
    public GameObject monsterInHand;
    public bool isHoldingObject;
    public GameObject[] possibleMonsters;
    [HideInInspector]
    public GameObject monsterInstance;
    public GameObject heldObject;

    //Resume Stuff
    public GameObject resume;
    public List<GameObject> currentResumes;
    public List<GameObject> expiredResumes;
    public int activeResume;
    public GameObject applicationsButton;
    public Transform resumeSpawn;
    private bool resumeOpen = false;

    //Money Stuff
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

    public GameObject[,] roomList;

    //Time Unit stuff
	private bool paused = true;
	private int currentTime = 100;
	public float timeSpeed = 3.0f;
	public Text timeUnitText;
	public Text pauseButtonText;

    // Use this for initialization
    void Awake()
    {
        roomList = new GameObject[10, 10];
		currentCurrency = 0;
		UpdateCurrency ();
        CreateNewResume(3);
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
        }*/
    }

    public GameObject SpawnMonster(GameObject resume)
    {
        GameObject newMonster = Instantiate(possibleMonsters[Random.Range(0, possibleMonsters.Length)], resume.transform.position, Quaternion.identity);
        newMonster.SetActive(false);
        return newMonster;
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

    public void CreateNewResume(int resumesToCreate)
    {
        for (int i = 0; i < resumesToCreate; i++)
        {
            currentResumes.Add(Instantiate(resume, resumeSpawn.position, Quaternion.identity));
            var thisResume = currentResumes[currentResumes.Count - 1];

            monsterInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SpawnMonster(thisResume);
            thisResume.GetComponent<ResumeScript>().monster = monsterInstance;
            monsterInstance.SetActive(false);
            var monsterInstanceScript = monsterInstance.GetComponent<MonsterScript>();

            //resume pictures
            thisResume.transform.Find("Resume Picture").transform.Find("Resume Image box").GetComponent<SpriteRenderer>().sortingLayerName = "Resume";
            thisResume.transform.Find("Resume Picture").transform.Find("Resume Image box").transform.Find("enemy image").GetComponent<SpriteRenderer>().sortingLayerName = "Resume";

            // Resume Text
            thisResume.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(0).GetComponent<Text>().text = monsterInstanceScript.monsterName;
            thisResume.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(2).GetComponent<Text>().text = monsterInstanceScript.monsterType;
            thisResume.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(4).GetComponent<Text>().text = "Average Health " + monsterInstanceScript.averageHealth;
            thisResume.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(5).GetComponent<Text>().text = "Average Damage " + monsterInstanceScript.averageDamage;
            thisResume.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(6).GetComponent<Text>().text = "Requested Salary: $" + monsterInstanceScript.requestedSalary;

            thisResume.SetActive(false);
        }
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
        Destroy(currentResumes[activeResume]);

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

            foreach (GameObject resume in currentResumes)
            {
                resume.GetComponent<ResumeScript>().timeTillExpiration--;
                if (resume.GetComponent<ResumeScript>().timeTillExpiration <= 0)
                    expiredResumes.Add(resume);
            }

            foreach (GameObject expiredResume in expiredResumes)
            {
                currentResumes.Remove(expiredResume);
                Destroy(expiredResume);
            }

            if (Random.Range(0, 9) > 4)
                CreateNewResume(1);

            expiredResumes.Clear();
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

	public void Interview()//enables interview UI and hides other UI elements that are in the way
	{
		interviewing = true;
		interviewButtons.SetActive(true);
		interviewImage.SetActive(true);
		interviewBackground.SetActive(true);
		interviewExit.SetActive(true);
		constructionButton.SetActive(false);
		applicationsButton.SetActive(false);

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
