using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterviewManager : MonoBehaviour {

    public GameObject gameManager;
    public GameObject resume;

    public GameObject interviewQuestions;
    public GameObject questionOne;
    public GameObject questionTwo;
    public GameObject questionThree;
    public GameObject interviewResponse;

    public Text responseText;

    // Use this for initialization
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Question1Response()
    {
        interviewQuestions.SetActive(false);
        interviewResponse.SetActive(true);
        responseText.text = "1";
    }

    public void Question2Response()
    {
        interviewQuestions.SetActive(false);
        interviewResponse.SetActive(true);
        responseText.text = "2";
    }

    public void Question3Response()
    {
        interviewQuestions.SetActive(false);
        interviewResponse.SetActive(true);
        responseText.text = "3";
    }

    public void InterviewResponse()
    {
        interviewResponse.SetActive(false);
        interviewQuestions.SetActive(true);
    }

    public void ExitInterview()
    {
        gameManager.GetComponent<GameManager>().interviewing = false;
    }

}
