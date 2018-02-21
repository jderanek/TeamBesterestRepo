using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResumeScript : MonoBehaviour {


    public GameObject monster;
    public GameObject resumeCanvas;
    private bool holdingResume;
    public GameManager gameManager;
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

        hideButton.onClick.AddListener(gameManager.OpenApplications);
        nextButton.onClick.AddListener(gameManager.NextApplication);
        previousButton.onClick.AddListener(gameManager.PreviousApplication);
        hireButton.onClick.AddListener(gameManager.HireButton);
        
		interviewButton.onClick.AddListener(gameManager.Interview);

        timeTillExpiration = 5;
    }
	
	// Update is called once per frame
	void Update ()
    {
        
	}

    void OnMouseOver ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().PickUpObject(this.gameObject);
        }
    }
}
