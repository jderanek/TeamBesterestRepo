using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterScript : MonoBehaviour
{
    public string[] possibleNames;
    public string monsterName;

    public string monsterType;

    public int averageHealth;
    public int startingHealth;
    public int currentHealth;
	private Text damageText;

    public int[] possibleThreatValue = new int[] { 2, 4, 6 };
    public int threatValue;

    public int averageDamage;
    public int attackDamage;

    //possibly obsolete
    public string[] possibleTraits = new string[] { "Aggressive", "Annoying", "Irritable", "Friendly", "Hard-Working" };
    public string trait1;
    public string trait2;

    public int averageSalary = 500;
    public int requestedSalary;

    private bool monsterGrabbed;

    private GameObject hero;
    //private HeroScript heroScript; //uneeded?
    public bool heroInRoom = false; //note to self, have rooms update monsters on inhabitants at on the time pass function

    public GameObject myRoom;

	private IEnumerator attackRepeater;

	//Stress, Morale, Nerve and Size
	//Temporarily public for test purposes
	public float stress = 0;
	public float morale = .5f;
	public float baseNerve = .5f;
	public float curNerve = .5f;
	public int size;
	public int curThreat;
	public float stressGain = .02f;
	public float vacationStressLoss = .15f;
	//No idea what this should be
	public int infamyGain = 1;
	public string gender;

	//Attack damage after stress and morale modifers
	public int curDamage;

	//Monster personality, of type TraitBase
	public TraitBase personality;
	//String name of personality, for debugging
	public string traitName;

	//List of all personalities
	public TraitBase[] allTraits = new TraitBase[] {new CowardlyTrait(), new FancyTrait(), new FlirtyTrait(), new GrossTrait(), new GuardianTrait(), new PridefulTrait(), new RecklessTrait(), new SlackerTrait(), new TyrantTrait(), new WaryTrait(), new WorkaholicTrait()};

	//Boolean to keep track of whether the monster has fought this week
	public bool hasFought = false;

    void Awake()
    {
		stress = 0;
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
		this.personality.ApplyBase (this);
		this.traitName = this.personality.getName ();
    }

    void Update()
    {

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().PickUpObject(this.gameObject);
        }
    }

	public void Attack() {

		//Updates this monsters attack damage
		this.curDamage = (int) Mathf.Clamp((int)(this.attackDamage * (((1-stress) + morale/2))), 1, 100);
		Debug.Log (curDamage);

		if (myRoom.GetComponent<RoomScript>().heroInRoom) 
		{
			hero = myRoom.GetComponent<RoomScript>().heroesInRoom[0];
		}

		if (hero != null)
		{
			hero.GetComponent<HeroScript>().TakeDamage(curDamage);
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