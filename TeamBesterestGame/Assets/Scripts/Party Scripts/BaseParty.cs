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

	//Backtracks by one room, changes state to Exploring if there's a room they haven't explored
	public RoomScript Backtrack() {
		RoomScript room = roomPath [roomPath.Count - 1];
		roomPath.Remove (room);
		return room;
	}

	//Finds next room to explore and moves to it
	//Changes state from Exploring if it is null
	public void MoveToNextRoom() {
		RoomScript toMove;
		switch (state) {
		case "Explore":
			toMove = this.FindNextRoom ();
			if (toMove != null)
				MoveTo (toMove);
			else if (this.exploredRooms.Count == gameManager.roomList.Length)
				state = "Exit";
			else
				state = "Back";
			break;
		case "Back":
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
			GameObject.Destroy (hero.gameObject);
		}
	}

	//Adds heros to the list of members
	public abstract void CreateParty();
}
