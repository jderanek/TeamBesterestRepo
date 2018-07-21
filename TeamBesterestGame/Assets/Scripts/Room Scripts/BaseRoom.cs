using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRoom : MonoBehaviour {

	//Private variables
	//Damage type here, is it a string???
	//float efficieny;
	int size;
	int cost;
	int slots;
	int threat = 0;

    private GameManager gameManager;

    public GameObject canvas;
    public GameObject confirmationBox;

    public List<GameObject> neighborRooms = new List<GameObject>(); //public to assign reference in editor

    public List<GameObject> roomMembers = new List<GameObject>(); //public to be accessed by hero/monster
    public int myX; //public to assign reference in editor
    public int myY; //public to assign reference in editor

    public List<GameObject> heroesInRoom = new List<GameObject>();

    public bool monsterInRoom = false; //public to be accessed by hero/monster
    public bool heroInRoom = false; //public to be accessed by hero/monster

    public GameObject northRoom; //public to assign reference in editor, should be unnecesary in the future
    public GameObject southRoom; //public to assign reference in editor
    public GameObject eastRoom; //public to assign reference in editor
    public GameObject westRoom; //public to assign reference in editor

    private BaseRoom northRoomScript;
    private BaseRoom southRoomScript;
    private BaseRoom eastRoomScript;
    private BaseRoom westRoomScript;

    public GameObject northDoor; //public to be accessed by door script, may replace with trigger events later
    public GameObject southDoor; //public to assign reference in editor
    public GameObject eastDoor; //public to assign reference in editor
    public GameObject westDoor; //public to assign reference in editor

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
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        gameManager.roomList[myX, myY] = gameObject;
        UpdateNeighbors();
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        if (!(this.CompareTag("Spawn Room") || this.CompareTag("Boss Room")))
        {
            coin1 = transform.GetChild(2).GetComponent<SpriteRenderer>();
            coin2 = transform.GetChild(3).GetComponent<SpriteRenderer>();
            coin3 = transform.GetChild(4).GetComponent<SpriteRenderer>();
        }
    }

    void OnMouseOver()
    {
        if (gameManager.selectedObject != null && gameManager.selectedObject.GetComponent<BaseMonster>() != null)
        {
            if (Input.GetMouseButtonDown(1))
            {
                gameManager.selectedObject.transform.position = new Vector3(
                    gameObject.transform.position.x + UnityEngine.Random.Range(-0.25f, 0.25f),
                    gameObject.transform.position.y + UnityEngine.Random.Range(-0.25f, 0.25f),
                    0
                    );
                roomMembers.Add(gameManager.selectedObject);
                roomThreat += gameManager.selectedObject.GetComponent<BaseMonster>().getThreat();
                monsterInRoom = true;
                gameManager.selectedObject.GetComponent<BaseMonster>().setCurRoom(this.gameObject);
                foreach (GameObject neighbor in neighborRooms)
                {
                    neighbor.GetComponent<BaseRoom>().SortNeighbors();
                }
                gameManager.AddToDepartment(gameManager.selectedObject, gameManager.dungeonList);
                gameManager.UpdateMonsters();
                gameManager.UpdateDepartments();
                gameManager.selectedObject = null;
            }
        }

        if (Input.GetMouseButtonDown(1) && gameManager.inConstructionMode)
        {
            gameManager.RoomMenu();
            gameManager.selectedObject = gameObject;
        }
    }

    public void UpdateNeighbors()
    {
        neighborRooms.Clear();
        if (myY <= 8)
        {
            if (gameManager.roomList[myX, myY + 1] != null && gameManager.roomList[myX, myY + 1].GetComponent<BaseRoom>() != null)
            {
                northRoom = gameManager.roomList[myX, myY + 1];
                northRoomScript = northRoom.GetComponent<BaseRoom>();
                northRoomScript.southRoom = gameObject;
                if (!northDoorBool && !northRoomScript.southDoorBool)
                {
                    northRoomScript.southRoom = gameObject;
                    northRoomScript.neighborRooms.Add(gameObject);
                    neighborRooms.Add(northRoom);
                }
                else northRoomScript.neighborRooms.Remove(gameObject);
            }
        }

        if (myY >= 1)
        {
            if (gameManager.roomList[myX, myY - 1] != null && gameManager.roomList[myX, myY - 1].GetComponent<BaseRoom>() != null)
            {
                southRoom = gameManager.roomList[myX, myY - 1];
                southRoomScript = southRoom.GetComponent<BaseRoom>();
                southRoomScript.northRoom = gameObject;
                if (!southDoorBool && !southRoomScript.northDoorBool)
                {
                    southRoomScript.northRoom = gameObject;
                    southRoomScript.neighborRooms.Add(gameObject);
                    neighborRooms.Add(southRoom);
                }
                else southRoomScript.neighborRooms.Remove(gameObject);

            }
        }

        if (myX <= 8)
        {
            if (gameManager.roomList[myX + 1, myY] != null && gameManager.roomList[myX + 1, myY].GetComponent<BaseRoom>() != null)
            {
                eastRoom = gameManager.roomList[myX + 1, myY];
                eastRoomScript = eastRoom.GetComponent<BaseRoom>();
                eastRoomScript.westRoom = gameObject;
                if (!eastDoorBool && !eastRoomScript.westDoorBool)
                {
                    eastRoomScript.westRoom = gameObject;
                    eastRoomScript.neighborRooms.Add(gameObject);
                    neighborRooms.Add(eastRoom);
                }
                else eastRoomScript.neighborRooms.Remove(gameObject);
            }
        }

        if (myX >= 1)
        {
            if (gameManager.roomList[myX - 1, myY] != null && gameManager.roomList[myX - 1, myY].GetComponent<BaseRoom>() != null)
            {
                westRoom = gameManager.roomList[myX - 1, myY];
                westRoomScript = westRoom.GetComponent<BaseRoom>();
                westRoomScript.eastRoom = gameObject;
                if (!westDoorBool && !westRoomScript.eastDoorBool)
                {
                    westRoomScript.eastRoom = gameObject;
                    westRoomScript.neighborRooms.Add(gameObject);
                    neighborRooms.Add(westRoom);
                }
                else westRoomScript.neighborRooms.Remove(gameObject);
            }
        }
        SortNeighbors();
    }

    public void SortNeighbors()
    {
        if (neighborRooms.Count >= 2)
        {
            neighborRooms.Sort(delegate (GameObject x, GameObject y) {
                if (x.GetComponent<BaseRoom>().roomThreat > y.GetComponent<BaseRoom>().roomThreat)
                {
                    return -1;
                }
                else if (x.GetComponent<BaseRoom>().roomThreat < y.GetComponent<BaseRoom>().roomThreat)
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
    }

    public void ClearNeighbors()
    {
        if (northRoom != null)
        {
            northRoomScript.southRoom = null;
        }

        if (southRoom != null)
        {
            southRoomScript.northRoom = null;
        }

        if (eastRoom != null)
        {
            eastRoomScript.westRoom = null;
        }

        if (westRoom != null)
        {
            westRoomScript.eastRoom = null;
        }

        northRoom = null;
        southRoom = null;
        eastRoom = null;
        westRoom = null;

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
                            gameManager.UpdateCurrency();
                        }
                        break;
                    case 100:
                        gameManager.currentCurrency += currentGold;
                        currentGold = 0;
                        gameManager.UpdateCurrency();
                        break;
                    case 200:
                    case 300:
                        gameManager.currentCurrency += currentGold - 100;
                        currentGold = 100;
                        gameManager.UpdateCurrency();
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
                            gameManager.UpdateCurrency();
                        }
                        break;
                    case 200:
                        gameManager.currentCurrency += currentGold - 100;
                        currentGold = 100;
                        gameManager.UpdateCurrency();
                        break;
                    case 300:
                        gameManager.currentCurrency += currentGold - 200;
                        currentGold = 200;
                        gameManager.UpdateCurrency();
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
                            gameManager.UpdateCurrency();
                        }
                        break;
                    case 300:
                        gameManager.currentCurrency += currentGold - 200;
                        currentGold = 200;
                        gameManager.UpdateCurrency();
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
                coin1.GetComponent<SpriteRenderer>().sprite = emptyCoin;
                coin2.GetComponent<SpriteRenderer>().sprite = emptyCoin;
                coin3.GetComponent<SpriteRenderer>().sprite = emptyCoin;
                break;
            case 100:
                coin1.GetComponent<SpriteRenderer>().sprite = filledCoin;
                coin2.GetComponent<SpriteRenderer>().sprite = emptyCoin;
                coin3.GetComponent<SpriteRenderer>().sprite = emptyCoin;
                break;
            case 200:
                coin1.GetComponent<SpriteRenderer>().sprite = filledCoin;
                coin2.GetComponent<SpriteRenderer>().sprite = filledCoin;
                coin3.GetComponent<SpriteRenderer>().sprite = emptyCoin;
                break;
            case 300:
                coin1.GetComponent<SpriteRenderer>().sprite = filledCoin;
                coin2.GetComponent<SpriteRenderer>().sprite = filledCoin;
                coin3.GetComponent<SpriteRenderer>().sprite = filledCoin;
                break;
            default:
                Debug.Log("You shouldn't be seeing this message");
                break;
        }
    }

    public void OperateDoors(int door)
    {
        UpdateNeighbors();
        if (gameManager.inConstructionMode)
        {
            switch (door)
            {
                case 1:
                    northDoorBool = !northDoorBool;
                    northDoor.GetComponent<SpriteRenderer>().enabled = northDoorBool;
                    if (northRoom != null)
                    {
                        northRoomScript.southDoorBool = northDoorBool;
                        northRoomScript.southDoor.GetComponent<SpriteRenderer>().enabled = northDoorBool;
                    }
                    break;
                case 2:
                    southDoorBool = !southDoorBool;
                    southDoor.GetComponent<SpriteRenderer>().enabled = southDoorBool;
                    if (southRoom != null)
                    {
                        southRoomScript.northDoorBool = southDoorBool;
                        southRoomScript.northDoor.GetComponent<SpriteRenderer>().enabled = southDoorBool;
                    }
                    break;
                case 3:
                    eastDoorBool = !eastDoorBool;
                    eastDoor.GetComponent<SpriteRenderer>().enabled = eastDoorBool;
                    if (eastDoor != null)
                    {
                        eastRoomScript.westDoorBool = eastDoorBool;
                        eastRoomScript.westDoor.GetComponent<SpriteRenderer>().enabled = eastDoorBool;
                    }
                    break;
                case 4:
                    westDoorBool = !westDoorBool;
                    westDoor.GetComponent<SpriteRenderer>().enabled = westDoorBool;
                    if (westRoom != null)
                    {
                        westRoomScript.eastDoorBool = westDoorBool;
                        westRoomScript.eastDoor.GetComponent<SpriteRenderer>().enabled = westDoorBool;
                    }
                    break;
            }
        }
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
    private bool isValid(Vector2 pos)
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
        BaseRoom north = null;
        BaseRoom east = null;
        BaseRoom south = null;
        BaseRoom west = null;

        if (northRoom != null)
            north = northRoom.GetComponent<BaseRoom>();
        if (eastRoom != null)
            east = eastRoom.GetComponent<BaseRoom>();
        if (southRoom != null)
            south = southRoom.GetComponent<BaseRoom>();
        if (westRoom != null)
            west = westRoom.GetComponent<BaseRoom>();

        return (HasPath(north) && HasPath(east) && HasPath(south) && HasPath(west));
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
        while (current.dis != 0)
        {
            //Adds current node to all checked nodes
            allNodes.Add(current.pos, current);
            //Adds all four possible new directions, if available
            newPos = new Vector2(current.pos.x + 1, current.pos.y);
            if (isValid(newPos) && !allNodes.ContainsKey(newPos) &&
                newPos != removePos)
            {
                toAdd = new Node(newPos, current.pos, Node.GetDistance(newPos, spawnPos));
                queue.Enqueue(toAdd, toAdd.dis);
            }
            newPos = new Vector2(current.pos.x - 1, current.pos.y);
            if (isValid(newPos) && !allNodes.ContainsKey(newPos) &&
                newPos != removePos)
            {
                toAdd = new Node(newPos, current.pos, Node.GetDistance(newPos, spawnPos));
                queue.Enqueue(toAdd, toAdd.dis);
            }
            newPos = new Vector2(current.pos.x, current.pos.y + 1);
            if (isValid(newPos) && !allNodes.ContainsKey(newPos) &&
                newPos != removePos)
            {
                toAdd = new Node(newPos, current.pos, Node.GetDistance(newPos, spawnPos));
                queue.Enqueue(toAdd, toAdd.dis);
            }
            newPos = new Vector2(current.pos.x, current.pos.y - 1);
            if (isValid(newPos) && !allNodes.ContainsKey(newPos) &&
                newPos != removePos)
            {
                toAdd = new Node(newPos, current.pos, Node.GetDistance(newPos, spawnPos));
                queue.Enqueue(toAdd, toAdd.dis);
            }

            //If the queue is empty, there is no path so it returns false
            if (queue.Count == 0)
                return false;

            //Gets the new shortest distance node from the queue
            current = queue.Dequeue();
        }

        return true;
    }
}
