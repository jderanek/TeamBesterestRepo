using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseParty {

	BaseHero[] partyMembers;
	RoomScript curRoom;
	List<RoomScript> roomPath;
	List<RoomScript> exploredRooms;
	string state = "Explore";

	GameManager gameManager;

	//Assigns gameManager, and other initialization
	public void Awake() {
		this.gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		roomPath = new List<RoomScript> ();
		exploredRooms = new List<RoomScript> ();
		roomPath.Add (gameManager.spawnRoom.GetComponent<RoomScript> ());
		exploredRooms.Add (gameManager.spawnRoom.GetComponent<RoomScript> ());
	}

	//Getter functions for variables
	public BaseHero[] getPartyMembers() {
		return this.partyMembers;
	}
	public RoomScript getRoom() {
		return this.curRoom;
	}
	public string getState() {
		return this.state;
	}

	//Calls all party members attack function in the current room
	public void AttackPhase() {
		foreach (BaseHero hero in partyMembers)
			hero.Attack ();
	}

	//Moves all party members to the given room
	public void MoveTo(RoomScript room) {
		this.curRoom = room;
		this.exploredRooms.Add (curRoom);
		this.roomPath.Add (curRoom);
		foreach (BaseHero hero in partyMembers)
			hero.MoveTo (curRoom);
	}

	//Finds the adjacent room with the lowest threat
	//Cannot be an explored room
	public RoomScript FindNextRoom() {
		RoomScript room;
		RoomScript min = null;
		int threat = int.MaxValue;
		foreach (GameObject roomObject in curRoom.neighborRooms) {
			room = roomObject.GetComponent<RoomScript> ();
			if (room.roomThreat < threat && !exploredRooms.Contains(room)) {
				threat = room.roomThreat;
				min = room;
			}
		}

		return min;
	}

	//Finds next room to explore and moves to it
	//Changes state from Exploring if it is null
	public void MoveToNextRoom() {
		RoomScript toMove = this.FindNextRoom ();
		if (toMove != null) {
			MoveTo (toMove);
			return;
		}

		if (this.exploredRooms.Count == gameManager.roomList.Length)
			state = "Exit";
		else
			state = "Back";
	}

	//Adds heros to the list of members
	public abstract void CreateParty();
}
