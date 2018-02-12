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

	//Stress and Morale
	//Temporarily public for test purposes
	public float stress = 0;
	public float morale = .5f;

	//Attack damage after stress and morale modifers
	public int curDamage;

	//Monster personality, of type TraitBase
	public TraitBase personality;

	//Boolean to keep track of whether the monster has fought this week
	public bool hasFought = false;

    void Awake()
    {
		stress = 0;
		morale = .5f;
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
		if (myRoom != null && myRoom.GetComponent<RoomScript>().heroInRoom)
        {
			var coroutine = Attack(2f);
			StartCoroutine(coroutine);
        }

		//Calculation to modify attack damage based on stress and morale
		curDamage = (int)((attackDamage * (1 - stress))/2) + (int)((attackDamage * (morale * 2))/2);
		//Makes sure damage is at least 1
		if (curDamage == 0) {
			curDamage = 1;
		}
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().PickUpObject(this.gameObject);
        }
    }


	private IEnumerator Attack(float attackSpeed)
	{
		hasFought = true;
		while (true)
		{
			if (myRoom.GetComponent<RoomScript>().heroInRoom) 
			{
				hero = myRoom.GetComponent<RoomScript>().heroesInRoom[0];
			}
			else 
			{
				StopAllCoroutines ();
			}

			yield return new WaitForSeconds(attackSpeed);

			if (hero != null)
			{
				hero.GetComponent<HeroScript>().TakeDamage(curDamage);
			}

			StopAllCoroutines();
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