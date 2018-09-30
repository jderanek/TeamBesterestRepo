using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class BaseMonster : MonoBehaviour {
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
    public List<BaseTrait> traits;
    public List<PersonalityTags.Tag> tags;
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
    UIManager uiManager;
	float xpMod = 1f;
	float goldMod = 1f;
	int breakdowns = 3;
	public bool canBeDebuffed = true;
	float promotionMod = 1f;
    int applicationLife = 3;
	bool stunned = false;

    public int healthTier;
    public int defenseTier;
    public int attackTier;

    public Animator anim; //= this.gameObject.GetComponentInChildren<Animator>();

    //Temp variables from transfer
    private TraitBase[] allTraits = new TraitBase[] {new CowardlyTrait(), new FancyTrait(), new FlirtyTrait(), new GrossTrait(), new GuardianTrait(), new PridefulTrait(), new RecklessTrait(), new SlackerTrait(), new TyrantTrait(), new WaryTrait(), new WorkaholicTrait()};
	string traitName;
	public float baseNerve = .5f; //public to be edited in editor
	public float curNerve = .5f; //public to be edited in editor

	//List to hold everything currently affecting the monster
	public List<string> effects = new List<string>();
    
    public List<BaseTrait> traitsToReveal = new List<BaseTrait>();
    public List<String> revealedTraits = new List<String>();

    public List<PersonalityTags.Tag> revealedTags = new List<PersonalityTags.Tag>();


    void Awake() {
		gameManager = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameManager> ();
        uiManager = gameManager.GetComponent<UIManager>();
		//monsterGrabbed = true; //this is outdated I think, need to sort that out soon
        //setCurRoom(GameObject.FindGameObjectWithTag("Room"));
       // this.transform.position.Set(curRoom.transform.position.x, curRoom.transform.position.y, 0);
		heroInRoom = false;
		//Temp measure to suppress errors
		//this.AssignStats (gameObject.name);
		this.personality = allTraits [UnityEngine.Random.Range (0, allTraits.Length)];
		this.personality.ApplyBase (this);
		this.traitName = this.personality.getName ();
		this.workEthic = UnityEngine.Random.Range (-1, 1);
        //this.curRoom = gameManager.spawnRoom;

        //Adds three traits to the trait list
        traits = new List<BaseTrait>();
        int rand;
        List<string> traitList = new List<string>(gameManager.traits);
        for (int x = 0; x < 3; x++)
        {
            rand = UnityEngine.Random.Range(0, traitList.Count-1);

            //Debug.Log(traitList[rand].ToString());
            BaseTrait toAdd = Activator.CreateInstance(System.Type.GetType(traitList[rand]), 
                gameManager.traitTags[traitList[rand]]) as BaseTrait;
            traits.Add(toAdd);

            foreach (PersonalityTags.Tag tag in toAdd.GetTags())
            {
                if (!tags.Contains(tag))
                    tags.Add(tag);
            }

            //revealedTraits.Add(traitList[rand]);

            traitList.RemoveAt(rand);
        }

        //moved damage text here bc it was throwing error on pre-instantiated monsters
        damageText = this.gameObject.GetComponentInChildren<Text>();
        anim = this.gameObject.GetComponentInChildren<Animator>();
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
        switch((UnityEngine.Random.Range(1, 7) + UnityEngine.Random.Range(1, 7) + UnityEngine.Random.Range(1, 7)))
        {
            case 3: case 4:
                this.maxHealth = (int)(this.maxHealth * 0.75f);
                this.healthTier = -1;
                break;
            case 5: case 6: case 7:
                this.maxHealth = (int)(this.maxHealth * 0.9f);
                this.healthTier = -1;
                break;
            default:
                this.healthTier = 0;
                break;
            case 14: case 15: case 16:
                this.maxHealth = (int)(this.maxHealth * 1.1f);
                this.healthTier = 1;
                break;
            case 17: case 18:
                this.maxHealth = (int)(this.maxHealth * 1.25f);
                this.healthTier = 1;
                break;
        }
		this.curHealth = this.maxHealth;
		this.baseHealth = curHealth;

		this.damage = int.Parse(gameManager.monsters.data [type] ["Attack"]);
        this.damage *= gameManager.attackWeight;
        switch((UnityEngine.Random.Range(1, 7)) + UnityEngine.Random.Range(1, 7) + UnityEngine.Random.Range(1, 7))
        {
            case 3:
            case 4:
                this.damage = (int)(this.damage * 0.75f);
                this.attackTier = -1;
                break;
            case 5:
            case 6:
            case 7:
                this.damage = (int)(this.damage * 0.9f);
                this.attackTier = -1;
                break;
            default:
                break;
            case 14:
            case 15:
            case 16:
                this.damage = (int)(this.damage * 1.1f);
                this.attackTier = 1;
                break;
            case 17:
            case 18:
                this.damage = (int)(this.damage * 1.25f);
                this.attackTier = 1;
                break;
        }
        this.curDamage = damage;

        this.armor = int.Parse(gameManager.monsters.data [type] ["Defense"]);
        this.armor *= gameManager.defenseWeight;
        switch ((UnityEngine.Random.Range(1, 7)) + UnityEngine.Random.Range(1, 7) + UnityEngine.Random.Range(1, 7))
        {
            case 3:
            case 4:
                this.armor = (int)(this.armor * 0.75f);
                this.defenseTier = -1;
                break;
            case 5:
            case 6:
            case 7:
                this.armor = (int)(this.armor * 0.9f);
                this.defenseTier = -1;
                break;
            default:
                this.defenseTier = 0;
                break;
            case 14:
            case 15:
            case 16:
                this.armor = (int)(this.armor * 1.1f);
                this.defenseTier = 1;
                break;
            case 17:
            case 18:
                this.armor = (int)(this.armor * 1.25f);
                this.defenseTier = 1;
                break;
        }

        this.threat = int.Parse(gameManager.monsters.data [type] ["Threat"]);
		this.size = int.Parse (gameManager.monsters.data [type] ["Size"]);
		this.tier = int.Parse (gameManager.monsters.data [type] ["Tier"]);
		this.salary = int.Parse (gameManager.monsters.data [type] ["Cost"]);
		this.archetype = gameManager.monsters.data [type] ["Archetype"];
		int num = UnityEngine.Random.Range (1, 25);
        bool useArchetype = (UnityEngine.Random.value > .3f);
        if (useArchetype)
    		this.monName = gameManager.monNames.data [num.ToString ()] [this.archetype];
        else
            this.monName = gameManager.monNames.data[num.ToString()]["Any"];
        this.name = monName;

		this.stress = 0f;
		this.morale = .5f;
		this.stressGain = .02f;
		this.vacationStressLoss = .15f;
		this.infamyGain = 1;
		damageText = this.gameObject.GetComponentInChildren<Text>();
        damageText.text = this.getCurHealth().ToString() + "hp";
        anim = this.gameObject.GetComponentInChildren<Animator>();
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
        if (this.inCombat)
        {
            anim.SetBool("inCombat", true);
        }
        else
        {
            anim.SetBool("inCombat", false);
        }
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
	public BaseRoom getCurRoom() {
        if (curRoom != null)
        {
            return this.curRoom.GetComponent<BaseRoom>();
        }
		else
        {
            return null;
        }
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
    public int getApplicationLife()
    {
        return applicationLife;
    }
    public void setApplicationLife(int newAppLife)
    {
        applicationLife = newAppLife;
    }
    public int getDefense()
    {
        return this.defenseTier;
    }
    public void setDefense(int def)
    {
        this.defenseTier = def;
    }
	public void Stun() {
		this.stunned = true;
	}

	//Function to make monster lose health
	public void TakeDamage(int dam, BaseHero attacker = null) {
        foreach (BaseTrait trait in this.traits)
        {
            dam = trait.OnAttacked(dam, this, attacker);
        }
		this.curHealth = Mathf.Clamp (this.curHealth - dam, 0, this.maxHealth);
		damageText.text = this.curHealth.ToString() + "hp";
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
		//Breaks attack function if stunned
		if (stunned) {
			this.stunned = false;
			return;
		}

		BaseHero heroScript;
		BaseHero highThreat = null;
		int threat = int.MinValue;
        if (curRoom != null)
        {
            foreach (GameObject hero in this.getCurRoom().heroesInRoom)
            {
                if (hero != null)
                {
                    heroScript = hero.GetComponent<BaseHero>();

                    if (heroScript != null)
                    {
                        if (heroScript.getThreat() > threat)
                        {
                            highThreat = heroScript;
                            threat = heroScript.getThreat();
                        }
                    }
                }
            }
        }

        //Makes hero take damage
        if (highThreat != null)
        {
            int dmg = this.getCurDamage();
            foreach (BaseTrait trait in this.traits)
            {
                dmg = trait.OnAttack(dmg, highThreat, this);
            }
            highThreat.TakeDamage(dmg, this);
        }
	}

    //Plays animations, and kills this monster
	private void Death()
	{
        //Runs all trait death functions before removing monster
        foreach (BaseTrait trait in this.traits)
        {
            trait.OnDeath(this);
        }

        //anim.SetTrigger("death");
        //anim.Play("hellhound_death");
		curRoom.GetComponent<BaseRoom>().roomMembers.Remove(gameObject);
        gameManager.monsterList.Remove(gameObject);
		if (curRoom.GetComponent<BaseRoom>().roomMembers.Count == 0)
		{
			curRoom.GetComponent<BaseRoom>().monsterInRoom = false;
		}
		//Destroy(gameObject);
		gameObject.SetActive(false);
        gameManager.AddToDepartment(gameObject, gameManager.deadMonsters);
        uiManager.UpdateDepartments();
	}

	//Applies personality effects to the monster, as well as other stat modifiers
	//Called at end of each work day
	public void DayHandler()
    {

	}

    private void OnMouseDown()
    {
        gameManager.selectedObjects.Clear();
        gameManager.selectedObjects.Add(gameObject);
        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void RevealTraits()
    {
        revealedTraits.Clear();
        foreach (BaseTrait trait in traits)
        {
            int j = 0;
            //for each tag in each trait on the monster if a tag isn't revealed for that trait increment j
            //at the end of the loop if j > 0 the trait is not revealed
            for (int i = 0; i < trait.GetTags().Count - 1; i++)
            {
                if (!revealedTags.Contains(trait.GetTags()[i]))
                {
                    j++;
                }
            }
            if (j <= 0)
            {
                revealedTraits.Add(trait.traitName);
            }
        }
    }

}
