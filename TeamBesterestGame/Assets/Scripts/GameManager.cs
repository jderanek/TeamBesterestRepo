using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	//Test Party
	public TestPart party;

	//Monster Stuff
	//[HideInInspector]
	public GameObject[] possibleMonsters; //public to assign references in editor
	//[HideInInspector]
	public GameObject monsterInstance; //public to assign reference in editor
    public GameObject selectedObject;
    public int healthWeight = 10;
    public int attackWeight = 3;
    public int defenseWeight = 2;

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
    public Font arial;

    public GameObject emptyField;
    public GameObject applicationPanel;
    public GameObject monsterPanel;
    public GameObject departmentPanel;
    public GameObject assignmentPanel;
    public GameObject breakRoomHeader;
    public GameObject prHeader;
    private int prCapacity = 3;
    public GameObject hrHeader;
    private int hrCapacity = 3;
    public GameObject emptySlotField;
    private bool applicationOpen = false;
    private bool monsterOpen = false;
    private bool departmentOpen = false;
    private bool assignmentOpen = false;
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
	public int roomCount = 1; //Number of rooms in dungeon

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
    public List<GameObject> prList = new List<GameObject>();
	public List<BaseParty> attackParties = new List<BaseParty> ();
    public List<GameObject> deadMonsters = new List<GameObject>(); //for the corporeally challenged

    //CSVImporter for Monsters and Heroes
    //public CSVImporter monsters = new CSVImporter(22, 9, "Monster_Stats_-_Sheet1.csv");
	public CSVImporter monsters;
	public CSVImporter monNames;
	public CSVImporter heroStats;

	// Use this for initialization
	void Awake()
	{
		roomList = new GameObject[10, 10];
		monsters = new CSVImporter(24, 11, "Monster_Stats_-_Sheet1.csv", 
			"https://docs.google.com/spreadsheets/d/e/2PACX-1vSBLCQyX37HLUhxOVtonHsR0S76lt2FzvDSeoAzPsB_TbQa43nR7pb6Ns5QeuaHwpIqun55JeEM8Llc/pub?gid=2027062354&single=true&output=csv");
		monNames = new CSVImporter(6, 7, "NamesWIP - Sheet1.csv",
			"https://docs.google.com/spreadsheets/d/e/2PACX-1vS3YmSZDNM2JAfk0jTir8mO4tq2Z_6SF7hPDmQvovd2G9Ld_dfFcDARmPQ2kB2hKYFSuupbD4oB2m7f/pub?gid=1640444901&single=true&output=csv");
		heroStats = new CSVImporter (7, 9, "Heroes - Sheet1.csv",
			"https://docs.google.com/spreadsheets/d/e/2PACX-1vROE5F1pcPZ65Zg5H5QsEqwpayjzcLOYQMffmv6E3zjR3tMq7kD68zPNGdrCXmq8w67wZHNNGwehsLo/pub?gid=0&single=true&output=csv");
        currentCurrency = 1500;
		UpdateCurrency ();
		UpdateInfamy();
		CreateNewResume(3);

		modifiedHeroSpawnRate = baseHeroSpawnRate;

		currentTime = timePerDay;
        stressImage = GameObject.FindGameObjectWithTag("Aggregate Stress").GetComponent<Image>();
    }

	void Start() {
		//var rooms = GameObject.FindGameObjectsWithTag("Room");
		/*
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

        //this was Avery's turning it off for now
        /*
		roomList [5, 5] = spawnRoom;
		for (int x = 0; x < roomList.GetLength (0); x++) {
			for (int y = 0; y < roomList.GetLength (1); y++) {
				if (roomList [x, y] != null)
					Debug.Log ("Room at: " + x.ToString () + ", " + y.ToString ());
			}
		}
        */
	}

	// Update is called once per frame
	void Update()
    {
	
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
		monsterInstance.GetComponent<BaseMonster>().applicationLife = -1;
        if (currentCurrency >= salary)
        {
            CurrencyChanged(-salary); //this will have to change when the monster inheritance class is set up
            IncreaseInfamyXP(monsterInstance.GetComponent<BaseMonster>().getThreat());

            //monsterInstance.GetComponent<BaseMonster>().setCurRoom(GameObject.FindGameObjectWithTag("Room"));
            //var curRoom = monsterInstance.GetComponent<BaseMonster>().getCurRoom();
            //monsterInstance.transform.position = new Vector3(curRoom.transform.position.x, curRoom.transform.position.y, 0f);

            monsterList.Add(monsterInstance);
            AddToDepartment(monsterInstance, breakRoomList);
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
            monsterOpen = false;
            monsterPanel.SetActive(false);
            departmentPanel.SetActive(false);
            departmentOpen = false;
            assignmentPanel.SetActive(false);
            assignmentOpen = false;

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
            newField.transform.GetChild(0).transform.GetChild(1).GetComponent<Image>().color = application.GetComponent<SpriteRenderer>().color;
            newField.GetComponentInChildren<Text>().text = application.name;
            newField.GetComponent<RectTransform>().sizeDelta = new Vector2(255f, 57.4f);

            //stats/interview button
            newField.transform.GetChild(0).transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { Interview(application); });
            //hire button
            newField.transform.GetChild(0).transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { HireButton(application, newField); });

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
            applicationPanel.SetActive(false);
            applicationOpen = false;
            departmentPanel.SetActive(false);
            departmentOpen = false;
            assignmentPanel.SetActive(false);
            assignmentOpen = false;

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
            if (!deadMonsters.Contains(monster))
            {
                //Create the field and set up its name and button functionality
                var newField = Instantiate(monsterField, new Vector3(0, 0, 0), Quaternion.identity);
                newField.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = monster.GetComponent<SpriteRenderer>().color;
                newField.GetComponentInChildren<Text>().text = monster.name;
                if (monster.GetComponent<BaseMonster>().department == breakRoomList)
                {
                    newField.transform.GetChild(0).transform.GetChild(3).GetComponentInChildren<Text>().text = "Assign";
                    newField.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectObject(monster); });
                }
                else
                {
                    newField.transform.GetChild(0).transform.GetChild(3).GetComponentInChildren<Text>().text = "Break Time!";
                    newField.GetComponentInChildren<Button>().onClick.AddListener(delegate
                    {
                        AddToDepartment(monster, breakRoomList);
                        monster.transform.position = new Vector3(0, 0, 0);
                        //TODO: NEED TO REMOVE MONSTER FROM ROOMS WHILE IN BREAK ROOM
                        //monster.GetComponent<BaseMonster>().setCurRoom(null);
                    });
                }
                newField.transform.SetParent(monsterPanel.transform, false);

                //manually adjust its position
                var newFieldCanvas = newField.transform.GetChild(0);
                newFieldCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
                newFieldCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
                newFieldCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            }            
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
			//GameObject[] toAdd = new GameObject[2];
			//toAdd[0] = Instantiate(heroes[1], spawnRoom.transform.position, Quaternion.identity);
			//toAdd[1] = Instantiate(heroes[1], spawnRoom.transform.position, Quaternion.identity);
			//party = new TestPart (toAdd);
			//attackParties.Add (party);

            applicationPanel.SetActive(false);
            applicationOpen = false;
            monsterPanel.SetActive(false);
            monsterOpen = false;
            assignmentPanel.SetActive(false);
            assignmentOpen = false;

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
        newBreakRoomHeader.transform.SetParent(departmentPanel.transform, true);
        newBreakRoomHeader.GetComponent<Image>().enabled = true;
        newBreakRoomHeader.GetComponentInChildren<Text>().enabled = true;

        foreach (GameObject monster in breakRoomList)
        {
            var newField = Instantiate(monsterField, new Vector3(0, 0, 0), Quaternion.identity);
            newField.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = monster.GetComponent<SpriteRenderer>().color;
            newField.GetComponentInChildren<Text>().text = monster.name;
            newField.GetComponentInChildren<Button>().onClick.AddListener(delegate {
                SelectObject(monster);
                //AddToDepartment(monster, dungeonList);
            });
            newField.transform.SetParent(departmentPanel.transform, false);

            //manually adjust its position
            var newFieldCanvas = newField.transform.GetChild(0);
            newFieldCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            newFieldCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            newFieldCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        }

        //Dont think that break room needs an assingment button but this code will make it happen lol
        /*
        var emptySlotBreakRoom = Instantiate(emptySlotField, new Vector3(0, 0, 0), Quaternion.identity);
        emptySlotBreakRoom.GetComponentInChildren<Button>().onClick.AddListener(delegate {  });
        emptySlotBreakRoom.transform.SetParent(departmentPanel.transform, false);

        var emptySlotBreakRoomCanvas = emptySlotBreakRoom.transform.GetChild(0);
        emptySlotBreakRoomCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        emptySlotBreakRoomCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
        emptySlotBreakRoomCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        */

        var newPRHeader = Instantiate(prHeader, new Vector3(0, 0, 0), Quaternion.identity);
        newPRHeader.GetComponent<RectTransform>().sizeDelta = new Vector2(255, 30);
        newPRHeader.transform.SetParent(departmentPanel.transform, false);
        newPRHeader.GetComponent<Image>().enabled = true;
        newPRHeader.GetComponentInChildren<Text>().enabled = true;

        foreach (GameObject monster in prList)
        {
            var newField = Instantiate(monsterField, new Vector3(0, 0, 0), Quaternion.identity);
            newField.GetComponentInChildren<Text>().text = monster.name;
            newField.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = monster.GetComponent<SpriteRenderer>().color;
            newField.GetComponentInChildren<Button>().onClick.AddListener(delegate { AddToDepartment(monster, breakRoomList); });
            newField.transform.GetChild(0).transform.GetChild(3).transform.GetComponentInChildren<Text>().text = "Remove";
            newField.transform.SetParent(departmentPanel.transform, false);

            var newFieldCanvas = newField.transform.GetChild(0);
            newFieldCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            newFieldCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            newFieldCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        }

        if (prList.Count < prCapacity)
        {
            var emptySlotPR = Instantiate(emptySlotField, new Vector3(0, 0, 0), Quaternion.identity);
            emptySlotPR.GetComponentInChildren<Button>().onClick.AddListener(delegate
            {
                UpdateAssignment(prList);
                AssignmentMenu();
            });
            emptySlotPR.transform.SetParent(departmentPanel.transform, false);
            emptySlotPR.GetComponent<Image>().enabled = true;

            var emptySlotPRCanvas = emptySlotPR.transform.GetChild(0);
            emptySlotPRCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            emptySlotPRCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            emptySlotPRCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
            emptySlotPRCanvas.GetComponent<Canvas>().enabled = true;
            emptySlotPRCanvas.GetComponent<CanvasScaler>().enabled = true;
            emptySlotPRCanvas.GetComponent<GraphicRaycaster>().enabled = true;
        }
    }

    public void AssignmentMenu()
    {
        if (!assignmentOpen)
        {
            applicationPanel.SetActive(false);
            applicationOpen = false;
            departmentPanel.SetActive(false);
            departmentOpen = false;
            monsterPanel.SetActive(false);
            monsterOpen = false;

            assignmentPanel.SetActive(true);
            assignmentOpen = true;
        }
    }

    public void UpdateAssignment(List<GameObject> department)
    {
        //reset panel
        foreach (Transform child in assignmentPanel.transform)
        {
            Destroy(child.gameObject);
        }

        //create an empty object to create some space between the menu and the first Field
        var empty = Instantiate(new GameObject(), new Vector3(0, 0, 0), Quaternion.identity);
        empty.AddComponent<RectTransform>().sizeDelta = new Vector2(0, 20);
        empty.AddComponent<Text>().text = "Choose a Monster to assign";
        var emptyText = empty.GetComponent<Text>();
        emptyText.font = arial;
        emptyText.horizontalOverflow = HorizontalWrapMode.Overflow;
        emptyText.verticalOverflow = VerticalWrapMode.Overflow;
        emptyText.alignment = TextAnchor.MiddleCenter;
        empty.transform.SetParent(assignmentPanel.transform, false);

        //create a field for each Monster
        foreach (GameObject monster in breakRoomList) //NEED TO USE AN "INACTIVE" LIST FOR THIS LATER
        {
            //Create the field and set up its name and button functionality
            var newField = Instantiate(monsterField, new Vector3(0, 0, 0), Quaternion.identity);
            newField.GetComponentInChildren<Text>().text = monster.name;
            newField.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = monster.GetComponent<SpriteRenderer>().color;
            newField.GetComponentInChildren<Button>().onClick.AddListener(delegate 
            {
                AddToDepartment(monster, department);
                DepartmentMenu();
            });
            newField.transform.SetParent(assignmentPanel.transform, false);

            //manually adjust its position
            var newFieldCanvas = newField.transform.GetChild(0);
            newFieldCanvas.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            newFieldCanvas.GetComponent<RectTransform>().anchorMin = new Vector2(0.5f, 0.5f);
            newFieldCanvas.GetComponent<RectTransform>().anchorMax = new Vector2(0.5f, 0.5f);
        }
    }

    public void AddToDepartment(GameObject monster, List<GameObject> department)
    {
        if (monster.GetComponent<BaseMonster>().department != null) //causing an error when moving a monster to the dungeon
        {
            monster.GetComponent<BaseMonster>().department.Remove(monster);
        }
        department.Add(monster);
        monster.GetComponent<BaseMonster>().department = department;
        UpdateDepartments();
        UpdateMonsters();
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
			/*
			foreach (GameObject hero in GameObject.FindGameObjectsWithTag("Hero"))
			{
                dungeonEmpty = false;
				hero.GetComponent<BaseHero>().Attack();
				hero.GetComponent<BaseHero>().CheckCurrentRoom();
			}*/

			//Makes all parties attack their current room, and then check it
			foreach (BaseParty heroParty in attackParties) {
				dungeonEmpty = false;
				heroParty.AttackPhase ();
				heroParty.CheckRoom ();
			}
			//Trims list of deleted parties
			attackParties.RemoveAll(item => item.markedForDelete());

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
				//Instantiate(heroes[Random.Range(0, heroes.Length)], spawnRoom.transform.position, Quaternion.identity);
				modifiedHeroSpawnRate = baseHeroSpawnRate;

				//Creates a new party with a random hero and adds it to the party list
				//Temporary, until there are more party types
				GameObject[] newHero = new GameObject[1];
				newHero[0] = Instantiate(heroes[Random.Range(0, heroes.Length)], spawnRoom.transform.position, Quaternion.identity);
				BaseParty newParty = new TestPart (newHero);
				this.attackParties.Add (newParty);

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
				if (application.GetComponent<BaseMonster>().applicationLife > 0)
	                application.GetComponent<BaseMonster>().applicationLife -= 1;
                if (application.GetComponent<BaseMonster>().applicationLife == 0)
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

            foreach (GameObject monster in prList)
            {
                CurrencyChanged(monster.GetComponent<BaseMonster>().getBaseDamage());
                infamyXP += monster.GetComponent<BaseMonster>().getThreat();
                UpdateInfamy();
            }

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
		//party.MoveToNextRoom ();
	}

	public void ToggleConstruction()
	{
        if (inConstructionMode)
        {
            foreach (GameObject room in roomList)
            {
                if (room != null && room.GetComponent<RoomScript>() != null)
                {
                    room.GetComponent<RoomScript>().UpdateNeighbors();
                }
            }
        }
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

	public void Interview(GameObject monster)//enables interview UI and hides other UI elements that are in the way
	{
        monsterInstance = monster;
		interviewing = true;
        applicationPanel.SetActive(false);
        Q1.SetActive(true);
        Q2.SetActive(true);
        Q3.SetActive(true);
        Q4.SetActive(true);
        Q5.SetActive(true);
        interviewImage.SetActive(true);
        interviewResponse.SetActive(true);
		interviewExit.SetActive(true);


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
