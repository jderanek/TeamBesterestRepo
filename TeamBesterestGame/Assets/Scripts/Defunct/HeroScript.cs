using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomMemberSorter : IComparer<GameObject>
{
    public int Compare(GameObject x, GameObject y)
    {
        if (x.GetComponent<MonsterScript>().threatValue > y.GetComponent<MonsterScript>().threatValue)
        {
            return 0;
        }
        else return 1;
    }
}

public class HeroScript : MonoBehaviour
{
	public int[] possibleThreatValue; //public to be assigned in editor
    public int threatValue; //public to be accessed by other scripts

    public int damage; //public to be edited in editor
    public int heroHp; //public to be edited in editor

	private Text damageText; //public to be assigned in editor

    public Transform monsterPosition; //public to be assigned in editor
    private GameObject targetMonster; //public to be assigned in editor
    private MonsterScript targetMonsterScript; //public to be assigned in editor
    //public bool monsterInRange;

	//public GameObject spawnRoom;
    private RoomScript currentRoomScript; 
    private GameObject currentRoom;

	private IEnumerator attackRepeater;

	public int currencyValue; //public to edited in editor

	private GameManager gameManager;

    public int carryCapacity = 200; //public to be edited in editor
    private bool packFull;
    private int currentGold;

    // Use this for initialization
    void Awake()
    {
		threatValue = possibleThreatValue[Random.Range(0, possibleThreatValue.Length)];
		currentRoom = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().spawnRoom;
        currentRoomScript = currentRoom.GetComponent<RoomScript>();
		currentRoomScript.heroesInRoom.Add(this.gameObject);
		damageText = this.gameObject.GetComponentInChildren<Text>();

		GameObject gameMangerObject = GameObject.FindWithTag ("GameController");
		if (gameMangerObject != null) {
			gameManager = gameMangerObject.GetComponent <GameManager> ();
		}
    }

    // Update is called once per frame
    void Update()
    {
		/*
        if (this.currentRoomScript.monsterInRoom == true)
        {
            //var coroutine = Attack(2f);
            //StartCoroutine(coroutine);
        }
        //else
        {
            //var routine = CheckCurrentRoom(5f);
            //StartCoroutine(routine);
        }
        */

    }

    /*
	private IEnumerator Attack(float attackSpeed)
	{
		while (true)
		{
			if (currentRoomScript.monsterInRoom) 
			{
				targetMonster = currentRoomScript.roomMembers [0];
			}
			else 
			{
				StopAllCoroutines ();
			}

			yield return new WaitForSeconds(attackSpeed);
            
			if (targetMonster != null)
            {
                targetMonster.GetComponent<MonsterScript>().TakeDamage(damage);
            }

            StopAllCoroutines();
        }
	}

    private IEnumerator CheckCurrentRoom(float timer)
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);
			currentRoomScript.SortNeighbors();

            if (currentRoomScript.neighborRooms.Count == 0)
            {
                StopAllCoroutines();
            }
            else
            {
                currentRoom = currentRoomScript.neighborRooms[0];

				currentRoomScript.heroesInRoom.Remove(this.gameObject);

				if (currentRoomScript.heroesInRoom.Count == 0)
				{
					currentRoomScript.heroInRoom = false;
				}

                currentRoomScript = currentRoom.GetComponent<RoomScript>();
				currentRoomScript.heroInRoom = true;
				currentRoomScript.heroesInRoom.Add(this.gameObject);
				currentRoomScript.SortHeroes();

                transform.position = currentRoom.transform.position;

                StopAllCoroutines();
            }

        }
    }
    */

    public void Attack() {
		if (currentRoomScript.monsterInRoom) 
		{
			targetMonster = currentRoomScript.roomMembers [0];
		}
		if (targetMonster != null)
		{
			targetMonster.GetComponent<MonsterScript>().TakeDamage(damage);
		}


	}

	public void CheckCurrentRoom() {
            currentRoomScript.SortNeighbors();
        
        if (!currentRoomScript.monsterInRoom && currentRoom.CompareTag("Boss Room") && gameManager.currentCurrency > 0)
        {
            currentGold += 100;
            gameManager.currentCurrency -= 100;
            gameManager.UpdateCurrency();
            if (currentGold == carryCapacity)
            {
                Destroy(gameObject);
            }
        }
        
        else if (!currentRoomScript.monsterInRoom && currentRoomScript.currentGold > 0) //If there isn't a monster in the room with the hero and there is gold to be looted
        {
            currentGold += 100;
            currentRoomScript.currentGold -= 100;
            currentRoomScript.UpdateCoins();
            if (currentGold == carryCapacity)
            {
                Destroy(gameObject);
            }
        }
        else if (!currentRoomScript.monsterInRoom && currentRoomScript.neighborRooms.Count != 0) //If there isn't a monster in the room with the hero and if the room has neighbor rooms
        {
            currentRoom = currentRoomScript.neighborRooms[0];

            currentRoomScript.heroesInRoom.Remove(this.gameObject);

            if (currentRoomScript.heroesInRoom.Count == 0)
            {
                currentRoomScript.heroInRoom = false;
            }

            currentRoomScript = currentRoom.GetComponent<RoomScript>();
            currentRoomScript.heroInRoom = true;
            currentRoomScript.heroesInRoom.Add(this.gameObject);
            //currentRoomScript.SortHeroes(); //somethings wrong here

            transform.position = currentRoom.transform.position;
        }
    }       
		
    //next two functions are what monsters will call to damage the hero
    public void TakeDamage(int damageTaken)
    {
        heroHp -= damageTaken;
		damageText.text = damageTaken.ToString();

		//Debug.Log (heroHp);
        if (heroHp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
		currentRoomScript.heroesInRoom.Remove(this.gameObject);
		if (currentRoomScript.heroesInRoom.Count == 0)
		{
			currentRoomScript.heroInRoom = false;
		}

        gameManager.GetComponent<GameManager>().IncreaseInfamyXP(threatValue);
        if (gameManager.currentCurrency < gameManager.maximumCurrency)
        {
            gameManager.GoldGainedOnDeath(currencyValue);
        }
        Destroy(this.gameObject);
    }
}
