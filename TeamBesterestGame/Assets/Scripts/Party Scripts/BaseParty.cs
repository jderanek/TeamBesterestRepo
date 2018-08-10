using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseParty {

	public List<BaseHero> partyMembers;
	BaseRoom curRoom;
	public List<BaseRoom> roomPath;
	List<BaseRoom> exploredRooms;
	string state = "Explore";
	bool shouldRemove = false;

	GameManager gameManager;

	//Constructor takes hero list to create new party
	public BaseParty(BaseHero[] heroes) {
		roomPath = new List<BaseRoom> ();
		exploredRooms = new List<BaseRoom> ();
		this.gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		partyMembers = new List<BaseHero> ();
		for (int x=0; x < heroes.Length; x++) {
			partyMembers.Add (heroes [x]);
			heroes [x].setParty (this);
		}
		this.MoveTo (gameManager.spawnRoom.GetComponent<BaseRoom>());
		this.exploredRooms.Add (curRoom);
		this.roomPath.Add (curRoom);
	}
	//Secondary constructor takes hero list of GameObjects
	public BaseParty(GameObject[] heroes) {
		roomPath = new List<BaseRoom> ();
		exploredRooms = new List<BaseRoom> ();
		this.gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		partyMembers = new List<BaseHero> ();
		for (int x=0; x < heroes.Length; x++) {
			partyMembers.Add (heroes [x].GetComponent<BaseHero> ());
			heroes [x].GetComponent<BaseHero> ().setParty (this);
		}
		this.MoveTo (gameManager.spawnRoom.GetComponent<BaseRoom> ());
		this.exploredRooms.Add (curRoom);
		this.roomPath.Add (curRoom);
	}

	//Getter functions for variables
	public BaseHero[] getPartyMembers() {
		return this.partyMembers.ToArray();
	}
	public BaseRoom getRoom() {
		return this.curRoom;
	}
	public string getState() {
		return this.state;
	}
	public GameManager getManager() {
		return this.gameManager;
	}
	public bool markedForDelete() {
		return this.shouldRemove;
	}

	//Returns true if heroes can hold any more gold
	bool CanContinue() {
		foreach (BaseHero hero in partyMembers) {
			if (hero.getHolding () < hero.getCapacity ())
				return true;
		}
		return false;
	}

	//Calls all party members attack function in the current room
	public void AttackPhase() {
		foreach (BaseHero hero in partyMembers) {
			if (hero != null)
				hero.Attack ();
		}
	}

	//Calls all party members CheckRoom function in the current room
	public void CheckRoom() {
		foreach (BaseHero hero in partyMembers)
			hero.CheckCurrentRoom ();

		//Attempts to move to next room if possible
		this.MoveToNextRoom();
	}

	//Moves all party members to the given room
	public void MoveTo(BaseRoom room) {
		this.curRoom = room;
		foreach (BaseHero hero in partyMembers)
			hero.MoveTo (curRoom);
	}

	/*//Finds the adjacent room with the lowest threat
	//Cannot be an explored room
	public BaseRoom FindNextRoom() {
		BaseRoom room;
		BaseRoom min = null;
		int threat = int.MaxValue;
		foreach (GameObject roomObject in curRoom.neighborRooms) {
			room = roomObject.GetComponent<BaseRoom> ();
			if (room.roomThreat < threat && !exploredRooms.Contains(room)) {
				threat = room.roomThreat;
				min = room;
			}
		}

		return min;
	}*/

	//Finds the adjacent room with the lowest threat
	//Cannot be an explored room
	public BaseRoom FindNextRoom() {
		BaseRoom room;
		BaseRoom max = null;
		int maxScore = int.MinValue;
		int score;
		foreach (GameObject roomObject in curRoom.neighborRooms) {
			room = roomObject.GetComponent<BaseRoom> ();
			score = (room.currentGold / 100) - room.roomThreat;
			if (score > maxScore && !exploredRooms.Contains(room)) {
				maxScore = score;
				max = room;
			}
		}

		return max;
	}

	//Backtracks by one room
	public BaseRoom Backtrack() {
		//Returns null if there are no more rooms to backtrack through
		if (roomPath.Count == 0)
			return null;

		//Returns the last room in the path and removes it from the list
		BaseRoom room = roomPath [roomPath.Count - 1];
		roomPath.Remove (room);
		room = gameManager.roomList [room.myX, room.myY].GetComponent<BaseRoom> ();
		if (room == curRoom)
			Backtrack ();
		return room;
	}

	//Datastructure for pathfinding. Contains the Vector2 of the room, and the Vector2 of the preceding room
	//as well as an integer consisting of the distance to the spawnroom
	private class Node {
		public Vector2 pos;
		public Vector2 path;
		public int dis;

		public Node(Vector2 pos, Vector2 path, int dis) {
			this.pos = pos;
			this.path = path;
			this.dis = dis;
		}

		public static int GetDistance (Vector2 x, Vector2 y) {
			return (int)(Mathf.Abs (x.x - y.x) + Mathf.Abs (x.y - y.y));
		}
	}
	//Comparer to sort queue
	private class NodeCompare : IComparer<Node> {
		int IComparer<Node>.Compare ( Node x, Node y )  {
			return (x.dis - y.dis);
		}
	}

	//Checks if a given pos is a valid room in the GameManager
	private bool isValid(Vector2 pos) {
		if (pos.x < 0 || pos.y < 0 || pos.x >= gameManager.roomList.GetLength(0) ||
			pos.y >= gameManager.roomList.GetLength(1))
			return false;

		if (gameManager.roomList [(int)pos.x, (int)pos.y] == null)
			return false;
		else if (gameManager.roomList [(int)pos.x, (int)pos.y].GetComponent<BaseRoom> () == null)
			return false;
		else
			return true;
	}

	//Finds the shortest path back to the dungeons spawn room
	//Sets the roomPath to the new path
	//A* type algorithm
	public void FindExitPath() {
		Dictionary<Vector2, Node> allNodes = new Dictionary<Vector2, Node> ();
		Priority_Queue.SimplePriorityQueue<Node, int> queue = new Priority_Queue.SimplePriorityQueue<Node, int> ();
		BaseRoom spawn = gameManager.spawnRoom.GetComponent<BaseRoom> ();
		Vector2 spawnPos = new Vector2 (spawn.myX, spawn.myY);
		Node current = new Node (new Vector2 (this.curRoom.myX, this.curRoom.myY), Vector2.negativeInfinity, 
			               Node.GetDistance (new Vector2 (this.curRoom.myX, this.curRoom.myY), spawnPos));

		Vector2 newPos;
		Node toAdd;
		BaseRoom currentRoom = this.curRoom;
		while (current.dis != 0) {
			//Adds current node to all checked nodes
			allNodes.Add (current.pos, current);
			//Adds all possible rooms to move to
			foreach (GameObject roomObject in currentRoom.neighborRooms) {
				BaseRoom room = roomObject.GetComponent<BaseRoom> ();
				newPos = new Vector2 (room.myX, room.myY);
				if (!allNodes.ContainsKey (newPos)) {
					toAdd = new Node (newPos, current.pos, Node.GetDistance (newPos, spawnPos));
					queue.Enqueue (toAdd, toAdd.dis);
				}
			}

			//Gets the new shortest distance node from the queue
			current = queue.Dequeue();
			currentRoom = gameManager.roomList [(int)current.pos.x, (int)current.pos.y].GetComponent<BaseRoom> ();
		}

		//Iterates up until the last node in the path is found
		List<BaseRoom> newPath = new List<BaseRoom> ();
		while (current != null && current.path != Vector2.negativeInfinity) {
			newPath.Add (gameManager.roomList [(int)current.pos.x, (int)current.pos.y].GetComponent<BaseRoom>());
			if (current.path != Vector2.negativeInfinity)
				allNodes.TryGetValue (current.path, out current);
		}
		roomPath = newPath;
	}

	//Attempts to find and move to next room
	//Handles the State Machine
	//Stops if current room has monsters in it
	public void MoveToNextRoom() {
		if (curRoom.monsterInRoom)
			return;

		//Marks party for deletion if no heroes remain
		if (this.partyMembers.Count == 0) {
			this.shouldRemove = true;
			return;
		}
		
		BaseRoom toMove;
		switch (state) {
		case "Explore":
			toMove = this.FindNextRoom ();

			//Loops until there is a room with no hero, or there are no rooms
			while (toMove != null && toMove.heroInRoom) {
				curRoom = toMove;
				toMove = this.FindNextRoom ();
			}
			
			if (this.exploredRooms.Count == gameManager.roomCount || !CanContinue ()) {
				state = "Exit";
				FindExitPath ();
				roomPath.RemoveAt (roomPath.Count - 1);
				this.MoveToNextRoom ();
				//toMove = this.Backtrack ();
				//MoveTo (toMove);
			} else if (toMove != null) {
				MoveTo (toMove);
				this.exploredRooms.Add (toMove);
				this.roomPath.Add (toMove);
			} else {
				state = "Back";
				roomPath.RemoveAt (roomPath.Count - 1);
				this.MoveToNextRoom ();
				/*toMove = this.Backtrack ();
				if (toMove == null || this.curRoom == gameManager.spawnRoom.GetComponent<BaseRoom>()) {
					this.RemoveParty ();
					return;
				}
				MoveTo (toMove);*/
			}
			break;
		case "Back":
			toMove = this.Backtrack ();

			//Loops until there is a room with no hero, or there are no rooms
			while (toMove != null && toMove.heroInRoom)
				toMove = this.Backtrack ();

			if (toMove == null || this.curRoom == gameManager.spawnRoom.GetComponent<BaseRoom> ())
				this.RemoveParty ();
			else {
				MoveTo (toMove);
				//Sets state to exploring if there is a room to explore now
				BaseRoom room;
				foreach (GameObject roomObject in toMove.neighborRooms) {
					room = roomObject.GetComponent<BaseRoom> ();
					if (!this.exploredRooms.Contains (room)) {
						this.state = "Explore";
						this.roomPath.Add (curRoom);
					}
				}
			}
			break;
		case "Exit":
			toMove = this.Backtrack ();

			//Loops until there is a room with no hero, or there are no rooms
			while (toMove != null && toMove.heroInRoom)
				toMove = this.Backtrack ();
			
			if (toMove == null)
				this.RemoveParty ();
			else
				MoveTo (toMove);
			break;
		}
	}

	//Destroys all party members of this Party to despawn the party
	//Meant for use when returned to spawnRoom
	public void RemoveParty() {
		foreach (BaseHero hero in this.partyMembers) {
			hero.Remove ();
		}
		partyMembers = null;
		this.shouldRemove = true;
	}

	//Adds heros to the list of members
	//public abstract void CreateParty();
}
