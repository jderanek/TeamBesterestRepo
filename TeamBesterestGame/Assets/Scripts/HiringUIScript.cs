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

    public GameObject gameManager;


	// Use this for initialization
	void Start ()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
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
            gameManager.GetComponent<GameManager>().resume = resumeInstance;
            resumeInstance.SetActive(true);
            resumeInstance.transform.Find("Resume Picture").transform.Find("Resume Image box").GetComponent<SpriteRenderer>().sortingLayerName = "Resume";
            resumeInstance.transform.Find("Resume Picture").transform.Find("Resume Image box").transform.Find("enemy image").GetComponent<SpriteRenderer>().sortingLayerName = "Resume";
            //resumeInstance.GetComponent<>

            monsterInstance = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().SpawnMonster(resume);
            monsterInstance.SetActive(false);
            var monsterInstanceScript = monsterInstance.GetComponent<MonsterScript>();

            // Resume Text
            resumeInstance.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(0).GetComponent<Text>().text = monsterInstanceScript.monsterName;
            //resumeInstance.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(2).GetComponent<Text>().text = monsterInstanceScript.trait1;
            //resumeInstance.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(3).GetComponent<Text>().text = monsterInstanceScript.trait2;
            resumeInstance.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(2).GetComponent<Text>().text = monsterInstanceScript.monsterType;
            resumeInstance.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(4).GetComponent<Text>().text = "Average Health " + monsterInstanceScript.averageHealth;
            resumeInstance.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(5).GetComponent<Text>().text = "Average Damage " + monsterInstanceScript.averageDamage;
            resumeInstance.GetComponent<ResumeScript>().resumeCanvas.transform.GetChild(6).GetComponent<Text>().text = "Requested Salary: $" + monsterInstanceScript.requestedSalary;
        }
    }

}
