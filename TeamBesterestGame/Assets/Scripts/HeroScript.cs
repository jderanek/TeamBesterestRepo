using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private int aggro;
    public int damage;
    //public int attackSpeed;
    public int heroHp;
    public float movementSpeed;
    public int traits;

    public Transform monsterPosition;
    private GameObject currentMonster;
    private MonsterScript currentMonsterScript;
    public bool monsterInRange;

	//public GameObject spawnRoom;
    public RoomScript currentRoomScript;
    public GameObject currentRoom;

	private IEnumerator attackRepeater;

    // Use this for initialization
    void Awake()
    {
        monsterInRange = false;
		currentRoom = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().spawnRoom;
        currentRoomScript = currentRoom.GetComponent<RoomScript>();
    }

    // Update is called once per frame
    void Update()
    {

        if (this.currentRoomScript.monsterInRoom == true)
        {
            var coroutine = Attack(2f);
            StartCoroutine(coroutine);
        }
        //else
        {
            var routine = CheckCurrentRoom(5f);
            StartCoroutine(routine);
        }

    }
		
   /* 
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            Debug.Log("Monster Hit");
            currentMonsterScript = other.gameObject.GetComponent<MonsterScript>();
            currentMonster = other.gameObject;
            movementSpeed = 0;
            monsterInRange = true;
        }

        if (other.CompareTag("Room"))
        {
            currentRoomScript = other.GetComponent<RoomScript>();
        }
    }
    */

	private IEnumerator Attack(float attackSpeed)
	{
		while (true)
		{
            currentMonster = currentRoomScript.roomMembers[0];
            yield return new WaitForSeconds(attackSpeed);
            if (currentMonster != null)
            {
                currentMonster.GetComponent<MonsterScript>().TakeDamage(damage);
                //print(currentMonster.GetComponent<MonsterScript>().currentHealth);
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
                currentRoomScript = currentRoom.GetComponent<RoomScript>();
                transform.position = currentRoom.transform.position;
                StopAllCoroutines();
            }

        }
    }

    //next two functions are what monsters will call to damage the hero
    public void TakeDamage(int damageTaken)
    {
        heroHp -= damageTaken;
		Debug.Log (heroHp);
        if (heroHp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        currentMonsterScript.heroInRoom = false;
        Destroy(this);
    }
}
