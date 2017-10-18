using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiringUIScript : MonoBehaviour {

    public GameObject resume;
    private GameObject resumeInstance;
    private GameObject resumeButton;
    public Transform resumeSpawn;
    [HideInInspector]
    public bool resumeUp;

    public GameObject monster;
    [HideInInspector]
    public GameObject monsterInstance;

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
            resumeInstance = Instantiate(resume, resumeSpawn.position, Quaternion.identity);
            resumeInstance.SetActive(true);

            monsterInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SpawnMonster(resume);
            monsterInstance.SetActive(false);
            var monsterInstanceScript = monsterInstance.GetComponent<MonsterScript>();

            // Resume Text
            resumeInstance.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(0).GetComponent<Text>().text = monsterInstanceScript.monsterName;
            resumeInstance.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(2).GetComponent<Text>().text = monsterInstanceScript.trait1;
            resumeInstance.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(3).GetComponent<Text>().text = monsterInstanceScript.trait2;
            resumeInstance.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(5).GetComponent<Text>().text = "Health " + monsterInstanceScript.startingHealth;
            resumeInstance.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(6).GetComponent<Text>().text = "Damage " + monsterInstanceScript.attackDamage;
            resumeInstance.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(7).GetComponent<Text>().text = "Requested Salary: $" + monsterInstanceScript.requestedSalary;
        }
    }
}
