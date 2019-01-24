using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class GlobalFlagManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    [YarnCommand("CharacterDeath")]
    public void CharacterDeath(string character)
    {

    }
}
