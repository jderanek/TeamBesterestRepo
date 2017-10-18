using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour
{
    private string[] possibleNames = new string[] { "Larry", "Gary" };
    public string monsterName;

    public int averageHealth;
    public int startingHealth;
    public int currentHealth;

    public int averageDamage = 5;
    public int attackDamage;

    private string[] possibleTraits = new string[] {"Aggressive", "Annoying", "Irritable", "Friendly", "Hard-Working" };
    public string trait1;
    public string trait2;

    public int averageSalary = 500;
    public int requestedSalary;

    private GameObject resume;
    private GameObject monsterInstance;
    private bool monsterGrabbed;
    private GameObject resumeButton;

    private GameObject hero;
    private bool heroInRange;

    void Awake()
    {
        averageDamage = 5;
        averageHealth = 10;
        averageSalary = 500;

        monsterName = possibleNames[Random.Range(0, possibleNames.Length - 1)];
        startingHealth = averageHealth + Random.Range(-5, 5);
        currentHealth = startingHealth;
        attackDamage = averageDamage + Random.Range(-3, 3);
        trait1 = possibleTraits[Random.Range(0, possibleTraits.Length - 1)];
        trait2 = possibleTraits[Random.Range(0, possibleTraits.Length - 1)];
        if (trait1 == trait2)
        {
            trait2 = null;
            trait1 = "Very " + trait1;
        }
        requestedSalary = averageSalary + Random.Range(-500, 500);

        resume = GameObject.FindGameObjectWithTag("Resume");
        monsterGrabbed = true;
        monsterInstance = this.gameObject;

        resumeButton = GameObject.FindGameObjectWithTag("ResumeButton");
        heroInRange = false;
    }

    void Update()
    {
        //if HeroInRange is set to true, the Attack function will run
        if (heroInRange) {
			Attack();
		}

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().PickUpMonster(this.gameObject);
        }
    }


    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hero"))
        {
            hero = other.gameObject;
            heroInRange = true;
        }
    }

    public void Attack()
    {
        hero.GetComponent<HeroScript>().TakeDamage(attackDamage);
    }

	//next two functions are what the monster will call to take damage
    public void TakeDamage(int damageTaken)
    {
		currentHealth -= damageTaken;
		if (currentHealth <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        Destroy(this);
    }
}
