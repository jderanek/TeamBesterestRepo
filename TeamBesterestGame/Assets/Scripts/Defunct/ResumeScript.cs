using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeScript : MonoBehaviour {

    public GameObject monster;
    public GameObject resumeCanvas;
    private bool holdingResume;
    private GameManager gameManager;

    //better way to grab these?
    public Button hireButton;
    public Button hideButton;
    public Button interviewButton;
    public Button nextButton;
    public Button previousButton;

    public int timeTillExpiration;

    // Use this for initialization
    void Awake()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        
		//interviewButton.onClick.AddListener(gameManager.Interview);

        timeTillExpiration = 5;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}
}
