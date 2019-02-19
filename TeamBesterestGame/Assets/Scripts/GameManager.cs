using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class GameManager : MonoBehaviour
{
    #region Declarations
    UIManager uiManager;

    public List<GameObject> monsterList = new List<GameObject>();

    [HideInInspector]
    public bool interviewing = false; //public to be accessed in interview script

    public GameObject combatCanvas;

    public GameObject goblennPortrait;
    public GameObject geoffPortrait;
    public GameObject jeffPortrait;
    public GameObject gabbinPortrait;
    public GameObject nilbogPortrait;

    public GameObject shiftOnePortraits;
    public GameObject shiftTwoPortraits;
    public GameObject shiftThreePortraits;

    public GameObject combatManager;

    //Current phase of the game, as well as the enemies to spawn next combat
    public Button phaseButton;
    string phase = "Start";
    bool canSkip = true;
    public readonly int maxInterviews = 3;
    public int interviewsRemaining;
    public GameObject dialogueRunner;

    public GameObject interviewCanvas;
    public GameObject interviewResponse;
    public GameObject interviewHireButton;

    public GameObject interviewMenu;

    public int strikes = 0;
    public int shift = 1;
    int finalScore = 0;

    #endregion

    public GameObject notebook;
    public GameObject goblennNotes;
    public GameObject nilbogNotes;
    public GameObject geoffNotes;
    public GameObject jeffNotes;
    public GameObject gabbinNotes;

    void Awake()
    {
        phaseButton.GetComponentInChildren<Text>().text = "Start";
        uiManager = this.GetComponent<UIManager>();

        //Debug.Log("Start Tests:");
        //Debug.Log(Scissors.CamelToSentence("ThisStringShouldTurnIntoAProperSentence"));
        //Debug.Log(Scissors.CamelToSentence("IfThereAreMultipleSentences.ThenTheWordFollowingThePeriodShouldBeCapitilized."));
    }

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
                this.shift = 3;
                ChangeShift();
                canSkip = true;
                phase = "Interview";

                foreach (GameObject monster in monsterList)
                {
                    monster.GetComponent<BaseMonster>().EndInterview();
                }

                //EndInterview();
                this.SetGoblinPoints();
                interviewing = false;
                interviewCanvas.SetActive(false);
                combatCanvas.SetActive(true);
                combatManager.GetComponent<DialogueRunner>().StartDialogue("Shift11");
                phaseButton.GetComponentInChildren<Text>().text = "Skip Combat";
                break;
            case "Interview":
                canSkip = true;
                combatManager.GetComponent<DialogueRunner>().Stop();
                combatCanvas.SetActive(false);
                ResetPhase();
                phase = "Start";
                this.interviewsRemaining = 3;
                phaseButton.GetComponentInChildren<Text>().text = "Start Combat";
                //phaseButton.gameObject.SetActive(false);
                ToggleInterviewMenu();
                break;
        }
    }

    [YarnCommand("ChangeShift")]
    public void ChangeShift()
    {
        if (this.shift < 3)
        {
            shift++;
            if (shift == 2) //second shift, enable second shift portraits (first is on by default)
            {
                shiftOnePortraits.SetActive(false);
                shiftTwoPortraits.SetActive(true);
                shiftThreePortraits.SetActive(false);
                /*goblennPortrait.SetActive(false);
                gabbinPortrait.SetActive(false);
                nilbogPortrait.SetActive(false);
                jeffPortrait.SetActive(true);
                geoffPortrait.SetActive(true);*/
            }
            else //third shift
            {
                shiftOnePortraits.SetActive(false);
                shiftTwoPortraits.SetActive(false);
                shiftThreePortraits.SetActive(true);
                /*goblennPortrait.SetActive(false);
                gabbinPortrait.SetActive(true);
                nilbogPortrait.SetActive(true);
                jeffPortrait.SetActive(false);
                geoffPortrait.SetActive(false);*/
            }
        }
        else //resets shift to 1
        {
            shiftOnePortraits.SetActive(true);
            shiftTwoPortraits.SetActive(false);
            shiftThreePortraits.SetActive(false);
            canSkip = true;
            combatCanvas.SetActive(false);
            shift = 1;
            /*goblennPortrait.SetActive(true);
            gabbinPortrait.SetActive(false);
            nilbogPortrait.SetActive(false);
            jeffPortrait.SetActive(false);
            geoffPortrait.SetActive(false);*/
            this.interviewing = false;
        }
    }

    public void SetGoblinPoints() //set the yarn variables according to each goblin's int point values
    {
        foreach (GameObject monster in monsterList)
        {
            if (monster.GetComponent<BaseMonster>().monName == "Goblenn")
            {
                var varToSet = new Yarn.Value((float)monster.GetComponent<BaseMonster>().points);
                combatManager.GetComponent<InterviewVariableStorage>().SetValue("$goblennPoints", varToSet);
            }
            if (monster.GetComponent<BaseMonster>().monName == "Geoff")
            {
                var varToSet = new Yarn.Value((float)monster.GetComponent<BaseMonster>().points);
                combatManager.GetComponent<InterviewVariableStorage>().SetValue("$geoffPoints", varToSet);
            }
            if (monster.GetComponent<BaseMonster>().monName == "Jeff")
            {
                var varToSet = new Yarn.Value((float)monster.GetComponent<BaseMonster>().points);
                combatManager.GetComponent<InterviewVariableStorage>().SetValue("$jeffPoints", varToSet);
            }
            if (monster.GetComponent<BaseMonster>().monName == "Gabbin")
            {
                var varToSet = new Yarn.Value((float)monster.GetComponent<BaseMonster>().points);
                combatManager.GetComponent<InterviewVariableStorage>().SetValue("$gabbinPoints", varToSet);
            }
            if (monster.GetComponent<BaseMonster>().monName == "Nilbog")
            {
                var varToSet = new Yarn.Value((float)monster.GetComponent<BaseMonster>().points);
                combatManager.GetComponent<InterviewVariableStorage>().SetValue("$nilbogPoints", varToSet);
            }
        }
    }

    public void FinalScore() //accumulates all the monsters' points for the final score
    {
        foreach (GameObject monster in monsterList)
        {
            finalScore += monster.GetComponent<BaseMonster>().points;
        }
    }

    //Resets the dungeon to the original state, but keeps monster changes
    public void ResetPhase()
    {
        strikes = 0;

        foreach (GameObject monster in monsterList)
        {
            monster.GetComponent<BaseMonster>().Reset();
        }
        foreach (GameObject monster in monsterList)
        {
            monster.GetComponent<BaseMonster>().dialogueRunner.GetComponent<DialogueRunner>().Stop();
        }
        
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    //Debug controls. Disable for full builds
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            ToggleInterviewMenu();
        }
    }

    public void ChangeTabs(int tab)
    {
        switch(tab)
        {
            case 1:
                goblennNotes.SetActive(true);
                nilbogNotes.SetActive(false);
                geoffNotes.SetActive(false);
                jeffNotes.SetActive(false);
                gabbinNotes.SetActive(false);
                break;
            case 2:
                goblennNotes.SetActive(false);
                nilbogNotes.SetActive(true);
                geoffNotes.SetActive(false);
                jeffNotes.SetActive(false);
                gabbinNotes.SetActive(false);
                break;
            case 3:
                goblennNotes.SetActive(false);
                nilbogNotes.SetActive(false);
                geoffNotes.SetActive(true);
                jeffNotes.SetActive(false);
                gabbinNotes.SetActive(false);
                break;
            case 4:
                goblennNotes.SetActive(false);
                nilbogNotes.SetActive(false);
                geoffNotes.SetActive(false);
                jeffNotes.SetActive(true);
                gabbinNotes.SetActive(false);
                break;
            case 5:
                goblennNotes.SetActive(false);
                nilbogNotes.SetActive(false);
                geoffNotes.SetActive(false);
                jeffNotes.SetActive(false);
                gabbinNotes.SetActive(true);
                break;
        }
    }

    public void ToggleNotebook()
    {
        notebook.SetActive(!notebook.activeInHierarchy);
    }
}
