using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class Manager : MonoBehaviour
{
    #region Declarations
    UIManager uiManager;
    
    public Text pauseButtonText; //public to be assigned in editor
    
    [HideInInspector]
    public bool interviewing = false; //public to be accessed in interview script

    //Day counter to increase week
    private int days = 0;

    //Current phase of the game, as well as the enemies to spawn next combat
    public Button phaseButton;
    string phase = "Start";
    int enemiesToSpawn = 3;
    bool canSkip = true;
    public readonly int maxInterviews = 3;
    public int interviewsRemaining;

    public GameObject dialogueRunner;
    public GameObject interviewCanvas;
    public GameObject interviewResponse;
    public GameObject interviewHireButton;
    public GameObject hellhoundImage;
    public GameObject goblinImage;

    public GameObject trainingMenu;
    public GameObject trainingMenuSlot;
    public int actionsRemaining;

    public GameObject interviewMenu;
    public GameObject interviewMenuSlot;

    //
    public int strikes = 0;

    #endregion

    #region Initialization
    // Use this for initialization
    void Awake()
    {
        phaseButton.GetComponentInChildren<Text>().text = "Start";
        uiManager = this.GetComponent<UIManager>();
    }

	void Start() {
    }
    #endregion

    public void ToggleInterviewMenu()
    {
        if (interviewing)
        {
            interviewCanvas.SetActive(!interviewCanvas.activeSelf);
        }
        else
        {
            interviewCanvas.SetActive(!interviewCanvas.activeSelf);
        }
    }
    
    //enables interview UI and hides other UI elements that are in the way
    public void Interview(GameObject monster)
    {
        if (interviewsRemaining > 0)
        {
            interviewing = true;

            interviewCanvas.SetActive(true);
            interviewResponse.SetActive(true);
            
            interviewsRemaining--;
        }
        else
        {
            phaseButton.gameObject.SetActive(true);
            interviewMenu.SetActive(!interviewMenu.activeSelf);
        }
    }

    public void EndInterview()
    {
        this.gameObject.GetComponentInChildren<InterviewManager>().ExitInterview();
    }

    //Starts the next phase when the button is clicked.
    //Only works when canSkip is true
    public void StartPhase()
    {
        if (!canSkip)
            return;

        switch (phase)
        {
            case "Start":
                canSkip = false;
                phase = "Combat";
                enemiesToSpawn = 3;
                phaseButton.GetComponentInChildren<Text>().text = "Combat In Progress";
                break;
            case "Combat":
                phase = "Interview";
                ResetPhase();
                phaseButton.GetComponentInChildren<Text>().text = "Begin Interviews";
                break;
            case "Interview":
                canSkip = true;
                phase = "Start";
                this.interviewsRemaining = 3;
                phaseButton.GetComponentInChildren<Text>().text = "Start Combat";
                ToggleInterviewMenu();
                break;
        }
    }

    //Resets the dungeon to the original state, but keeps monster changes
    public void ResetPhase()
    {
        strikes = 0;
    }
    
    public void ExitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitForClick()
    {
        while(!Input.GetMouseButtonDown(1))
        {
            yield return null;
        }
        trainingMenu.SetActive(true);
        if (actionsRemaining == 0)
        {
            StartCoroutine(CloseMenuButSlowly());
            phaseButton.gameObject.SetActive(true);
        }
    }

    IEnumerator CloseMenuButSlowly()
    {
        yield return new WaitForSeconds(1.5f);
        trainingMenu.SetActive(false);
    }

    //Debug controls. Disable for full builds
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ToggleInterviewMenu();
        }
    }
}
