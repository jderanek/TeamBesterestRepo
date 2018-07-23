using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    #region Declarations
    UIManager uiManager;
    //Test Party
    public TestPart party;

    //Array stuff
    public GameObject[] possibleMonsters; //public to be assigned in editor
    private GameObject[][][] monsterSpawnList;
    public GameObject monsterInstance; //public to assign reference in editor

    public GameObject[] possibleHeroes; //public to be assigned in editor
    private List<GameObject> heroSpawnSet = new List<GameObject>();

    public GameObject[] possibleRooms; //public to be assigned in editor

    //TODO jagged array is faster, need to convert
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
    public List<BaseParty> attackParties = new List<BaseParty>();
    [HideInInspector]
    public List<GameObject> deadMonsters = new List<GameObject>(); //for the corporeally challenged
    [HideInInspector]
    public List<GameObject> applicationsList = new List<GameObject>();

    //TODO getter and setter
    [HideInInspector]
    public GameObject selectedObject;

    //Balance stuff
    public int healthWeight = 10; //public for testing in editor
    public int attackWeight = 3; //public for testing in editor
    public int defenseWeight = 2; //public for testing in editor

	//Money Stuff
    //TODO getters and setters
	public Text currencyText; //public to assign reference in editor
	public int currentCurrency; //public to be accessed by other scripts
    public int maximumCurrency = 1500; //public to be edited in editor

    //infamy
    public int infamyLevel = 0; //public to be tested in editor
    public int infamyXP = 0; //public to be tested in editor
    public int xpToNextInfamyLevel = 20; //public to be tested in editor
    public int baseXP = 20; //public to be tested in editor
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

    public GameObject spawnRoom; //public to be assigned in editor //can assign using tag later
    public GameObject bossRoom; //public to be assigned in editor //can assign using tag later
    public Image stressImage; //public to assign reference in editor

    //CSVImporter for Monsters and Heroes
    //public CSVImporter monsters = new CSVImporter(22, 9, "Monster_Stats_-_Sheet1.csv");
	public CSVImporter monsters;
	public CSVImporter monNames;
	public CSVImporter heroStats;

    #endregion

    #region Initialization
    // Use this for initialization
    void Awake()
    {
        uiManager = this.GetComponent<UIManager>();
        roomList = new GameObject[10, 10];
        monsters = new CSVImporter("Monster_Stats_-_Sheet1.csv",
            "https://docs.google.com/spreadsheets/d/e/2PACX-1vSBLCQyX37HLUhxOVtonHsR0S76lt2FzvDSeoAzPsB_TbQa43nR7pb6Ns5QeuaHwpIqun55JeEM8Llc/pub?gid=2027062354&single=true&output=csv");
        monNames = new CSVImporter("NamesWIP - Sheet1.csv",
            "https://docs.google.com/spreadsheets/d/e/2PACX-1vS3YmSZDNM2JAfk0jTir8mO4tq2Z_6SF7hPDmQvovd2G9Ld_dfFcDARmPQ2kB2hKYFSuupbD4oB2m7f/pub?gid=1640444901&single=true&output=csv");
        heroStats = new CSVImporter("Heroes - Sheet1.csv",
            "https://docs.google.com/spreadsheets/d/e/2PACX-1vROE5F1pcPZ65Zg5H5QsEqwpayjzcLOYQMffmv6E3zjR3tMq7kD68zPNGdrCXmq8w67wZHNNGwehsLo/pub?gid=0&single=true&output=csv");
        currentCurrency = 1500;
        uiManager.UpdateCurrency();
        uiManager.UpdateInfamy();

        modifiedHeroSpawnRate = baseHeroSpawnRate;

        currentTime = timePerDay;
        stressImage = GameObject.FindGameObjectWithTag("Aggregate Stress").GetComponent<Image>();

        //sets up intial list of monster spawns from possible monsters
        monsterSpawnList = new GameObject[][][]
        {
            //UNDEAD
            new GameObject[][]
            {
                new GameObject[] //infamy level 0
                {
                    possibleMonsters[0], possibleMonsters[1]
                },
                new GameObject[] //infamy level 1
                {
                    possibleMonsters[0], possibleMonsters[0], possibleMonsters[0], possibleMonsters[0], possibleMonsters[1],
                    possibleMonsters[1], possibleMonsters[1], possibleMonsters[1], possibleMonsters[8], possibleMonsters[9]
                }
            }, 

            //BESTIAL
            new GameObject[][]
            {
                new GameObject[] //infamy level 0
                {
                    possibleMonsters[2], possibleMonsters[3]
                },
                new GameObject[] //infamy level 1
                {
                    possibleMonsters[2], possibleMonsters[2], possibleMonsters[2], possibleMonsters[2], possibleMonsters[3],
                    possibleMonsters[3], possibleMonsters[3], possibleMonsters[3], possibleMonsters[10], possibleMonsters[11]
                }
            },

            //TRIBAL
            new GameObject[][]
            {
                new GameObject[] //infamy level 0
                {
                    possibleMonsters[4], possibleMonsters[5]
                },
                new GameObject[] //infamy level 1
                {
                    possibleMonsters[4], possibleMonsters[4], possibleMonsters[4], possibleMonsters[4], possibleMonsters[5],
                    possibleMonsters[5], possibleMonsters[5], possibleMonsters[5], possibleMonsters[12], possibleMonsters[13]
                }
            }, 

            //DEMONS
            new GameObject[][]
            {
                new GameObject[] //infamy level 0
                {
                    possibleMonsters[6], possibleMonsters[7]
                },
                new GameObject[] //infamy level 1
                {
                    possibleMonsters[6], possibleMonsters[6], possibleMonsters[6], possibleMonsters[6], possibleMonsters[7],
                    possibleMonsters[7], possibleMonsters[7], possibleMonsters[7], possibleMonsters[14], possibleMonsters[15]
                }
            }

        };
        CreateNewResume(3);

        for (int i = 0; i < 7; i++)
        {
            heroSpawnSet.Add(possibleHeroes[i]);
        }
    }

	void Start() {			
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
		roomList [5, 5] = spawnRoom;
		for (int x = 0; x < roomList.GetLength (0); x++) {
			for (int y = 0; y < roomList.GetLength (1); y++) {
				if (roomList [x, y] != null)
					Debug.Log ("Room at: " + x.ToString () + ", " + y.ToString ());
			}
		}
        */
    }
    #endregion

    #region Handlers
    //spawns a new monster instance
    public GameObject SpawnMonster()
    {
        int rand = Random.Range(0, 4);
        GameObject monsterPrefab = monsterSpawnList[rand][infamyLevel][Random.Range(0, monsterSpawnList[rand][infamyLevel].Length)];
        GameObject newMonster = Instantiate(monsterPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        newMonster.GetComponent<BaseMonster>().AssignStats(monsterPrefab.name);
        newMonster.SetActive(false);
        return newMonster;
    }

    //Changes a monster's department
    public void AddToDepartment(GameObject monster, List<GameObject> department)
    {
        BaseMonster monsterScript = monster.GetComponent<BaseMonster>();
        if (dungeonList.Contains(monster))
        {
            monsterScript.getCurRoom().RemoveRoomEffect(monsterScript);
        }

        if (monsterScript.department != null)
        {
            monsterScript.department.Remove(monster);
        }
        department.Add(monster);
        monsterScript.department = department;
        uiManager.UpdateDepartments();
        uiManager.UpdateMonsters();
    }

    //Converts a monster from an applicant to an employee. Called by Hire Button
    public void HireButton(GameObject monsterInstance, GameObject monsterApplicationField)
    {
        int salary = monsterInstance.GetComponent<BaseMonster>().getSalary();
        int infamyRaise = monsterInstance.GetComponent<BaseMonster>().getInfamyGain();
        monsterInstance.GetComponent<BaseMonster>().setApplicationLife(-1);
        if (currentCurrency >= salary)
        {
            applicationsList.Remove(monsterInstance);
            Destroy(monsterApplicationField);
            monsterInstance.SetActive(true);
            CurrencyChanged(-salary);
            IncreaseInfamyXP(monsterInstance.GetComponent<BaseMonster>().getThreat());
            monsterList.Add(monsterInstance);
            AddToDepartment(monsterInstance, breakRoomList);
            uiManager.UpdateMonsters();
            uiManager.UpdateDepartments();
            uiManager.UpdateStressMeter();
        }
    }

    //Spawns a new applicant monster
    public void CreateNewResume(int resumesToCreate)
	{
		for (int i = 0; i < resumesToCreate; i++)
		{
			monsterInstance = SpawnMonster();
            applicationsList.Add(monsterInstance);
            monsterInstance.SetActive(false);
		}
        uiManager.UpdateApplications();
	}

    //Pass to here when an option is selected in the room menu to add the proper funcionality to the confirm and cancel buttons in it
    public void RoomMenuHandler(int optionSelected)
    {
        uiManager.roomMenuConfirm.onClick.RemoveAllListeners();
        switch (optionSelected)
        {
            case -1: //destroy room
                     //Checks if the room can be destroyed
                if (selectedObject.GetComponent<BaseRoom>().CanRemove())
                {
                    uiManager.roomMenuConfirm.onClick.AddListener(delegate
                    {
                        CurrencyChanged(50);
                        roomList[selectedObject.GetComponent<BaseRoom>().myX, selectedObject.GetComponent<BaseRoom>().myY] = null;
                        selectedObject.GetComponent<BaseRoom>().UpdateNeighbors();
                        Destroy(selectedObject);
                        this.GetComponent<ConstructionScript>().ClearConstructionIcons();
                        this.GetComponent<ConstructionScript>().StartConstruction();
                        roomCount--;
                        uiManager.ToggleMenu(4);
                    });
                }
                else
                {
                    uiManager.roomMenuConfirm.onClick.AddListener(delegate
                    {
                        print("NO!");
                        uiManager.ToggleMenu(4);
                    });
                }
                break;
            default: //nothing selected
                uiManager.roomMenuConfirm.onClick.AddListener(delegate
                {
                    uiManager.ToggleMenu(4);
                });
                break;
            case 1: //Cemetary selected
                uiManager.roomMenuConfirm.onClick.AddListener(delegate
                {
                    //out with the old, in with the new
                    BaseRoom oldScript = selectedObject.GetComponent<BaseRoom>();
                    GameObject newRoom = Instantiate(possibleRooms[1], selectedObject.transform.position, Quaternion.identity);
                    CemetaryRoom newScript = newRoom.GetComponent<CemetaryRoom>();
                    newScript.myX = oldScript.myX;
                    newScript.myY = oldScript.myY;
                    newScript.Initialize();

                    //reseting monsters in room
                    foreach (GameObject monster in oldScript.roomMembers)
                    {
                        AddToDepartment(monster, breakRoomList);
                        monster.transform.position = Vector3.zero;
                    }
                    uiManager.UpdateDepartments();

                    CurrencyChanged(oldScript.currentGold);
                    Destroy(selectedObject);

                    uiManager.ToggleMenu(4);
                });
                break;
        }
    }
    #endregion

    //enables interview UI and hides other UI elements that are in the way
    public void Interview(GameObject monster)
    {
        monsterInstance = monster; //might wanna use selectedObject for consistency - Nate
        interviewing = true;
        uiManager.menus[3].SetActive(false);
        Q1.SetActive(true);
        Q2.SetActive(true);
        Q3.SetActive(true);
        Q4.SetActive(true);
        Q5.SetActive(true);
        interviewImage.SetActive(true);
        interviewResponse.SetActive(true);
        interviewExit.SetActive(true);


        //this.gameObject.GetComponentInChildren<InterviewManager>().UpdateQuestions();
    }

    #region Time Stuff
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
                if (room != null && room.GetComponent<BaseRoom>() != null)
                {
                    room.GetComponent<BaseRoom>().RoomMemeberHandler();
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
                //grabs a hero from spawn set with equal weight. Maybe best way to affect spawn %s is to just add duplicates to spawn set?
				newHero[0] = Instantiate(heroSpawnSet[Random.Range(0, infamyLevel * 2)], spawnRoom.transform.position, Quaternion.identity);
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
            uiManager.UpdateApplications();

            //PR Department code
            foreach (GameObject monster in prList)
            {
                BaseMonster monsterScript = monster.GetComponent<BaseMonster>();
                CurrencyChanged(monsterScript.getBaseDamage());
                infamyXP += monsterScript.getThreat();
                uiManager.UpdateInfamy();
            }

            //Potential of 1 new resume per time unit
            //May put some public variables here so chance and amount can be edited in editor
            if (Random.Range(0, 9) > 4)
				CreateNewResume(1);

            uiManager.UpdateStressMeter();

            if (currentTime <= 0 && dungeonEmpty)
            {
                NewCycle();
            }
		}
		//party.MoveToNextRoom ();
	}

    //New day handler to apply effects on start of day
    public void DayHandler()
    {
        if (!paused)
        {
            TogglePlay();
        }
        currentTime = timePerDay;
        timeUnitText.text = currentTime.ToString();
    }

    //New week handler to apply effects on start of week
    public void WeekHandler()
    {
        foreach (GameObject room in roomList)
        {
            foreach (GameObject monster in room.GetComponent<BaseRoom>().roomMembers)
            {
                BaseMonster monScript = monster.GetComponent<BaseMonster>();

                if (monScript != null)
                {
                    if (monScript.getTrait() != null)
                    {
                        monScript.personality.ApplyWeekEffects(monScript);
                    }
                }

                monScript.setHasFought(false);
            }
        }
    }
    #endregion

    #region State Managers

    public void ToggleConstruction()
    {
        if (inConstructionMode)
        {
            foreach (GameObject room in roomList)
            {
                if (room != null && room.GetComponent<BaseRoom>() != null)
                {
                    room.GetComponent<BaseRoom>().UpdateNeighbors();
                }
            }
        }
        inConstructionMode = !inConstructionMode;
    }

    public void TogglePlay()
    {
        if (paused)
        {
            var coroutine = Play();
            StartCoroutine(coroutine);
            pauseButtonText.text = "Pause";

        }
        else
        {
            StopAllCoroutines();
            pauseButtonText.text = "Play";
        }
        paused = !paused;
    }

    #endregion

    #region Getters and Setters

    public void SelectObject(GameObject otherObject)
    {
        selectedObject = otherObject;
    }

    //changes value of currency and updates UI
    //Takes: integer (either positive or negative)
    public void CurrencyChanged(int value)
	{
		currentCurrency += value;
		uiManager.UpdateCurrency();
	}

	//function to 'level up' player -> aka increase infamy level
	public void IncreaseInfamyXP(int characterValue)
	{
		int high = 25, low = 10;
		int divNum = Random.Range(low, high);
		int gain = characterValue / divNum;
        if (gain > 1)
        {
            infamyXP += gain;
        }
        else
        {
            infamyXP += 1;
        }

        if (infamyXP >= xpToNextInfamyLevel)
		{
			infamyLevel += 1;
			infamyXP -= xpToNextInfamyLevel;
			xpToNextInfamyLevel = InfamyXPNeeded();

            //modify spawn sets and options availible here
            switch(infamyLevel) {
                case 1:
                    //monsterSpawnSet.Add(possibleMonsters[0]);
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
            }
		}
		else
		{
			int toNext = (int)xpToNextInfamyLevel - (int)infamyXP;
			//print("xp to next level " + toNext);
		}
		uiManager.UpdateInfamy();
	}

	public int InfamyXPNeeded()
	{
		//print("reached new level");
		float exponent = 1.5f;
		float xp = baseXP;
		int xpNeeded = Mathf.FloorToInt(xp * (Mathf.Pow(infamyLevel, exponent)));
		return xpNeeded;
	}
    #endregion
}
