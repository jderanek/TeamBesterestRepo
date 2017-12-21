using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public string[] possibleNames;
    public string monsterName;

    public string monsterType;

    public int averageHealth;
    public int startingHealth;
    public int currentHealth;

    public int[] possibleThreatValue = new int[] { 2, 4, 6 };
    public int threatValue;

    public int averageDamage;
    public int attackDamage;

    public string[] possibleTraits = new string[] { "Aggressive", "Annoying", "Irritable", "Friendly", "Hard-Working" };
    public string trait1;
    public string trait2;

    public int averageSalary = 500;
    public int requestedSalary;

    private GameObject resume;
    private GameObject monsterInstance;
    private bool monsterGrabbed;
    private GameObject resumeButton;

    private GameObject hero;
    private HeroScript heroScript;
    public bool heroInRoom = false;

    //public List<GameObject> myList = null;
    public GameObject myRoom;

	private IEnumerator attackRepeater;

    void Awake()
    {

        monsterName = possibleNames[Random.Range(0, possibleNames.Length)];
        startingHealth = averageHealth + Random.Range(-5, 5);
        currentHealth = startingHealth;

        attackDamage = averageDamage + Random.Range(-3, 3);
        threatValue = possibleThreatValue[Random.Range(0, possibleThreatValue.Length)];

        trait1 = possibleTraits[Random.Range(0, possibleTraits.Length)];
        trait2 = possibleTraits[Random.Range(0, possibleTraits.Length)];
        if (trait1 == trait2)
        {
            trait2 = null;
            trait1 = "Very " + trait1;
        }
        requestedSalary = averageSalary + Random.Range(-500, 500);

        resume = GameObject.FindGameObjectWithTag("Resume");
        monsterGrabbed = true;
        monsterInstance = this.gameObject;

        resumeButton = GameObject.FindGameObjectWithTag("ResumeButton");
        heroInRoom = false;
    }

    void Update()
    {
        //if HeroInRoom is true, the Attack function will run
        if (heroInRoom)
        {
			var coroutine = Attack(2f);
			StartCoroutine(coroutine);
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().PickUpObject(this.gameObject);
        }
    }


    /*public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            heroScript = other.gameObject.GetComponent<HeroScript>();
            hero = other.gameObject;
            heroInRange = true;
        }
    }*/

	private IEnumerator Attack(float attackSpeed)
	{
		while (true)
		{
			yield return new WaitForSeconds(attackSpeed);
            hero.GetComponent<HeroScript>().TakeDamage(attackDamage);
        }
	}

    //next two functions are what the monster will call to take damage
    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;

        if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        myRoom.GetComponent<RoomScript>().roomMembers.Remove(this.gameObject);
        if (myRoom.GetComponent<RoomScript>().roomMembers.Count == 0)
        {
            myRoom.GetComponent<RoomScript>().monsterInRoom = false;
        }
        Destroy(this.gameObject);
    }
}