using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterviewManager : MonoBehaviour {

    public GameObject gameManager;
    //public GameObject resume;
    //public GameObject hiringButton;

    public GameObject monsterInstance;

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
        if (gameManager.GetComponent<GameManager>().interviewing)
        {
			monsterInstance = gameManager.GetComponent<GameManager>().monsterInstance; //takes the gameManagers monsterInstance for questioning
        }
        else
        {
            monsterInstance = null;
        }
    }

    public void Question1Response()
    {
        interviewQuestions.SetActive(false);
        interviewResponse.SetActive(true);
		responseText.text = "I've only worked in a few dungeons throughout my career."; //"I have " + monsterInstance.GetComponent<MonsterScript>().startingHealth + " health.";
    }

    public void Question2Response()
    {
        interviewQuestions.SetActive(false);
        interviewResponse.SetActive(true);
		responseText.text = "I haven't killed any legendary heroes, but I've killed a few knights."; //"My damage is " + monsterInstance.GetComponent<MonsterScript>().attackDamage;
    }

    public void Question3Response()
    {
        interviewQuestions.SetActive(false);
        interviewResponse.SetActive(true);
		responseText.text = "I hate fire! Fire can do some serious damage to me!"; //+ monsterInstance.GetComponent<MonsterScript>().trait1 + " and " + monsterInstance.GetComponent<MonsterScript>().trait2;
    }

	public void Question4Response()
	{
		interviewQuestions.SetActive(false);
		interviewResponse.SetActive(true);
		responseText.text = "I enjoy walking around town and killing humans.";
	}

	public void Question5Response()
	{
		interviewQuestions.SetActive(false);
		interviewResponse.SetActive(true);
		responseText.text = "I drink a lot of milk! It's pretty good stuff.";
	}

    public void InterviewResponse()
    {
        interviewResponse.SetActive(false);
        interviewQuestions.SetActive(true);
    }

    public void ExitInterview() //exits interview and hides the interview UI
    {
		gameManager.GetComponent<GameManager>().interviewButtons.SetActive(false);
		gameManager.GetComponent<GameManager>().interviewImage.SetActive(false);
		gameManager.GetComponent<GameManager>().interviewBackground.SetActive(false);
		gameManager.GetComponent<GameManager>().interviewExit.SetActive(false);
		gameManager.GetComponent<GameManager>().constructionButton.SetActive(true);
		gameManager.GetComponent<GameManager>().applicationsButton.SetActive(true);

		/*if (gameManager.GetComponent<GameManager>().resume != null)
		{
			gameManager.GetComponent<GameManager>().resume.SetActive(true);
		}
        gameManager.GetComponent<GameManager>().interviewing = false;*/
    }

}
