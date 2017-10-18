using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiringUIScript : MonoBehaviour {

    public GameObject resume;
    public Transform resumeSpawn;
    [HideInInspector]
    public bool resumeUp;

    public GameObject monster;
    [HideInInspector]
    public GameObject monsterInstance;

    private Text nameText;
    private Text trait1Text;
    private Text trait2Text;
    private Text healthText;
    private Text damageText;
    private Text salaryText;

	// Use this for initialization
	void Start ()
    {
        resumeUp = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && !resumeUp)
        {
            resumeUp = true;
            Instantiate(resume, resumeSpawn.position, Quaternion.identity);
            monsterInstance = Instantiate(monster, this.transform.position, Quaternion.identity);
            var monsterScript = monsterInstance.GetComponent<MonsterScript>();

            ///* Resume Text
            nameText = GameObject.Find("NameText").GetComponent<Text>();
            trait1Text = GameObject.Find("Trait1Text").GetComponent<Text>();
            trait2Text = GameObject.Find("Trait2Text").GetComponent<Text>();
            healthText = GameObject.Find("HealthText").GetComponent<Text>();
            damageText = GameObject.Find("DamageText").GetComponent<Text>();
            salaryText = GameObject.Find("SalaryText").GetComponent<Text>();
            nameText.text = monsterScript.monsterName;
            trait1Text.text = monsterScript.trait1;
            trait2Text.text = monsterScript.trait2;
            healthText.text = "Health " + monsterScript.startingHealth;
            damageText.text = "Damage " + monsterScript.attackDamage;
            salaryText.text = "Requested Salary: $" + monsterScript.attackDamage;
            //*/
            monsterInstance.SetActive(false);
        }
    }
}
