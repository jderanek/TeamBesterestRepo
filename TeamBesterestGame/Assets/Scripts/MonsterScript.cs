using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    public string[] PossibleNames;
    public string Name;

    public int[] PossibleHealth;
    public int StartingHealth;
    public int CurrentHealth;

    public int[] PossibleDamage;
    public int AttackDamage;

    public GameObject[] PossibleTraits;
    public GameObject Trait1;
    public GameObject Trait2;

    private GameObject Resume;
    private GameObject MonsterInstance;
    public bool MonsterGrabbed;
    private GameObject ResumeButton;

    private GameObject Hero;
    private bool HeroInRange;

    void Awake()
    {
        Name = PossibleNames[Random.Range(0, PossibleNames.Length)];
        StartingHealth = PossibleHealth[Random.Range(0, PossibleHealth.Length)];
        CurrentHealth = StartingHealth;
        AttackDamage = PossibleDamage[Random.Range(0, PossibleDamage.Length)];
        int Trait1Index = Random.Range(0, PossibleTraits.Length);
        int Trait2Index = Random.Range(0, PossibleTraits.Length);
        Trait1 = PossibleTraits[Trait1Index];
        Trait2 = PossibleTraits[Trait2Index];

        Resume = GameObject.FindGameObjectWithTag("Resume");
        MonsterGrabbed = true;
        MonsterInstance = this.gameObject;

        ResumeButton = GameObject.FindGameObjectWithTag("ResumeButton");
        HeroInRange = false;
    }

    void Update()
    {

        //Monster placement start
        if (MonsterInstance != null && MonsterGrabbed)
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = transform.position.z - Camera.main.transform.position.z;
            MonsterInstance.transform.position = Camera.main.ScreenToWorldPoint(mousePos);
        }

        if (Input.GetMouseButtonDown(1) && MonsterGrabbed)
        {
            MonsterGrabbed = false;
            ResumeButton.GetComponent<HiringUIScript>().ResumeUp = false;
            //Resume.SetActive(true);
        }
        //Monster placement end

		//if HeroInRange is set to true, the Attack function will run
		if (HeroInRange) {
			Attack ();
		}

    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            Hero = other.gameObject;
            HeroInRange = true;
        }
    }

    public void Attack()
    {
        Hero.GetComponent<HeroScript>().TakeDamage(AttackDamage);

    }

	//next two functions are what the monster will call to take damage
    public void TakeDamage(int damageTaken)
    {
		CurrentHealth -= damageTaken;
		if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(this);
    }
}
