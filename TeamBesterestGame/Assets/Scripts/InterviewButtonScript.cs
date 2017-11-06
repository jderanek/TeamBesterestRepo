using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterviewButtonScript : MonoBehaviour {

    public GameObject gameManager;
    public GameObject monsterInstance;

    //UI stuff
    //public GameObject monsterImage;
    //public GameObject interviewQuestions;


    // Use this for initialization
    void Awake ()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
        //interviewQuestions = GameObject.FindGameObjectWithTag("Interview Questions");
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            gameManager.GetComponent<GameManager>().interviewing = true;
        }
    }
}
