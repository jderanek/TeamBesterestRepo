using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRoom : MonoBehaviour {

	//Private variables
	//Damage type here, is it a string???
	//float efficieny;
	public int size = 1;
	int cost;
	int slots;
	int threat = 0;

    public GameManager gameManager;
    private UIManager uiManager;

    public GameObject canvas;
    public GameObject confirmationBox;

	//List of rooms with open doors (neighbor) and list of all nearby rooms (adjacent)
    public List<GameObject> neighborRooms = new List<GameObject>(); //public to assign reference in editor
	public List<GameObject> adjacentRooms = new List<GameObject>();

    public List<GameObject> roomMembers = new List<GameObject>(); //public to be accessed by hero/monster
    public int myX; //public to assign reference in editor
    public int myY; //public to assign reference in editor

    public List<GameObject> heroesInRoom = new List<GameObject>();

    public bool monsterInRoom = false; //public to be accessed by hero/monster
    public bool heroInRoom = false; //public to be accessed by hero/monster

    public bool northDoorBool; //public to be accessed by other scripts
    public bool southDoorBool; //public to be accessed by other scripts
    public bool eastDoorBool; //public to be accessed by other scripts
    public bool westDoorBool; //public to be accessed by other scripts

    public int roomThreat; //public to be accessed by heroes

    public int goldCapacity = 300; //public so it can be tested in editor
    public int currentGold; //public so it can be tested in editor
    private SpriteRenderer coin1; 
    private SpriteRenderer coin2; 
    private SpriteRenderer coin3; 
    public Sprite emptyCoin; //public to assign reference in editor
    public Sprite filledCoin; //public to assign reference in editor
    public SpriteRenderer highlight;

	//Room to pass when clicked
	public BaseRoom master;

    ///<summary>
    ///Assigns all stats to this room, to be used in place of super.
    ///</summary>
    /// <param name="sz">Room Size</param>
    /// <param name="ct">Room Cost</param>
    /// <param name="sl">Treasure Slots</param>
    void AssignStats(int sz, int ct, int sl) {
		this.size = sz;
		this.cost = ct;
		this.slots = sl;
	}

	//Getters
	public int getSize() {
		return this.size;
	}
	public int getCost() {
		return this.cost;
	}
	public int getTreasureSlots() {
		return  this.slots;
	}

    //Group of all functions copied from BaseRoom
    // Use this for initialization
    void Start()
    {
        Initialize();
    }  

    public void Initialize()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
		this.master = this;
        uiManager = gameManager.GetComponent<UIManager>();
        gameManager.roomList[myX, myY] = gameObject;
        UpdateNeighbors();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (!(this.CompareTag("Spawn Room") || this.CompareTag("Boss Room")))
        {
            coin1 = transform.GetChild(2).GetComponent<SpriteRenderer>();
            coin2 = transform.GetChild(3).GetComponent<SpriteRenderer>();
            coin3 = transform.GetChild(4).GetComponent<SpriteRenderer>();
            highlight = transform.GetChild(5).GetComponent<SpriteRenderer>();
        }

		GameObject newDoor;
		DoorScript door;
		if (isValid (new Vector2 (myX + 1, myY)))
			adjacentRooms.Add (gameManager.roomList [myX + 1, myY]);
		else {
			newDoor = Instantiate (gameManager.door, new Vector2 (myX + .5f, myY), Quaternion.Euler (new Vector3 (0, 0, 90)));
			door = newDoor.GetComponent<DoorScript> ();
			door.pos1 = new Vector2 (this.myX, this.myY);
			door.pos2 = new Vector2 (this.myX + 1, this.myY);
		}
		if (isValid (new Vector2 (myX - 1, myY)))
			adjacentRooms.Add (gameManager.roomList [myX - 1, myY]);
		else {
			newDoor = Instantiate (gameManager.door, new Vector2 (myX - .5f, myY), Quaternion.Euler (new Vector3 (0, 0, 90)));
			door = newDoor.GetComponent<DoorScript> ();
			door.pos1 = new Vector2 (this.myX, this.myY);
			door.pos2 = new Vector2 (this.myX - 1, this.myY);
		}
		if (isValid (new Vector2 (myX, myY + 1)))
			adjacentRooms.Add (gameManager.roomList [myX, myY + 1]);
		else {
			newDoor = Instantiate (gameManager.door, new Vector2 (myX, myY + .5f), Quaternion.Euler (new Vector3 (0, 0, 0)));
			door = newDoor.GetComponent<DoorScript> ();
			door.pos1 = new Vector2 (this.myX, this.myY);
			door.pos2 = new Vector2 (this.myX, this.myY + 1);
		}
		if (isValid (new Vector2 (myX, myY - 1)))
			adjacentRooms.Add (gameManager.roomList [myX, myY - 1]);
		else {
			newDoor = Instantiate (gameManager.door, new Vector2 (myX, myY - .5f), Quaternion.Euler (new Vector3 (0, 0, 0)));
			door = newDoor.GetComponent<DoorScript> ();
			door.pos1 = new Vector2 (this.myX, this.myY);
			door.pos2 = new Vector2 (this.myX, this.myY - 1);
		}
    }

    void OnMouseOver()
    {
        if (gameManager.selectedObjects.Count != 0 && gameManager.selectedObjects[0].GetComponent<BaseMonster>() != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                print(master);
                if (master.GetComponent<MergedRoom>() != null) {
                    master.GetComponent<MergedRoom>().rooms[0].AssignMonsters(gameManager.selectedObjects);
                }
                else
                {
                    master.AssignMonsters(gameManager.selectedObjects);
                }
            }
        }

        if (gameManager.inConstructionMode)
        {
            if (Input.GetMouseButtonDown(0) && (gameManager.selectedObjects.Count == 0 || gameManager.selectedObjects[0].CompareTag("Room")))
            {
                if (true)
                {
                    if (!this.highlight.enabled)
                    {
                        gameManager.selectedObjects.Add(gameObject);
                        this.highlight.enabled = true;
                    }
                    else
                    {
                        gameManager.selectedObjects.Remove(gameObject);
                        this.highlight.enabled = false;
                    }
                }   
            }

            if (Input.GetMouseButtonDown(1))
            {
                uiManager.ToggleMenu(4);
            }
        }
        
    }

    public void AssignMonsters(List<GameObject> monsters)
    {
        foreach (GameObject monster in monsters)
        {
            monster.transform.position = new Vector3(
                    gameObject.transform.position.x + UnityEngine.Random.Range(-0.25f, 0.25f),
                    gameObject.transform.position.y + UnityEngine.Random.Range(-0.25f, 0.25f),
                    0
                    );
            roomMembers.Add(monster);
            RoomEffect(monster.GetComponent<BaseMonster>());
            roomThreat += monster.GetComponent<BaseMonster>().getThreat();
            monsterInRoom = true;
            monster.GetComponent<BaseMonster>().setCurRoom(this.gameObject);
            gameManager.AddToDepartment(monster, gameManager.dungeonList);
            //uiManager.UpdateMonsters();
            //uiManager.UpdateDepartments();
            //selectedObject = null;
        }
        uiManager.UpdateMonsters();
        uiManager.UpdateDepartments();
        monsters.Clear();
    }

    public void UpdateNeighbors()
    {
        //neighborRooms.Clear();
    }

    public void ClearNeighbors()
    {
        neighborRooms.Clear();
    }

    public void SortMembers()
    {
        roomMembers.Sort(delegate (GameObject x, GameObject y)
        {
            if (x.GetComponent<BaseMonster>().getThreat() > y.GetComponent<BaseMonster>().getThreat())
            {
                return -1;
            }

            else if (x.GetComponent<BaseMonster>().getThreat() < y.GetComponent<BaseMonster>().getThreat())
            {
                return 1;
            }

            else
            {
                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        });
    }

    public void SortHeroes()
    {
        roomMembers.Sort(delegate (GameObject x, GameObject y)
        {
            if (x.GetComponent<BaseHero>().getThreat() > y.GetComponent<BaseHero>().getThreat())
            {
                return -1;
            }

            else if (x.GetComponent<BaseHero>().getThreat() < y.GetComponent<BaseHero>().getThreat())
            {
                return 1;
            }

            else
            {
                if (UnityEngine.Random.Range(0, 2) == 0)
                {
                    return -1;
                }
                else
                {
                    return 1;
                }
            }
        });
    }

    //make this more efficent later by only calling it on necesary rooms rather than all
    public void RoomMemeberHandler()
    {
        foreach (GameObject monster in roomMembers)
        {
            if (heroesInRoom.Count != 0)
            {
                monster.GetComponent<BaseMonster>().setInCombat(true);
            }
            else
            {
                monster.GetComponent<BaseMonster>().setInCombat(false);
            }
        }
    }

    public void AddGoldToRoom(int coinClicked)
    {
        switch (coinClicked)
        {
            case 1:
                switch (currentGold)
                {
                    case 0:
                        if (gameManager.currentCurrency >= 100)
                        {
                            gameManager.currentCurrency -= 100;
                            currentGold = 100;
                            uiManager.UpdateCurrency();
                        }
                        break;
                    case 100:
                        gameManager.currentCurrency += currentGold;
                        currentGold = 0;
                        uiManager.UpdateCurrency();
                        break;
                    case 200:
                    case 300:
                        gameManager.currentCurrency += currentGold - 100;
                        currentGold = 100;
                        uiManager.UpdateCurrency();
                        break;
                }
                break;
            case 2:
                switch (currentGold)
                {
                    case 0:
                    case 100:
                        if (gameManager.currentCurrency >= (200 - currentGold))
                        {
                            gameManager.currentCurrency -= 200 - currentGold;
                            currentGold = 200;
                            uiManager.UpdateCurrency();
                        }
                        break;
                    case 200:
                        gameManager.currentCurrency += currentGold - 100;
                        currentGold = 100;
                        uiManager.UpdateCurrency();
                        break;
                    case 300:
                        gameManager.currentCurrency += currentGold - 200;
                        currentGold = 200;
                        uiManager.UpdateCurrency();
                        break;
                }
                break;
            case 3:
                switch (currentGold)
                {
                    case 0:
                    case 100:
                    case 200:
                        if (gameManager.currentCurrency >= (300 - currentGold))
                        {
                            gameManager.currentCurrency -= 300 - currentGold;
                            currentGold = 300;
                            uiManager.UpdateCurrency();
                        }
                        break;
                    case 300:
                        gameManager.currentCurrency += currentGold - 200;
                        currentGold = 200;
                        uiManager.UpdateCurrency();
                        break;
                }
                break;
        }
        UpdateCoins();
    }

    public void UpdateCoins()
    {
        switch (currentGold)
        {
            case 0:
                coin1.sprite = emptyCoin;
                coin2.sprite = emptyCoin;
                coin3.sprite = emptyCoin;
                break;
            case 100:
                coin1.sprite = filledCoin;
                coin2.sprite = emptyCoin;
                coin3.sprite = emptyCoin;
                break;
            case 200:
                coin1.sprite = filledCoin;
                coin2.sprite = filledCoin;
                coin3.sprite = emptyCoin;
                break;
            case 300:
                coin1.sprite = filledCoin;
                coin2.sprite = filledCoin;
                coin3.sprite = filledCoin;
                break;
            default:
                Debug.Log("You shouldn't be seeing this message");
                break;
        }
    }

	//Removed
    public void OperateDoors(int door)
    {
        UpdateNeighbors();
    }

    //Datastructure for pathfinding. Contains the Vector2 of the room, and the Vector2 of the preceding room
    //as well as an integer consisting of the distance to the spawnroom
    private class Node
    {
        public Vector2 pos;
        public Vector2 path;
        public int dis;

        public Node(Vector2 pos, Vector2 path, int dis)
        {
            this.pos = pos;
            this.path = path;
            this.dis = dis;
        }

        public static int GetDistance(Vector2 x, Vector2 y)
        {
            return (int)(Mathf.Abs(x.x - y.x) + Mathf.Abs(x.y - y.y));
        }
    }
    //Comparer to sort queue
    private class NodeCompare : IComparer<Node>
    {
        int IComparer<Node>.Compare(Node x, Node y)
        {
            return (x.dis - y.dis);
        }
    }

    //Checks if a given pos is a valid room in the GameManager
	public bool isValid(Vector2 pos)
    {
        if (pos.x < 0 || pos.y < 0 || pos.x >= gameManager.roomList.GetLength(0) ||
            pos.y >= gameManager.roomList.GetLength(1))
            return false;

        if (gameManager.roomList[(int)pos.x, (int)pos.y] == null)
            return false;
        else if (gameManager.roomList[(int)pos.x, (int)pos.y].GetComponent<BaseRoom>() == null)
            return false;
        else
            return true;
    }

    //Taken from BaseParty to check and see if all of a Room's neighbors have a path to the spawn room
    public bool CanRemove()
    {
		BaseRoom roomScript;
		foreach (GameObject room in adjacentRooms) {
			roomScript = room.GetComponent<BaseRoom> ();
			if (roomScript != null) {
				if (!HasPath (roomScript))
					return false;
			}
		}

		return true;
    }

    //Finds the shortest path back to the dungeons spawn room
    //Returns true if a path is found
    //A* type algorithm
    public bool HasPath(BaseRoom room)
    {
        //Returns true if the given room is null
        if (room == null)
            return true;

        Dictionary<Vector2, Node> allNodes = new Dictionary<Vector2, Node>();
        Priority_Queue.SimplePriorityQueue<Node, int> queue = new Priority_Queue.SimplePriorityQueue<Node, int>();
        BaseRoom spawn = gameManager.spawnRoom.GetComponent<BaseRoom>();
        Vector2 spawnPos = new Vector2(spawn.myX, spawn.myY);
        Node current = new Node(new Vector2(room.myX, room.myY), Vector2.negativeInfinity,
            Node.GetDistance(new Vector2(room.myX, room.myY), spawnPos));
        Vector2 removePos = new Vector2(this.myX, this.myY);

        Vector2 newPos;
        Node toAdd;
		BaseRoom currentRoom = room;
		while (current.dis != 0) {
			//Adds current node to all checked nodes
			allNodes.Add (current.pos, current);

			//Breaks if there is a null room still
			if (currentRoom == null)
				return false;

			//Adds all possible rooms to move to
			foreach (GameObject roomObject in currentRoom.adjacentRooms) {
				BaseRoom script = roomObject.GetComponent<BaseRoom> ();
				newPos = new Vector2 (script.myX, script.myY);
				if (!allNodes.ContainsKey (newPos) && newPos != removePos) {
					toAdd = new Node (newPos, current.pos, Node.GetDistance (newPos, spawnPos));
					queue.Enqueue (toAdd, toAdd.dis);
				}
			}

            //If the queue is empty, there is no path so it returns false
			if (queue.Count == 0) {
				return false;
			}

            //Gets the new shortest distance node from the queue
            current = queue.Dequeue();

			//Another check for null values
			if (!isValid(current.pos))
				return false;

			currentRoom = gameManager.roomList [(int)current.pos.x, (int)current.pos.y].GetComponent<BaseRoom> ();
        }

        return true;
    }

	//Sets gameManager, meant for use from MergedRoom
	public void SetManager() {
		gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
	}

	//Removes this object from all adjacent rooms
	//Should also be done by doors, but this should work between updates
	public void RemoveAdjacent() {
		BaseRoom toRemove;
		foreach (GameObject room in this.adjacentRooms) {
			toRemove = room.GetComponent<BaseRoom> ();
			toRemove.adjacentRooms.Remove (this.gameObject);
		}
		this.adjacentRooms.Clear ();
	}

    //call when monster enters room
    abstract public void RoomEffect(BaseMonster monster);

    //call when monster leaves room
    abstract public void RemoveRoomEffect(BaseMonster monster);
}
