using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiringUIScript : MonoBehaviour {

    public GameObject resume;
    //public GameObject resumeCanvas;
    private GameObject resumeButton;
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
        resumeButton = GameObject.FindGameObjectWithTag("ResumeButton");
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
            resume = Instantiate(resume, resumeSpawn.position, Quaternion.identity);
            resume.SetActive(true);

            monsterInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SpawnMonster(resume);
            monsterInstance.SetActive(false);
            var monsterInstanceScript = monsterInstance.GetComponent<MonsterScript>();
            
            // Resume Text
            GameObject.Find("NameText").GetComponent<Text>().text = monsterInstanceScript.monsterName;
            GameObject.Find("Trait1Text").GetComponent<Text>().text = monsterInstanceScript.trait1;
            GameObject.Find("Trait2Text").GetComponent<Text>().text = monsterInstanceScript.trait2;
            GameObject.Find("HealthText").GetComponent<Text>().text = "Health " + monsterInstanceScript.startingHealth;
            GameObject.Find("DamageText").GetComponent<Text>().text = "Damage " + monsterInstanceScript.attackDamage;
            GameObject.Find("SalaryText").GetComponent<Text>().text = "Requested Salary: $" + monsterInstanceScript.attackDamage;            
        }
    }
}
