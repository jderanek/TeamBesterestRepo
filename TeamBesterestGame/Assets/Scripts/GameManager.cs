﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
	//Test Party
	public TestPart party;

	//Monster Stuff
	[HideInInspector]
	public GameObject[] possibleMonsters; //public to assign references in editor
	[HideInInspector]
	public GameObject monsterInstance; //public to assign reference in editor
    [HideInInspector]
    public GameObject selectedObject;
    public int healthWeight = 10;
    public int attackWeight = 3;
    public int defenseWeight = 2;

    //Aggregate Stress Stuff
    [HideInInspector]
    public Image stressImage; //public to assign reference in editor

	//Money Stuff
    //TODO getters and setters
	public Text currencyText; //public to assign reference in editor
	public int currentCurrency; //public to be accessed by other scripts
    public int maximumCurrency = 1500; //public to be edited in editor

    //infamy
    private float infamyLevel = 1;
    private float infamyXP = 0;
    private int xpToNextInfamyLevel = 20;
    private int baseXP = 20;
    public Text infamyLevelText; //public to be assigned in editor
    public Text infamyXPText; //public to be assigned in editor

    //Time Unit stuff
    private bool paused = true;
    public int timePerDay = 16; //public to be edited it editor
    private int currentTime;
    public float timeSpeed = 3.0f; //public to be edited in editor
    public Text timeUnitText; //public to be assigned in editor
    public Text pauseButtonText; //public to be assigned in editor
    private bool dungeonEmpty = false;

    //spawn stuff
    public float baseHeroSpawnRate = 0.25f; //public to be edited in editor
    public float spawnRateIncrement = 0.1f; //public to be assigned in editor
    private float modifiedHeroSpawnRate;
    public float peakHoursSpawnRateBonus = 0.25f; //how much is added on during peak hours
    public float peakHourStart = 0.5f; //public to be assigned in editor
    public float peakHourEnd = 1.0f; //public to be assigned in editor
    private bool peakHours = false;

    private bool doingSetup;
    //TODO getters and setters
    [HideInInspector]
    public bool interviewing = false; //public to be accessed in interview script

    //Day counter to increase week
    private int days = 0;

    //construction stuff
    //TODO getters and setters
    [HideInInspector]
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

    public GameObject roomMenu;
    public Button roomMenuConfirm;
    public Button roomMenuCancel;
    private bool roomMenuOpen = false;
    private int roomOptionSelected;

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

    //WHOLE BUNCH OF FUCKING LISSSTTSSSTSTTST
    [HideInInspector]
    public List<GameObject> monsterList = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> breakRoomList = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> dungeonList = new List<GameObject>();
    [HideInInspector]
    public List<GameObject> prList = new List<GameObject>();
    [HideInInspector]
    public List<BaseParty> attackParties = new List<BaseParty> ();
    [HideInInspector]
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
			monsterInstance = SpawnMonster();
            applicationsList.Add(monsterInstance);
            monsterInstance.SetActive(false);
		}
        UpdateApplications();
	}

    public void HireButton(GameObject monsterInstance, GameObject monsterApplicationField)
    {
        //monsterInstance = currentResumes[activeResume].GetComponent<ResumeScript>().monster;        
        int salary = monsterInstance.GetComponent<BaseMonster>().getSalary();
        float infamyRaise = monsterInstance.GetComponent<BaseMonster>().getInfamyGain();
		monsterInstance.GetComponent<BaseMonster>().setApplicationLife(-1);
        if (currentCurrency >= salary)
        {
            Destroy(monsterApplicationField);
            monsterInstance.SetActive(true);
            CurrencyChanged(-salary); //this will have to change when the monster inheritance class is set up
            IncreaseInfamyXP(monsterInstance.GetComponent<BaseMonster>().getThreat());
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
        int childNum = 0;
        foreach (Transform child in applicationPanel.transform)
        {
            if (childNum != 0)
            {
                Destroy(child.gameObject);
            }
            childNum++;
        }

        //Create a field for each application pending
        foreach (GameObject application in applicationsList)
        {
            //Create new field in the Application Panel and set up its Name, size, position, and button function
            var newField = Instantiate(applicationField, new Vector3(0, 0, 0), Quaternion.identity);
            var newFieldCanvas = newField.transform.GetChild(0);
            var newFieldCanvasRect = newField.transform.GetChild(0).GetComponent<RectTransform>();

            newFieldCanvas.transform.GetChild(0).GetComponent<Image>().color = application.GetComponent<SpriteRenderer>().color;
            newField.GetComponentInChildren<Text>().text = application.name;
            newField.GetComponent<RectTransform>().sizeDelta = new Vector2(214.77f, 57.4f);

            //stats/interview button
            newFieldCanvas.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(delegate { Interview(application); });
            //hire button
            newFieldCanvas.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { HireButton(application, newField); });

            newField.transform.SetParent(applicationPanel.transform, false);

            //manually adjust its position           
            newFieldCanvasRect.anchoredPosition = new Vector2(0, 0);
            newFieldCanvasRect.anchorMin = new Vector2(0.5f, 0.5f);
            newFieldCanvasRect.anchorMax = new Vector2(0.5f, 0.5f);
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
        int childNum = 0;
        foreach (Transform child in monsterPanel.transform)
        { 
            if (childNum != 0)
            {
                Destroy(child.gameObject);
            }
            childNum++;
        }        

        //create a field for each Monster
        foreach (GameObject monster in monsterList)
        {
            if (!deadMonsters.Contains(monster))
            {
                //Create the field and set up its name and button functionality
                var newField = Instantiate(monsterField, new Vector3(0, 0, 0), Quaternion.identity);
                var newFieldCanvas = newField.transform.GetChild(0);
                var newFieldCanvasRect = newFieldCanvas.GetComponent<RectTransform>();
                newField.GetComponent<RectTransform>().sizeDelta = new Vector2(217.44f, 57.4f);
                newFieldCanvas.transform.GetChild(0).GetComponent<Image>().color = monster.GetComponent<SpriteRenderer>().color;
                newField.GetComponentInChildren<Text>().text = monster.name;
                if (monster.GetComponent<BaseMonster>().department == breakRoomList)
                {
                    newFieldCanvas.transform.GetChild(3).GetComponentInChildren<Text>().text = "Assign";
                    newField.GetComponentInChildren<Button>().onClick.AddListener(delegate { SelectObject(monster); });
                }
                else
                {
                    newFieldCanvas.transform.GetChild(3).GetComponentInChildren<Text>().text = "Break Time!";
                    newField.GetComponentInChildren<Button>().onClick.AddListener(delegate
                    {
                        monster.GetComponent<BaseMonster>().getCurRoom().GetComponent<RoomScript>().roomMembers.Remove(monster);
                        monster.GetComponent<BaseMonster>().setCurRoom(null);
                        AddToDepartment(monster, breakRoomList);
                        monster.transform.position = new Vector3(0, 0, 0);                        
                    });
                }
                newField.transform.SetParent(monsterPanel.transform, false);

                //manually adjust its position
                
                newFieldCanvasRect.anchoredPosition = new Vector2(0, 0);
                newFieldCanvasRect.anchorMin = new Vector2(0.5f, 0.5f);
                newFieldCanvasRect.anchorMax = new Vector2(0.5f, 0.5f);
            }            
        }
    }

    //Opens the Department Panel
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

    //Call this whenever you change the Department Panel
    public void UpdateDepartments()
    {
        //reset panel
        int childNum = 0;
        foreach (Transform child in departmentPanel.transform)
        { if (childNum != 0)
            {
                Destroy(child.gameObject);
            }
            childNum++;
        }

        var newBreakRoomHeader = Instantiate(breakRoomHeader, new Vector3(0, 0, 0), Quaternion.identity);
        newBreakRoomHeader.GetComponent<RectTransform>().sizeDelta = new Vector2(255, 30);
        newBreakRoomHeader.transform.SetParent(departmentPanel.transform, true);
        newBreakRoomHeader.GetComponent<Image>().enabled = true;
        newBreakRoomHeader.GetComponentInChildren<Text>().enabled = true;
        newBreakRoomHeader.GetComponentInChildren<Text>().text = "Unassigned";

        foreach (GameObject monster in breakRoomList)
        {
            var newField = Instantiate(monsterField, new Vector3(0, 0, 0), Quaternion.identity);
            var newFieldCanvas = newField.transform.GetChild(0);
            var newFieldCanvasRect = newFieldCanvas.GetComponent<RectTransform>();
            newFieldCanvas.transform.GetChild(0).GetComponent<Image>().color = monster.GetComponent<SpriteRenderer>().color;
            newField.GetComponentInChildren<Text>().text = monster.name;
            /*
            newField.GetComponentInChildren<Button>().onClick.AddListener(delegate {
                SelectObject(monster);
                //AddToDepartment(monster, dungeonList);
            });
            */
            newFieldCanvas.GetChild(3).gameObject.SetActive(false);
            newField.transform.SetParent(departmentPanel.transform, false);

            //manually adjust its position
            
            newFieldCanvasRect.anchoredPosition = new Vector2(0, 0);
            newFieldCanvasRect.anchorMin = new Vector2(0.5f, 0.5f);
            newFieldCanvasRect.anchorMax = new Vector2(0.5f, 0.5f);
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
        Text newPRHeaderText = newPRHeader.GetComponentInChildren<Text>();
        newPRHeaderText.enabled = true;
        newPRHeaderText.text = "Pillaging and Raiding " + prList.Count + "/3";

        foreach (GameObject monster in prList)
        {
            var newField = Instantiate(monsterField, new Vector3(0, 0, 0), Quaternion.identity);
            var newFieldCanvas = newField.transform.GetChild(0);
            var newFieldCanvasRect = newFieldCanvas.GetComponent<RectTransform>();
            newField.GetComponentInChildren<Text>().text = monster.name;
            newFieldCanvas.transform.GetChild(0).GetComponent<Image>().color = monster.GetComponent<SpriteRenderer>().color;
            newField.GetComponentInChildren<Button>().onClick.AddListener(delegate { AddToDepartment(monster, breakRoomList); });
            newFieldCanvas.transform.GetChild(3).transform.GetComponentInChildren<Text>().text = "Remove";
            newField.transform.SetParent(departmentPanel.transform, false);
                        
            newFieldCanvasRect.anchoredPosition = new Vector2(0, 0);
            newFieldCanvasRect.anchorMin = new Vector2(0.5f, 0.5f);
            newFieldCanvasRect.anchorMax = new Vector2(0.5f, 0.5f);
        }

        if (prList.Count < prCapacity)
        {
            var emptySlotPR = Instantiate(emptySlotField, new Vector3(0, 0, 0), Quaternion.identity);
            var emptySlotPRCanvas = emptySlotPR.transform.GetChild(0);
            var emptySlotPRCanvasRect = emptySlotPRCanvas.GetComponent<RectTransform>();
            emptySlotPR.GetComponentInChildren<Button>().onClick.AddListener(delegate
            {
                UpdateAssignment(prList);
                AssignmentMenu();
            });
            emptySlotPR.transform.SetParent(departmentPanel.transform, false);
            emptySlotPR.GetComponent<Image>().enabled = true;

            emptySlotPRCanvasRect.anchoredPosition = new Vector2(0, 0);
            emptySlotPRCanvasRect.anchorMin = new Vector2(0.5f, 0.5f);
            emptySlotPRCanvasRect.anchorMax = new Vector2(0.5f, 0.5f);
            emptySlotPRCanvas.GetComponent<Canvas>().enabled = true;
            emptySlotPRCanvas.GetComponent<CanvasScaler>().enabled = true;
            emptySlotPRCanvas.GetComponent<GraphicRaycaster>().enabled = true;
        }
    }

    //Call this to display the menu shown when choosing a monster to assing to a department
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

    //Call this to update the assignment menu with monsters availble to be assigned
    public void UpdateAssignment(List<GameObject> department)
    {
        //reset panel
        int childNum = 0;
        foreach (Transform child in assignmentPanel.transform)
        { if (childNum != 0)
            {
                Destroy(child.gameObject);
            }
            childNum++;
        }

        //create an empty object to create some space between the menu and the first Field
        /*
        var empty = Instantiate(new GameObject(), new Vector3(0, 0, 0), Quaternion.identity);
        empty.AddComponent<RectTransform>().sizeDelta = new Vector2(0, 20);
        empty.AddComponent<Text>().text = "Choose a Monster to assign";
        var emptyText = empty.GetComponent<Text>();
        emptyText.font = arial;
        emptyText.horizontalOverflow = HorizontalWrapMode.Overflow;
        emptyText.verticalOverflow = VerticalWrapMode.Overflow;
        emptyText.alignment = TextAnchor.MiddleCenter;
        empty.transform.SetParent(assignmentPanel.transform, false);
        */

        //create a field for each Monster
        foreach (GameObject monster in breakRoomList) //NEED TO USE AN "INACTIVE" LIST FOR THIS LATER
        {
            //Create the field and set up its name and button functionality
            var newField = Instantiate(monsterField, new Vector3(0, 0, 0), Quaternion.identity);
            var newFieldCanvas = newField.transform.GetChild(0);
            var newFieldCanvasRect = newFieldCanvas.GetComponent<RectTransform>();
            newField.GetComponentInChildren<Text>().text = monster.name;
            newFieldCanvas.transform.GetChild(0).GetComponent<Image>().color = monster.GetComponent<SpriteRenderer>().color;
            newField.GetComponentInChildren<Button>().onClick.AddListener(delegate 
            {
                AddToDepartment(monster, department);
                DepartmentMenu();
            });
            newField.transform.SetParent(assignmentPanel.transform, false);

            //manually adjust its position            
            newFieldCanvasRect.anchoredPosition = new Vector2(0, 0);
            newFieldCanvasRect.anchorMin = new Vector2(0.5f, 0.5f);
            newFieldCanvasRect.anchorMax = new Vector2(0.5f, 0.5f);
        }
    }

    //function should be called when pulling up room menu options (ie clicking on a room)
    public void RoomMenu()
    {
        if (roomMenuOpen)
        {
            RoomMenuHandler(0);
        }
        roomMenuOpen = !roomMenuOpen;
        roomMenu.SetActive(roomMenuOpen);
        roomMenuConfirm.onClick.RemoveAllListeners();
    }

    //call this when changes are made to the Room Menu
    public void UpdateRoomMenu()
    {

    }

    //Pass to here when an option is selected in the room menu to add the proper funcionality to the confirm and cancel buttons in it
    public void RoomMenuHandler(int optionSelected)
    {
        switch (optionSelected)
        {
            case -1: //destroy room
                     //Checks if the room can be destroyed
                if (selectedObject.GetComponent<RoomScript>().CanRemove())
                {
                    roomMenuConfirm.onClick.AddListener(delegate
                    {
                        CurrencyChanged(50);
                        roomList[selectedObject.GetComponent<RoomScript>().myX, selectedObject.GetComponent<RoomScript>().myY] = null;
                        selectedObject.GetComponent<RoomScript>().UpdateNeighbors();
                        Destroy(selectedObject);
                        this.GetComponent<ConstructionScript>().ClearConstructionIcons();
                        this.GetComponent<ConstructionScript>().StartConstruction();
                        roomCount--;
                        RoomMenu();
                    });
                }
                else
                {
                    roomMenuConfirm.onClick.AddListener(delegate
                    {
                        print("NO!");
                        RoomMenu();
                    });
                }
                break;
            default: //nothing selected
                roomMenuConfirm.onClick.AddListener(delegate
                {
                    RoomMenu();
                });
                break;
            case 1:
                roomMenuConfirm.onClick.AddListener(delegate
                {
                    RoomMenu();
                });
                break;
        }
    }

    public void AddToDepartment(GameObject monster, List<GameObject> department)
    {
        if (monster.GetComponent<BaseMonster>().department != null)
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

            foreach (GameObject room in roomList)
            {               
                if (room != null && room.GetComponent<RoomScript>() != null)
                {
                    room.GetComponent<RoomScript>().RoomMemeberHandler();
                }                    
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
                int applicationLife = application.GetComponent<BaseMonster>().getApplicationLife();

                if (applicationLife > 0)
	                application.GetComponent<BaseMonster>().setApplicationLife(applicationLife - 1);
                if (applicationLife == 0)
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
                BaseMonster monsterScript = monster.GetComponent<BaseMonster>();
                CurrencyChanged(monsterScript.getBaseDamage());
                infamyXP += monsterScript.getThreat();
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

        foreach (GameObject monster in monsterList)
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
