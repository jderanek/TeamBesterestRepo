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

    public RoomScript currentRoomScript;
    public GameObject currentRoom;

	private IEnumerator attackRepeater;

    // Use this for initialization
    void Awake()
    {
        monsterInRange = false;
        currentRoom = GameObject.FindGameObjectWithTag("Spawn Room");
        currentRoomScript = currentRoom.GetComponent<RoomScript>();
    }

    // Update is called once per frame
    void Update()
    {
        //CheckCurrentRoom();

        //movement script
        //transform.position = Vector3.MoveTowards(transform.position, monsterPosition.position, movementSpeed * Time.deltaTime);

        if (this.currentRoomScript.monsterInRoom == true)
        {
            //Attack();
            //var coroutine = attackTimer(2f);
            //StartCoroutine(coroutine);
            print("attack");
        }
        else
        {
            var routine = CheckCurrentRoom(5f);
            StartCoroutine(routine);
        }

    }

    /*void CheckCurrentRoom()
    {

        if (currentRoomScript.roomMembers != null)
        {
            print("bye");
            currentMonster = currentRoomScript.roomMembers[0];
        }
        RoomMemberSorter roomSorter = new RoomMemberSorter();
        currentRoomScript.roomMembers.Sort(roomSorter);

		if (currentRoomScript.roomMembers == null) 
		{
            print("hi");
		}
    }*/
		
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

	private IEnumerator attackTimer(float attackSpeed)
	{
		while (true)
		{
			yield return new WaitForSeconds(attackSpeed);
			Attack ();
		}
	}

    private IEnumerator CheckCurrentRoom(float timer)
    {
        while (true)
        {
            yield return new WaitForSeconds(timer);
            print("bye");
           	//this.currentRoom = currentRoomScript.northRoom;

			currentRoom = currentRoomScript.CheckNeighbors ();

            currentRoomScript = currentRoom.GetComponent<RoomScript>();
            this.transform.position = currentRoom.transform.position;
            StopAllCoroutines();
        }
    }

	public void Pathfinding()
	{
		//currentRoomScript;
	}

    public void Attack()
    {
        currentMonster.GetComponent<MonsterScript>().TakeDamage(damage);
    }

    //next two functions are what monsters will call to damage the hero
    public void TakeDamage(int damageTaken)
    {
        heroHp -= damageTaken;

        if (heroHp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        currentMonsterScript.heroInRange = false;
        Destroy(this);
    }
}
