using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMonster : MonoBehaviour {

    //TODO: not sure the best way to handle this @avery but im putting it here for now - Nate
    public int applicationLife = 3;
   
    public List<GameObject> department;

	//Private variables for this monsters stats
	//Some made TEMPORARILY public for test purposes
	string type;
	public string monName;
	string archetype;
	public int curHealth;
	int maxHealth;
	int baseHealth;
	public int curDamage;
	int damage;
	public TraitBase personality;
	int salary;
	float stress;
	float morale;
	float moraleGain = 1f;
	float moraleLoss = 1f;
	float stressGain;
	float vacationStressLoss;
	int infamyGain;
	int threat;
	int armor;
	int workEthic;
	int size;
	int tier;
	bool inCombat;
	bool hasFought;
	bool monsterGrabbed;
	bool heroInRoom;
	GameObject curRoom;
	Text damageText;
	GameManager gameManager;
	float xpMod = 1f;
	float goldMod = 1f;
	int breakdowns = 3;
	public bool canBeDebuffed = true;
	float promotionMod = 1f;

	//Temp variables from transfer
	private TraitBase[] allTraits = new TraitBase[] {new CowardlyTrait(), new FancyTrait(), new FlirtyTrait(), new GrossTrait(), new GuardianTrait(), new PridefulTrait(), new RecklessTrait(), new SlackerTrait(), new TyrantTrait(), new WaryTrait(), new WorkaholicTrait()};
	string traitName;
	public float baseNerve = .5f; //public to be edited in editor
	public float curNerve = .5f; //public to be edited in editor

	//List to hold everything currently affecting the monster
	public List<string> effects = new List<string>();

	void Awake() {
		gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		//monsterGrabbed = true; //this is outdated I think, need to sort that out soon
        //setCurRoom(GameObject.FindGameObjectWithTag("Room"));
       // this.transform.position.Set(curRoom.transform.position.x, curRoom.transform.position.y, 0);
		heroInRoom = false;
		//Temp measure to suppress errors
		//this.AssignStats (gameObject.name);
		this.personality = allTraits [Random.Range (0, allTraits.Length)];
		this.personality.ApplyBase (this);
		this.traitName = this.personality.getName ();
		this.workEthic = Random.Range (-1, 1);
		this.curRoom = gameManager.spawnRoom;

        //moved damage text here bc it was throwing error on pre-instantiated monsters
        damageText = this.gameObject.GetComponentInChildren<Text>();
    }

	///<summary>
	///Assigns all stats to this monster, to be used in place of super.
	/// Defaults stress and morale, as well as gain, loss and infamy
	///</summary>
	/// <param name="nm">Name of Monster</param>
	/// <param name="hp">Total Health</param>
	/// <param name="dam">Base Damage</param>
	/// <param name="trait">Personality Trait</param>
	/// <param name="sal">Base Salary</param>
	/// <param name="thr">Threat</param>
	/// <param name="arm">Base Armor</param>
	/// <param name="ethic">Work Ethic</param>
	/// <param name="sz">Monster Size</param>
	public void AssignStats(string nm, int hp, int dam, TraitBase trait, int sal, int thr, int arm, int ethic, int sz) {
		this.monName = nm;
		this.maxHealth = hp;
		this.baseHealth = hp;
		this.curHealth = maxHealth;
		this.damage = dam;
		this.curDamage = this.damage;
		this.personality = trait;
		this.salary = sal;
		this.threat = thr;
		this.stress = 0f;
		this.morale = .5f;
		this.stressGain = .02f;
		this.vacationStressLoss = .15f;
		this.infamyGain = 1;
		this.armor = arm;
		//damageText = this.gameObject.GetComponentInChildren<Text>();
		this.workEthic = ethic;
		this.size = sz;
	}

	///<summary>
	///Assigns all stats to this monster from monster stats sheet
	///</summary>
	/// <param name="type">Name of Monster Type</param>
	public void AssignStats(string type) {
		//gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
		this.type = type;
		curRoom = gameManager.spawnRoom;
		this.maxHealth = int.Parse(gameManager.monsters.data [type] ["Health"]);
        this.maxHealth *= gameManager.healthWeight;
        switch((Random.Range(1, 7) + Random.Range(1, 7) + Random.Range(1, 7)))
        {
            case 3: case 4:
                this.maxHealth = (int)(this.maxHealth * 0.75f);
                break;
            case 5: case 6: case 7:
                this.maxHealth = (int)(this.maxHealth * 0.9f);
                break;
            default:
                break;
            case 14: case 15: case 16:
                this.maxHealth = (int)(this.maxHealth * 1.1f);
                break;
            case 17: case 18:
                this.maxHealth = (int)(this.maxHealth * 1.25f);
                break;
        }
		this.curHealth = this.maxHealth;
		this.baseHealth = curHealth;

		this.damage = int.Parse(gameManager.monsters.data [type] ["Attack"]);
        this.damage *= gameManager.attackWeight;
		this.curDamage = damage;
        switch((Random.Range(1, 7)) + Random.Range(1, 7) + Random.Range(1, 7))
        {
            case 3:
            case 4:
                this.damage = (int)(this.damage * 0.75f);
                break;
            case 5:
            case 6:
            case 7:
                this.damage = (int)(this.damage * 0.9f);
                break;
            default:
                break;
            case 14:
            case 15:
            case 16:
                this.damage = (int)(this.damage * 1.1f);
                break;
            case 17:
            case 18:
                this.damage = (int)(this.damage * 1.25f);
                break;
        }

		this.armor = int.Parse(gameManager.monsters.data [type] ["Defense"]);
        this.armor *= gameManager.defenseWeight;
        switch ((Random.Range(1, 7)) + Random.Range(1, 7) + Random.Range(1, 7))
        {
            case 3:
            case 4:
                this.armor = (int)(this.armor * 0.75f);
                break;
            case 5:
            case 6:
            case 7:
                this.armor = (int)(this.armor * 0.9f);
                break;
            default:
                break;
            case 14:
            case 15:
            case 16:
                this.armor = (int)(this.armor * 1.1f);
                break;
            case 17:
            case 18:
                this.armor = (int)(this.armor * 1.25f);
                break;
        }

        this.threat = int.Parse(gameManager.monsters.data [type] ["Threat"]);
		this.size = int.Parse (gameManager.monsters.data [type] ["Size"]);
		this.tier = int.Parse (gameManager.monsters.data [type] ["Tier"]);
		this.salary = int.Parse (gameManager.monsters.data [type] ["Cost"]);
		this.archetype = gameManager.monsters.data [type] ["Archetype"];
		int num = Random.Range (1, 5);
		this.monName = gameManager.monNames.data [num.ToString ()] [this.archetype];
		this.name = monName;

		this.stress = 0f;
		this.morale = .5f;
		this.stressGain = .02f;
		this.vacationStressLoss = .15f;
		this.infamyGain = 1;
		damageText = this.gameObject.GetComponentInChildren<Text>();
	}

	//Getters for most stats
	public int getCurHealth() {
		return this.curHealth;
	}
	public int getMaxHealth() {
		return this.maxHealth;
	}
	public int getBaseHealth() {
		return this.baseHealth;
	}
	public int getCurDamage() {
		return this.curDamage;
	}
	public int getBaseDamage() {
		return this.damage;
	}
	public void addDamage(int d) {
		this.curDamage += d;
	}
	public void setDamage(int d) {
		this.damage = d;
	}
	public void setCurDamage(int d) {
		this.curDamage = d;
	}
	public TraitBase getTrait() {
		return this.personality;
	}
	public int getSalary() {
		return this.salary;
	}
	public int getThreat() {
		return this.threat;
	}
	public void setThreat(int t) {
		this.threat = t;
	}
	public void addThreat(int t) {
		this.threat += t;
	}
	public int getArmor() {
		return this.armor;
	}
	public float getMorale() {
		return this.morale;
	}
	public void setMorale(float newM) {
		this.morale = newM;
	}
	public float getMoraleGain() {
		return this.moraleGain;
	}
	public void setMoraleGain(float newM) {
		this.moraleGain = newM;
	}
	public float getMoraleLoss() {
		return this.moraleLoss;
	}
	public void setMoraleLoss(float newM) {
		this.moraleLoss = newM;
	}
	public float getStress() {
		return this.stress;
	}
	public void setStress(float newS) {
		this.stress = newS;
	}
	public float getStressGain() {
		return this.stressGain;
	}
	public void setStressGain(float newGain) {
		this.stressGain = newGain;
	}
	public float getStressLoss() {
		return this.vacationStressLoss;
	}
	public void setStressLoss(float loss) {
		this.vacationStressLoss = loss;
	}
	public int getInfamyGain() {
		return this.infamyGain;
	}
	public void setInfamyGain(int gain) {
		this.infamyGain = gain;
	}
	public bool isInCombat() {
		return this.inCombat;
	}
	public void setInCombat(bool value) {
		this.inCombat = value;
	}
	public bool getHasFought() {
		return this.hasFought;
	}
	public void setHasFought(bool value) {
		this.hasFought = value;
	}
	public string getName() {
		return this.monName;
	}
	public void setName(string nm) {
		this.name = nm;
	}
	public int getWorkEthic() {
		return this.workEthic;
	}
	public int getSize() {
		return this.size;
	}
	public string getTraitName() {
		return this.traitName;
	}
	public RoomScript getCurRoom() {
		return this.curRoom.GetComponent<RoomScript> ();
	}
	public void setCurRoom(GameObject room) {
		this.curRoom = room;
	}
	public string getType() {
		return this.type;
	}
	public string getArchetype() {
		return this.archetype;
	}
	public float getXPMod() {
		return this.xpMod;
	}
	public void setXPMod(float newM) {
		this.xpMod = newM;
	}
	public float getGoldMod() {
		return this.goldMod;
	}
	public void setGoldMod(float newM) {
		this.goldMod = newM;
	}
	public int getBreakdowns() {
		return this.breakdowns;
	}
	public void setBreakdowns(int breaks) {
		this.breakdowns = breaks;
	}
	public float getPromotionMod() {
		return this.promotionMod;
	}
	public void setPromotionMod(float mod) {
		this.promotionMod = mod;
	}

	//Function to make monster lose health
	public void TakeDamage(int dam) {
		this.curHealth = Mathf.Clamp (this.curHealth - dam, 0, this.maxHealth);
		damageText.text = this.curHealth.ToString();
		if (curHealth <= 0) {
			this.Death ();
		}
	}

	//Function to make monster gain health
	public void Heal(int heal) {
		this.curHealth = Mathf.Clamp (this.curHealth + heal, 0, this.maxHealth);
	}

	//Default attack function that hits the hero with the highest threat
	public virtual void Attack() {
		BaseHero heroScript;
		BaseHero highThreat = null;
		int threat = int.MinValue;
		foreach (GameObject hero in this.getCurRoom().heroesInRoom) {
			if (hero != null) {
				heroScript = hero.GetComponent<BaseHero> ();

				if (heroScript != null) {
					if (heroScript.getThreat () > threat) {
						highThreat = heroScript;
						threat = heroScript.getThreat ();
					}
				}
			}
		}

		//Makes hero take damage
		if (highThreat != null)
			highThreat.TakeDamage(this.getCurDamage());
	}

	private void Death()
	{
		curRoom.GetComponent<RoomScript>().roomMembers.Remove(gameObject);
        gameManager.monsterList.Remove(gameObject);
		if (curRoom.GetComponent<RoomScript>().roomMembers.Count == 0)
		{
			curRoom.GetComponent<RoomScript>().monsterInRoom = false;
		}
		//Destroy(gameObject);
		gameObject.SetActive(false);
        gameManager.AddToDepartment(gameObject, gameManager.deadMonsters);
        gameManager.UpdateDepartments();
	}

	//Applies personality effects to the monster, as well as other stat modifiers
	//Called at end of each work day
	public void DayHandler() {
	}
}
