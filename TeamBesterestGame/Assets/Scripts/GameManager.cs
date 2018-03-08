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

	//Aggregate Stress Stuff
    public Image stressImage;

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

	//construction stuff
	public bool inConstructionMode;

	public GameObject constructionButton;
	public GameObject roomButton;

	//interviewing stuff
	public GameObject interviewButtons;
	public GameObject interviewImage;
	public GameObject interviewBackground;
	public GameObject interviewExit;

	public GameObject spawnRoom;
	public GameObject[] heroes;

	public GameObject[,] roomList;

	//Time Unit stuff
	private bool paused = true;
	public int timePerDay = 16;
	private int currentTime;
	public float timeSpeed = 3.0f;
	public Text timeUnitText;
	public Text pauseButtonText;

	//infamy
	float infamyLevel = 1;
	float infamyXP = 0;
	int xpToNextInfamyLevel = 20;
	int baseXP = 20;
	public Text infamyLevelText;
	public Text infamyXPText;

	//spawn stuff
	public float baseHeroSpawnRate = 0.45f;
	public float spawnRateIncrement = 0.1f;
	private float modifiedHeroSpawnRate;
	public float peakHoursSpawnRateBonus = 0.25f; //how much is added on during peak hours
	public float peakHourStart = 0.5f;
	public float peakHourEnd = 1.0f;
	private bool peakHours = false;

	// Use this for initialization
	void Awake()
	{
		roomList = new GameObject[10, 10];
		currentCurrency = 5000;
		UpdateCurrency ();
		UpdateInfamy();
		CreateNewResume(3);

		modifiedHeroSpawnRate = baseHeroSpawnRate;

		currentTime = timePerDay;
        stressImage = GameObject.FindGameObjectWithTag("Aggregate Stress").GetComponent<Image>();
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

	//might combine this with creating a new monster and file the resume under the monsters as a child object
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
		currentTime = timePerDay;
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


        int salary = monsterInstance.GetComponent<MonsterScript>().requestedSalary;
        float infamyRaise = monsterInstance.GetComponent<MonsterScript>().infamyGain;
        if (currentCurrency >= salary)
        {
            CurrencyChanged(-salary); //this will have to change when the monster inheritance class is set up
            IncreaseInfamyXP(monsterInstance.GetComponent<MonsterScript>().threatValue);

            currentResumes[activeResume].SetActive(false);
            currentResumes.Remove(currentResumes[activeResume]);

            PickUpObject(monsterInstance);
            resumeOpen = !resumeOpen;
        }

        UpdateStressMeter();
    }

	public void OpenApplications()
	{
		if (currentResumes.Count == 0)
			return;
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

			foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
			{
				monster.GetComponent<MonsterScript>().Attack();
			}

			foreach (GameObject hero in GameObject.FindGameObjectsWithTag("Hero"))
			{
				hero.GetComponent<HeroScript>().Attack();
				hero.GetComponent<HeroScript>().CheckCurrentRoom();
			}

            foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
            {
                room.GetComponent<RoomScript>().RoomMemeberHandler();
            }

            //This part modifies spawn rates during peak hours
            float currentRelativeTime = 1f - ((float)currentTime / timePerDay);
			if ( (currentRelativeTime >= peakHourStart) && (currentRelativeTime <= peakHourEnd) )
			{
				peakHours = true;

				if (modifiedHeroSpawnRate < (baseHeroSpawnRate + peakHoursSpawnRateBonus))
				{
					modifiedHeroSpawnRate = baseHeroSpawnRate + peakHoursSpawnRateBonus;
				}
			}
			else
			{
				peakHours = false;
			}

			//Spawn heroes; the longer it goes without spawn, the more likely spawn is
			if (modifiedHeroSpawnRate > Random.Range(0f, 1f))
			{
				Instantiate(heroes[Random.Range(0, heroes.Length)], spawnRoom.transform.position, Quaternion.identity);
				modifiedHeroSpawnRate = baseHeroSpawnRate;

				if (peakHours)
				{
					modifiedHeroSpawnRate += peakHoursSpawnRateBonus;
				}
			}
			else
			{
				modifiedHeroSpawnRate += spawnRateIncrement;
			}

			//This part checks all the resumes to see how long they have left
			foreach (GameObject resume in currentResumes)
			{
				resume.GetComponent<ResumeScript>().timeTillExpiration--;
				if (resume.GetComponent<ResumeScript>().timeTillExpiration <= 0)
					expiredResumes.Add(resume);
			}

			//This part removes expired resumes from the list of resumes before deleting them
			foreach (GameObject expiredResume in expiredResumes)
			{
				currentResumes.Remove(expiredResume);
				Destroy(expiredResume);
			}
			expiredResumes.Clear();

			//Potential of 1 new resume per time unit
			//May put some public variables here so chance and amount can be edited in editor
			if (Random.Range(0, 9) > 4)
				CreateNewResume(1);

            UpdateStressMeter();
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

	//changes value of currency and updates UI
	//Takes: integer (either positive or negative)
	public void CurrencyChanged(int value)
	{
		currentCurrency += value;
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
		this.gameObject.GetComponentInChildren<InterviewManager>().UpdateQuestions();
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

	//function to 'level up' player -> aka increase infamy level
	public void IncreaseInfamyXP(int characterValue)
	{
		int high = 25, low = 10;
		int divNum = Random.Range(low, high);
		int gain = characterValue / divNum;
		infamyXP += divNum;

		if (infamyXP >= xpToNextInfamyLevel)
		{
			infamyLevel += 1;
			infamyXP -= xpToNextInfamyLevel;
			xpToNextInfamyLevel = InfamyXPNeeded();
		}
		else
		{
			int toNext = (int)xpToNextInfamyLevel - (int)infamyXP;
			print("xp to next level " + toNext);
		}
		UpdateInfamy();
	}

	public int InfamyXPNeeded()
	{
		print("reached new level");
		float exponent = 1.5f;
		float xp = baseXP;
		int xpNeeded = Mathf.FloorToInt(xp * (Mathf.Pow(infamyLevel, exponent)));
		return xpNeeded;
	}

	public void UpdateInfamy()
	{
		infamyLevelText.text = "InfamyLevel: " + infamyLevel;
		infamyXPText.text = "InfamyXP: " + infamyXP + "/" + xpToNextInfamyLevel;
	}

    //aggregate stress calculating
    public void UpdateStressMeter()
    {
        float overallStressValue = 0;
        int monsterCount = 0;

        foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
        {
            MonsterScript m = monster.GetComponent<MonsterScript>();
            overallStressValue += m.stress;
            monsterCount++;
        }

        overallStressValue = overallStressValue / monsterCount;

        if (overallStressValue <= 10f)
        {
            stressImage.color = Color.green;
            //stressImage.color = new Color(135, 255, 0, 255);
        }
        else if (overallStressValue < 20f && overallStressValue > 10f)
        {
            stressImage.color = Color.green;
            //stressImage.color = new Color(135, 230, 0, 255);
        }
        else if (overallStressValue < 30f && overallStressValue > 20f)
        {
            stressImage.color = Color.green;
            //stressImage.color = new Color(135, 205, 0, 255);
        }
        else if (overallStressValue < 40f && overallStressValue > 30f)
        {
            stressImage.color = Color.green;
            //stressImage.color = new Color(135, 190, 0, 255);
        }
        else if (overallStressValue < 50f && overallStressValue > 40f)
        {
            stressImage.color = Color.yellow;
            //stressImage.color = new Color(135, 165, 0, 255);
        }
        else if (overallStressValue < 60f && overallStressValue > 50f)
        {
            stressImage.color = Color.yellow;
            //stressImage.color = new Color(135, 140, 0, 255);
        }
        else if (overallStressValue < 70f && overallStressValue > 60f)
        {
            stressImage.color = Color.yellow;
            //stressImage.color = new Color(135, 115, 0, 255);
        }
        else if (overallStressValue < 80f && overallStressValue > 70f)
        {
            stressImage.color = Color.red;
            //stressImage.color = new Color(135, 90, 0, 255);
        }
        else if (overallStressValue < 90f && overallStressValue > 80f)
        {
            stressImage.color = Color.red;
            //stressImage.color = new Color(135, 65, 0, 255);
        }
        else if (overallStressValue >= 90f)
        {
            stressImage.color = Color.red;
            //stressImage.color = new Color(135, 40, 0, 255);
        }
        else
        {
            stressImage.color = Color.green;
            //stressImage.color = new Color(135, 255, 0, 255);
        }

    }
}
