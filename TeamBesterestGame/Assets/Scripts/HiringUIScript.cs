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
    public Text SalaryText;

	// Use this for initialization
	void Start ()
    {
        ResumeUp = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && ResumeUp == false)
        {
            ResumeUp = true;
            Instantiate(Resume, ResumeSpawn.position, Quaternion.identity);
            MonsterInstance = Instantiate(Monster, this.transform.position, Quaternion.identity);
            var MonsterScript = MonsterInstance.GetComponent<MonsterScript>();
            ///* Resume Text
            NameText = GameObject.Find("NameText").GetComponent<Text>();
            Trait1Text = GameObject.Find("Trait1Text").GetComponent<Text>();
            Trait2Text = GameObject.Find("Trait2Text").GetComponent<Text>();
            HealthText = GameObject.Find("HealthText").GetComponent<Text>();
            DamageText = GameObject.Find("DamageText").GetComponent<Text>();
            SalaryText = GameObject.Find("SalaryText").GetComponent<Text>();
            NameText.text = MonsterScript.Name;
            Trait1Text.text = MonsterScript.Trait1.transform.name;
            Trait2Text.text = MonsterScript.Trait2.transform.name;
            HealthText.text = "Health " + MonsterScript.StartingHealth;
            DamageText.text = "Damage " + MonsterScript.AttackDamage;
            SalaryText.text = "Requested Salary: $" + MonsterScript.AttackDamage;
            //*/
            MonsterInstance.SetActive(false);
        }
    }
}
