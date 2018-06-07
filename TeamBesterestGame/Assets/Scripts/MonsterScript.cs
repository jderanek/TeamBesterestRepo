using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterScript : MonoBehaviour
{
    public string[] possibleNames; //public to be edited in editor
    public string monsterName; //public to be accessed by UI stuff

    public string monsterType; //public to be accessed by UI stuff

    //this stuff needs to be reworked, specifically how it interacts with the Resume menu... since the resume technically shouldnt have this stuff listed on them?
    public int averageHealth; //public to be accessed by UI stuff
    public int startingHealth; //public to be accessed by UI stuff
    public int currentHealth; //public to be accessed by tyrant script?
	private Text damageText; //public to be assigned in editor

    private int[] possibleThreatValue = new int[] { 2, 4, 6 }; 
    public int threatValue; //public to be accessed by hero script

    public int averageDamage; //public to be accessed by scripts
    public int attackDamage; //public to be accessed by scripts

    //possibly obsolete
    public string[] possibleTraits = new string[] { "Aggressive", "Annoying", "Irritable", "Friendly", "Hard-Working" };
    public string trait1;
    public string trait2;

    public int averageSalary = 500; //public to be edited in editor
    public int requestedSalary; //public to be accessed by scripts

    private bool monsterGrabbed;

    private GameObject hero;
    //private HeroScript heroScript; //uneeded?
    public bool heroInRoom = false; //note to self, have rooms update monsters on inhabitants at on the time pass function

    public string status = "Break Room";
    public GameObject myRoom; //public to be assigned in editor

    //Stress, Morale, Nerve and Size
    //Temporarily public for test purposes
    public float stress; //public to be accessed by scripts
	public float morale; //public to be accessed by scripts
	public float baseNerve = .5f; //public to be edited in editor
	public float curNerve = .5f; //public to be edited in editor
	public int size; //public to be accessed by scripts
	public int curThreat; //public to be accessed by scripts
	public float stressGain = .02f; //public to be edited in editor
	public float vacationStressLoss = .15f; //public to be edited in editor
	//No idea what this should be
	public int infamyGain = 1; //public to be edited in editor
	public string gender; //maybe make this bool later, would be kinda funny but more efficent... doesnt do anything right now

	//Attack damage after stress and morale modifers
	public int curDamage; //public to be accessed by other scripts... maybe

	//Monster personality, of type TraitBase
	public TraitBase personality; //public to be accessed by other scripts
	//String name of personality, for debugging
	public string traitName; //public to be accessed by other scripts

	public int workEthic;//variable used to fake a workethic value for interviewing //public to be accessed by other scripts

	//List of all personalities
	private TraitBase[] allTraits = new TraitBase[] {new CowardlyTrait(), new FancyTrait(), new FlirtyTrait(), new GrossTrait(), new GuardianTrait(), new PridefulTrait(), new RecklessTrait(), new SlackerTrait(), new TyrantTrait(), new WaryTrait(), new WorkaholicTrait()};

    public bool inCombat; //public to be accesed by other scripts

	//Boolean to keep track of whether the monster has fought this week
	public bool hasFought = false; //public to be accessed by trait script

    void Awake()
    {
		stress = 25.0f;
		morale = .5f;
        monsterName = possibleNames[Random.Range(0, possibleNames.Length)];
        startingHealth = averageHealth + Random.Range(-5, 5);
        currentHealth = startingHealth;

		damageText = this.gameObject.GetComponentInChildren<Text>();//fetches DmgText

        attackDamage = averageDamage + Random.Range(-3, 3);
        threatValue = possibleThreatValue[Random.Range(0, possibleThreatValue.Length)];

		trait1 = possibleTraits[Random.Range(0, possibleTraits.Length)];//possibly obsolete
        trait2 = possibleTraits[Random.Range(0, possibleTraits.Length)];
        if (trait1 == trait2)
        {
            trait2 = null;
            trait1 = "Very " + trait1;
        }

        requestedSalary = averageSalary + Random.Range(-500, 500);
        if (requestedSalary < 0)
            requestedSalary = 100;

        monsterGrabbed = true;
        
        heroInRoom = false;
		this.curThreat = threatValue;

		this.personality = allTraits [Random.Range (0, allTraits.Length)];
		//this.personality.ApplyBase (this);
		this.traitName = this.personality.getName ();

		this.workEthic = Random.Range (-1, 1);// see workEthic variable for explanation of why i did this nonsense
    }

    void Update()
    {

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !inCombat)
        {
            //GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().PickUpObject(this.gameObject);
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SelectObject(this.gameObject);
        }
    }

	public void Attack() {

		//Updates this monsters attack damage
		//this.curDamage = (int) Mathf.Clamp((int)(this.attackDamage * (((1-stress) + morale/2))), 1, 100); // this line is causing monsters to never do more than 1 damage, need to take a look at the formula later

		//Debug.Log (curDamage);

		if (myRoom.GetComponent<RoomScript>().heroInRoom) //sometimes throws error, not sure what the cause is //error is caused by hiring a monster while time is passing, too lazy to fix right now
		{
			hero = myRoom.GetComponent<RoomScript>().heroesInRoom[0];
		}

		if (hero != null)
		{
			hero.GetComponent<BaseHero>().TakeDamage(attackDamage);
		}

	}

    //next two functions are what the monster will call to take damage
    public void TakeDamage(int damageTaken)
    {
        currentHealth -= damageTaken;
		damageText.text = damageTaken.ToString();
		//play animation to make damageText appear and disappear


        if (currentHealth <= 0)
        {
            Death();
        }

        //stress += 5.0f;
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