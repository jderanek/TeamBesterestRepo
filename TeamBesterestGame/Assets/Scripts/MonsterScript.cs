using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterScript : MonoBehaviour {


    public int StartingHealth;
    public int CurrentHealth;
    public int Damage;

    public GameObject[] Traits;
    public GameObject Trait1;
    public GameObject Trait2;


    void Awake ()
    {
        CurrentHealth = StartingHealth;
        int Trait1Index = Random.Range(0, Traits.Length);
        int Trait2Index = Random.Range(0, Traits.Length);
        Trait1 = Traits[Trait1Index];
        Trait2 = Traits[Trait2Index];
	}
	
	void Update ()
    {
		
	}
}
