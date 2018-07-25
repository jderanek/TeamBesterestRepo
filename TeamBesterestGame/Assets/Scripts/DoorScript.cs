using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour {
	//Game Manager
	GameManager gameManager;

	//The two rooms this door is connected to
	BaseRoom room1;
	BaseRoom room2;

	//The positions of the two rooms this door connects
	public Vector2 pos1 = Vector2.negativeInfinity;
	public Vector2 pos2 = Vector2.negativeInfinity;

	//Bool of whether this door is open or closed
	bool isOpen = false;

	//Sprite renderer
	public SpriteRenderer render;

	//Assigns gameManager
	void Awake() {
		this.gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		render = this.GetComponent<SpriteRenderer> ();
	}

	void OnMouseDown() {
		if (this.room1 != null && this.room2 != null) {
			if (isOpen) {
				isOpen = false;
				room1.neighborRooms.Remove (room2.gameObject);
				room2.neighborRooms.Remove (room1.gameObject);
				render.sprite = gameManager.closed;
			} else {
				isOpen = true;
				if (!room1.neighborRooms.Contains (room2.gameObject))
					room1.neighborRooms.Add (room2.gameObject);
				if (!room2.neighborRooms.Contains (room1.gameObject))
					room2.neighborRooms.Add (room1.gameObject);
				render.sprite = gameManager.open;
			}
		}
	}
		
	//Updates door sprite
	void Update() {
		if (pos1 == Vector2.negativeInfinity || pos2 == Vector2.negativeInfinity)
			return;
		
		if (gameManager.isValid (pos1)) {
			room1 = gameManager.roomList [(int)pos1.x, (int)pos1.y].GetComponent<BaseRoom> ();
			if (room2 != null && room1 != null) {
				this.AddRoom (room1, room2);
				this.AddRoom (room2, room1);
			}
		} else {
			if (room2 != null && room1 != null && 
				room2.adjacentRooms.Contains (room1.gameObject)) {
				room1.adjacentRooms.Remove (room2.gameObject);
				room2.adjacentRooms.Remove (room1.gameObject);
			}
			room1 = null;
		}
		if (gameManager.isValid (pos2)) {
			room2 = gameManager.roomList [(int)pos2.x, (int)pos2.y].GetComponent<BaseRoom> ();
			if (room1 != null && room2 != null) {
				this.AddRoom (room1, room2);
				this.AddRoom (room2, room1);
			}
		} else {
			if (room1 != null && room2 != null &&
				room1.adjacentRooms.Contains (room2.gameObject)) {
				room1.adjacentRooms.Remove (room2.gameObject);
				room2.adjacentRooms.Remove (room1.gameObject);
			}
			room2 = null;
		}

		if (room1 == null && room2 == null)
			GameObject.Destroy (this.gameObject);
		else if (room1 == null ^ room2 == null)
			this.render.sprite = gameManager.wall;
		else if (!isOpen)
			this.render.sprite = gameManager.closed;

		if (room1 == room2)
			this.render.enabled = false;
		else
			this.render.enabled = true;
	}

	//Adds room to adjacentList, unless it is alraedy added
	private void AddRoom(BaseRoom room1, BaseRoom room2) {
		if (!room1.adjacentRooms.Contains (room2.gameObject))
			room1.adjacentRooms.Add (room2.gameObject);
	}
}
