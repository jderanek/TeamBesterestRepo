using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	//Monster Stuff
	//[HideInInspector]
	public GameObject[] possibleMonsters; //public to assign references in editor
	[HideInInspector]
	public GameObject monsterInstance; //public to assign reference in editor
    public GameObject selectedObject;

	//Aggregate Stress Stuff
    public Image stressImage; //public to assign reference in editor

	//Money Stuff
	public Text currencyText; //public to assign reference in editor
	public int currentCurrency; //public to be accessed by other scripts
    public int maximumCurrency = 1500; //public to be edited in editor

	private bool doingSetup; 
	public bool interviewing = false; //public to be accessed in interview script

    //Day counter to increase week
    private int days = 0;

	//construction stuff
	public bool inConstructionMode; //public for now, pickup room script can be replaced

	//interviewing stuff
	public GameObject interviewButtons; //public to be assigned in editor
    public GameObject Q1;
    public GameObject Q2;
    public GameObject Q3;
    public GameObject Q4;
    public GameObject Q5;
    public GameObject interviewResponse;//public to be assigned in editor
    public GameObject interviewImage; //public to be assigned in editor
    public GameObject interviewBackground; //public to be assigned in editor
    public GameObject interviewExit; //public to be assigned in editor

    //UI stuff
    public Canvas canvas;

    public GameObject emptyField;
    public GameObject applicationPanel;
    public GameObject monsterPanel;
    public GameObject departmentPanel;
    public GameObject breakRoomHeader;
    public GameObject prHeader;
    public GameObject hrHeader;
    private bool applicationOpen = false;
    private bool monsterOpen = false;
    private bool departmentOpen = false;
    public GameObject applicationField;
    public GameObject monsterField;

    private bool pauseMenuOpen = false;
    public GameObject pauseMenu;
    private List<GameObject> applicationsList = new List<GameObject>();
    public GameObject applicantButton;
    public GameObject constructionButton; //public to be assigned in editor
    public GameObject roomButton; //public to be assigned in editor

    public GameObject spawnRoom; //public to be assigned in editor //can assign using tag later
    public GameObject bossRoom; //public to be assigned in editor //can assign using tag later
    public GameObject[] heroes; //public to be assigned in editor

    public GameObject[,] roomList; //public to be accessed by room script

	//Time Unit stuff
	private bool paused = true;
	public int timePerDay = 16; //public to be edited it editor
	private int currentTime;
	public float timeSpeed = 3.0f; //public to be edited in editor
	public Text timeUnitText; //public to be assigned in editor
    public Text pauseButtonText; //public to be assigned in editor
    private bool dungeonEmpty = false;

    //infamy
    private float infamyLevel = 1;
	private float infamyXP = 0;
	private int xpToNextInfamyLevel = 20;
	private int baseXP = 20;
	public Text infamyLevelText; //public to be assigned in editor
	public Text infamyXPText; //public to be assigned in editor

    //spawn stuff
    public float baseHeroSpawnRate = 0.25f; //public to be edited in editor
	public float spawnRateIncrement = 0.1f; //public to be assigned in editor
    private float modifiedHeroSpawnRate;
	public float peakHoursSpawnRateBonus = 0.25f; //how much is added on during peak hours
	public float peakHourStart = 0.5f; //public to be assigned in editor
    public float peakHourEnd = 1.0f; //public to be assigned in editor
    private bool peakHours = false;

    //WHOLE BUNCH OF FUCKING LISSSTTSSSTSTTST
    public List<GameObject> monsterList = new List<GameObject>();
    public List<GameObject> breakRoomList = new List<GameObject>();
    public List<GameObject> dungeonList = new List<GameObject>();

    //CSVImporter for Monsters and Heroes
    //public CSVImporter monsters = new CSVImporter(22, 9, "Monster_Stats_-_Sheet1.csv");
	public CSVImporter monsters;
	public CSVImporter monNames;
	public CSVImporter heroStats = new CSVImporter(7, 7, "Heroes - Sheet1.csv");

	// Use this for initialization
	void Awake()
	{
		roomList = new GameObject[10, 10];
		monsters = new CSVImporter(24, 11, "Monster_Stats_-_Sheet1.csv", 
			"https://docs.google.com/spreadsheets/d/e/2PACX-1vSBLCQyX37HLUhxOVtonHsR0S76lt2FzvDSeoAzPsB_TbQa43nR7pb6Ns5QeuaHwpIqun55JeEM8Llc/pub?gid=2027062354&single=true&output=csv");
		monNames = new CSVImporter(6, 7, "NamesWIP - Sheet1.csv",
			"https://docs.google.com/spreadsheets/d/e/2PACX-1vS3YmSZDNM2JAfk0jTir8mO4tq2Z_6SF7hPDmQvovd2G9Ld_dfFcDARmPQ2kB2hKYFSuupbD4oB2m7f/pub?gid=1640444901&single=true&output=csv");
        currentCurrency = 1500;
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


		/*
		//Test for CSVImporter
		foreach (KeyValuePair<string, Dictionary<string, string>> monster in monsters.data) {
			print (monster.Key);
			foreach (KeyValuePair<string, string> entry in monster.Value) {
				print (entry.Key + ": " + entry.Value);
			}
		}
		foreach (KeyValuePair<string, Dictionary<string, string>> hero in heroStats.data) {
			print (hero.Key);
			foreach (KeyValuePair<string, string> entry in hero.Value) {
				print (entry.Key + ": " + entry.Value);
			}
		}
		foreach (KeyValuePair<string, Dictionary<string, string>> name in monNames.data) {
			print (name.Key);
			foreach (KeyValuePair<string, string> entry in name.Value) {
				print (entry.Key + ": " + entry.Value);
			}
		}
		*/
	}

	// Update is called once per frame
	void Update()
    {
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

	public GameObject SpawnMonster()
	{
		GameObject monsterPrefab = possibleMonsters [Random.Range (0, possibleMonsters.Length)];
		GameObject newMonster = Instantiate(monsterPrefab, new Vector3(0,0,0), Quaternion.identity);
		newMonster.GetComponent<BaseMonster> ().AssignStats (monsterPrefab.name);
		newMonster.SetActive(false);
		return newMonster;
	}

    public void SelectObject(GameObject otherObject)
    {
        selectedObject = otherObject;
    }

    public void MoveMonster()
    {

    }

	//might combine this with creating a new monster and file the resume under the monsters as a child object
	public void CreateNewResume(int resumesToCreate)
	{
		for (int i = 0; i < resumesToCreate; i++)
		{
			monsterInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SpawnMonster();
            applicationsList.Add(monsterInstance);
            monsterInstance.SetActive(false);
		}
        UpdateApplications();
	}

    public void HireButton(GameObject monsterInstance, GameObject monsterApplicationField)
    {
        //monsterInstance = currentResumes[activeResume].GetComponent<ResumeScript>().monster;
        Destroy(monsterApplicationField);
        monsterInstance.SetActive(true);
        int salary = monsterInstance.GetComponent<BaseMonster>().getSalary();
        float infamyRaise = monsterInstance.GetComponent<BaseMonster>().getInfamyGain();
        if (currentCurrency >= salary)
        {
            CurrencyChanged(-salary); //this will have to change when the monster inheritance class is set up
            IncreaseInfamyXP(monsterInstance.GetComponent<BaseMonster>().getThreat());

            //monsterInstance.GetComponent<BaseMonster>().setCurRoom(GameObject.FindGameObjectWithTag("Room"));
            //var curRoom = monsterInstance.GetComponent<BaseMonster>().getCurRoom();
            //monsterInstance.transform.position = new Vector3(curRoom.transform.position.x, curRoom.transform.position.y, 0f);

            monsterList.Add(monsterInstance);
            breakRoomList.Add(monsterInstance);
            UpdateMonsters();
            UpdateDepartments();
            UpdateStressMeter();
        }
    }

    //Opens the Applications Panel
    public void ApplicationsMenu()
    {
        if (applicationOpen)
        {
            applicationOpen = false;
            applicationPanel.SetActive(false);
        }
        else
        {
            if (monsterOpen)
            {
                monsterOpen = false;
                monsterPanel.SetActive(false);
            }
            if (departmentOpen)
            {
                departmentPanel.SetActive(false);
                departmentOpen = false;
            }
            applicationOpen = true;
            applicationPanel.SetActive(true);
        }
    }

    //function should be called whenever the applicationList is changed
    public void UpdateApplications()
    {
        //reset the panel
        foreach (Transform child in applicationPanel.transform)
        {
            Destroy(child.gameObject);
        }

        //Add a blank object at the top of the panel to create some space bewtween the menu and the first field
        var empty = Instantiate(new GameObject(), new Vector3(0, 0, 0), Quaternion.identity);
        empty.AddComponent<RectTransform>().sizeDelta = new Vector2(0,0);
        empty.transform.SetParent(applicationPanel.transform, false);

        //Create a field for each application pending
        foreach (GameObject application in applicationsList)
        {
            //Create new field in the Application Panel and set up its Name, size, position, and button function
            var newField = Instantiate(applicationField, new Vector3(0, 0, 0), Quaternion.identity);
            newField.GetComponentInChildren<Text>().text = application.name;
            newField.GetComponent<RectTransform>().sizeDelta = new Vector2(255f, 57.4f);
            newField.transform.GetComponentInChildren<Button>().onClick.AddListener(delegate { HireButton(application, newField); });
            newField.transform.SetParent(applicationPanel.transform, false);

            //manually adjust its position
            var newFieldCanvas = newField.transform.GetChild(0);
            newFieldCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            newFieldCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            newFieldCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        }
    }

    //opens the Monster Panel
    public void MonsterMenu()
    {
        if (monsterOpen)
        {
            monsterOpen = false;
            monsterPanel.SetActive(false);
        }
        else
        {
            if (applicationOpen)
            {
                applicationPanel.SetActive(false);
                applicationOpen = false;
            }
            if (departmentOpen)
            {
                departmentPanel.SetActive(false);
                departmentOpen = false;
            }
            monsterOpen = true;
            monsterPanel.SetActive(true);
        }
    }

    //function should be called whenever the monsterList is changed
    public void UpdateMonsters()
    {
        //reset panel
        foreach (Transform child in monsterPanel.transform)
        {
            Destroy(child.gameObject);
        }

        //create an empty object to create some space between the menu and the first Field
        var empty = Instantiate(new GameObject(), new Vector3(0, 0, 0), Quaternion.identity);
        empty.AddComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        empty.transform.SetParent(monsterPanel.transform, false);

        //create a field for each Monster
        foreach (GameObject monster in monsterList)
        {
            //Create the field and set up its name and button functionality
            var newField = Instantiate(monsterField, new Vector3(0, 0, 0), Quaternion.identity);
            newField.GetComponentInChildren<Text>().text = monster.name;
            newField.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectObject(monster); });
            newField.transform.SetParent(monsterPanel.transform, false);

            //manually adjust its position
            var newFieldCanvas = newField.transform.GetChild(0);
            newFieldCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            newFieldCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            newFieldCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        }
    }

    public void DepartmentMenu()
    {
        if (departmentOpen)
        {
            departmentOpen = false;
            departmentPanel.SetActive(false);
        }
        else
        {
            if (applicationOpen)
            {
                applicationPanel.SetActive(false);
                applicationOpen = false;
            }
            if (monsterOpen)
            {
                monsterPanel.SetActive(false);
                monsterOpen = false;
            }
            departmentOpen = true;
            departmentPanel.SetActive(true);
        }
    }

    public void UpdateDepartments()
    {
        foreach (Transform child in departmentPanel.transform)
        {
            Destroy(child.gameObject);
        }

        var empty = Instantiate(new GameObject(), new Vector3(0, 0, 0), Quaternion.identity);
        empty.AddComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
        empty.transform.SetParent(departmentPanel.transform, false);

        var newBreakRoomHeader = Instantiate(breakRoomHeader, new Vector3(0, 0, 0), Quaternion.identity);
        newBreakRoomHeader.GetComponent<RectTransform>().sizeDelta = new Vector2(255, 30);
        newBreakRoomHeader.transform.SetParent(departmentPanel.transform, false);

        foreach (GameObject monster in breakRoomList)
        {
            print("hi");
            var newField = Instantiate(monsterField, new Vector3(0, 0, 0), Quaternion.identity);
            newField.GetComponentInChildren<Text>().text = monster.name;
            newField.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectObject(monster); });
            newField.transform.SetParent(departmentPanel.transform, false);

            //manually adjust its position
            var newFieldCanvas = newField.transform.GetChild(0);
            newFieldCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            newFieldCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            newFieldCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        }

        var newPRHeader = Instantiate(prHeader, new Vector3(0, 0, 0), Quaternion.identity);
        newPRHeader.GetComponent<RectTransform>().sizeDelta = new Vector2(255, 30);
        newPRHeader.transform.SetParent(departmentPanel.transform, false);


    }

    public void NewCycle()
	{
		currentTime = timePerDay;
		doingSetup = false;
		//cycleImage.SetActive(false);
		//cycleTimer = 0f;

		//Script to run dayhandler and weekhandler
		days += 1;
		DayHandler();
		if (days == 7) {
			WeekHandler();
			days = 0;
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

    //Master function for anything that changes when a time-unit passes
	public void PassTime(int timeToPass) {
		for (int i = timeToPass; i > 0; i--)
		{
            dungeonEmpty = true;
            if (currentTime > 0)
            {
                currentTime--;
                timeUnitText.text = currentTime.ToString();
            }

            //need to swap this out with a dungeon list
			foreach (GameObject monster in monsterList)
			{
				monster.GetComponent<BaseMonster>().Attack();
			}

            //probably should create a hero list
			foreach (GameObject hero in GameObject.FindGameObjectsWithTag("Hero"))
			{
                dungeonEmpty = false;
				hero.GetComponent<BaseHero>().Attack();
				hero.GetComponent<BaseHero>().CheckCurrentRoom();
			}

            //probabl should create a room list
            foreach (GameObject room in GameObject.FindGameObjectsWithTag("Room"))
            {
                room.GetComponent<RoomScript>().RoomMemeberHandler();
            }
            spawnRoom.GetComponent<RoomScript>().RoomMemeberHandler();
            bossRoom.GetComponent<RoomScript>().RoomMemeberHandler();

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
			if (modifiedHeroSpawnRate > Random.Range(0f, 1f) && currentTime > 0)
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

            //temp list for making changes to applicationList
            List<GameObject> applicationsToDestroy = new List<GameObject>();
            //marks apps to destroy later
            foreach (GameObject application in applicationsList)
            {
                application.GetComponent<MonsterScript>().applicationLife -= 1;
                if (application.GetComponent<MonsterScript>().applicationLife <= 0)
                {
                    applicationsToDestroy.Add(application);
                }
                
            }

            //destroys marked apps
            foreach (GameObject application in applicationsToDestroy)
            {
                applicationsList.Remove(application);
                Destroy(application);
            }
            //refresh application panel
            UpdateApplications();

            //Potential of 1 new resume per time unit
            //May put some public variables here so chance and amount can be edited in editor
            if (Random.Range(0, 9) > 4)
				CreateNewResume(1);

            UpdateStressMeter();

            if (currentTime <= 0 && dungeonEmpty)
            {
                NewCycle();
            }
		}
	}

	public void ToggleConstruction()
	{
		inConstructionMode = !inConstructionMode;
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
		currencyText.text = "Gold: " + currentCurrency + " / " + maximumCurrency;
	}

	public void Interview()//enables interview UI and hides other UI elements that are in the way
	{
		interviewing = true;
        //interviewButtons.SetActive(true);
        Q1.SetActive(true);
        Q2.SetActive(true);
        Q3.SetActive(true);
        Q4.SetActive(true);
        Q5.SetActive(true);
        interviewImage.SetActive(true);
        interviewResponse.SetActive(true);
		//interviewBackground.SetActive(true);
		interviewExit.SetActive(true);
		//constructionButton.SetActive(false);
        //contextMenu.SetActive(false);
		//applicationsButton.SetActive(false);
		this.gameObject.GetComponentInChildren<InterviewManager>().UpdateQuestions();
	}

	//New day handler to apply effects on start of day
	public void DayHandler() {
        if (!paused)
        {
            TogglePlay();
        }
        currentTime = timePerDay;
        timeUnitText.text = currentTime.ToString();
    }

	//New week handler to apply effects on start of week
	public void WeekHandler() {
		foreach (GameObject room in roomList) {
			foreach (GameObject monster in room.GetComponent<RoomScript>().roomMembers) {
				BaseMonster monScript = monster.GetComponent<BaseMonster> ();

				if (monScript != null) {
					if (monScript.getTrait() != null) {
						monScript.personality.ApplyWeekEffects (monScript);
					}
				}

				monScript.setHasFought(false);
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
			//print("xp to next level " + toNext);
		}
		UpdateInfamy();
	}

	public int InfamyXPNeeded()
	{
		//print("reached new level");
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

    public void PauseMenu()
    {
        if (!pauseMenuOpen)
        {
            pauseMenuOpen = true;
            pauseMenu.SetActive(true);
        }
        else
        {
            pauseMenuOpen = false;
            pauseMenu.SetActive(false);
        }
    }

    //aggregate stress calculating
    public void UpdateStressMeter()
    {
        float overallStressValue = 0;
        int monsterCount = 0;

        foreach (GameObject monster in GameObject.FindGameObjectsWithTag("Monster"))
        {
            BaseMonster m = monster.GetComponent<BaseMonster>();
			overallStressValue += m.getStress();
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
