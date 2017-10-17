using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HiringUIScript : MonoBehaviour {

    public GameObject Resume;
    public Transform ResumeSpawn;
    public bool ResumeUp;

    public GameObject Monster;
    public GameObject MonsterInstance;

    public Text NameText;
    public Text Trait1Text;
    public Text Trait2Text;
    public Text HealthText;
    public Text DamageText;

	// Use this for initialization
	void Start ()
    {
        ResumeUp = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (ResumeUp)
        {
            /*
            NameText = GameObject.Find("NameText").GetComponent<Text>();
            Trait1Text = GameObject.Find("Trait1Text").GetComponent<Text>();
            Trait2Text = GameObject.Find("Trait2Text").GetComponent<Text>();
            HealthText = GameObject.Find("HealthText").GetComponent<Text>();
            DamageText = GameObject.Find("DamageText").GetComponent<Text>();
            NameText.text = MonsterInstance.GetComponent<MonsterScript>().Name;
            HealthText.text = "Health " + MonsterInstance.GetComponent<MonsterScript>().StartingHealth;
            DamageText.text = "Damage " + MonsterInstance.GetComponent<MonsterScript>().AttackDamage;
            //*/
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && ResumeUp == false)
        {
            ResumeUp = true;
            Instantiate(Resume, ResumeSpawn.position, Quaternion.identity);
            MonsterInstance = Instantiate(Monster, this.transform.position, Quaternion.identity);
            ///*
            NameText = GameObject.Find("NameText").GetComponent<Text>();
            Trait1Text = GameObject.Find("Trait1Text").GetComponent<Text>();
            Trait2Text = GameObject.Find("Trait2Text").GetComponent<Text>();
            HealthText = GameObject.Find("HealthText").GetComponent<Text>();
            DamageText = GameObject.Find("DamageText").GetComponent<Text>();
            NameText.text = MonsterInstance.GetComponent<MonsterScript>().Name;
            HealthText.text = "Health " + MonsterInstance.GetComponent<MonsterScript>().StartingHealth;
            DamageText.text = "Damage " + MonsterInstance.GetComponent<MonsterScript>().AttackDamage;
            //*/
            MonsterInstance.SetActive(false);
        }
    }
}
