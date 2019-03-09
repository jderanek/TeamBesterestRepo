using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using System.IO;

public class GameManager : MonoBehaviour
{
    #region Declarations
    UIManager uiManager;

    public List<GameObject> monsterList = new List<GameObject>();

    [HideInInspector]
    public bool interviewing = false; //public to be accessed in interview script

    public GameObject combatCanvas;

    /*
    public GameObject goblennPortrait;
    public GameObject geoffPortrait;
    public GameObject jeffPortrait;
    public GameObject gabbinPortrait;
    public GameObject nilbogPortrait;
    */

    public GameObject shiftOnePortraits;
    public GameObject shiftTwoPortraits;
    public GameObject shiftThreePortraits;

    public GameObject combatManager;

    //Current phase of the game, as well as the enemies to spawn next combat
    public Button phaseButton;
    string phase = "Interview";
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

    public Text nameTag;
    public GameObject responseBubble;
    public Image currentSpeakerPortrait;
    public Sprite goblennSprite;
    public Sprite gabbinSprite;
    public Sprite nilbogSprite;
    public Sprite geoffSprite;
    public Sprite jeffSprite;
    public GameObject goblennInterviewPortrait;
    public GameObject gabbinInterviewPortrait;
    public GameObject nilbogInterviewPortrait;
    public GameObject geoffInterviewPortrait;
    public GameObject jeffInterviewPortrait;

    private DialogueRunner[] dialogueRunners;
    private InterviewVariableStorage[] storages;

    void Awake()
    {
        phaseButton.GetComponentInChildren<Text>().text = "Start";
        uiManager = this.GetComponent<UIManager>();

        dialogueRunners = FindObjectsOfType<DialogueRunner>();
        storages = FindObjectsOfType<InterviewVariableStorage>();
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

                this.DisableFollowup("shift1");
                this.DisableFollowup("shift2");
                this.DisableFollowup("shift3");

                //EndInterview();
                //this.SetGoblinPoints();
                this.SetCombatScore();
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
            }
            else //third shift
            {
                shiftOnePortraits.SetActive(false);
                shiftTwoPortraits.SetActive(false);
                shiftThreePortraits.SetActive(true);
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

    //set the combat manager's yarn variables according to each shifts points
    public void SetCombatScore()
    {
        foreach (GameObject monster in monsterList)
        {
            if (monster.GetComponent<BaseMonster>().monName == "Goblenn")
            {
                var varToSet = monster.GetComponent<BaseMonster>().dialogueRunner.GetComponent<InterviewVariableStorage>().GetValue("$shift11Combat");
                combatManager.GetComponent<InterviewVariableStorage>().SetValue("$shift11Combat", varToSet);
            }
            if (monster.GetComponent<BaseMonster>().monName == "Jeff" || monster.GetComponent<BaseMonster>().monName == "Geoff")
            {
                var varToSet = monster.GetComponent<BaseMonster>().dialogueRunner.GetComponent<InterviewVariableStorage>().GetValue("$shift12Combat");
                combatManager.GetComponent<InterviewVariableStorage>().SetValue("$shift12Combat", varToSet);
            }
            if (monster.GetComponent<BaseMonster>().monName == "Nilbog" || monster.GetComponent<BaseMonster>().monName == "Gabbin")
            {
                var varToSet = monster.GetComponent<BaseMonster>().dialogueRunner.GetComponent<InterviewVariableStorage>().GetValue("$shift13Combat");
                combatManager.GetComponent<InterviewVariableStorage>().SetValue("$shift13Combat", varToSet);
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
        this.interviewing = false;
        foreach (GameObject monster in monsterList)
        {
            monster.GetComponent<BaseMonster>().Reset();
        }
        foreach (GameObject monster in monsterList)
        {
            monster.GetComponent<BaseMonster>().dialogueRunner.GetComponent<DialogueRunner>().Stop();
            foreach (GameObject option in monster.GetComponent<BaseMonster>().interviewOptions)
            {
                option.SetActive(false);
            }
        }
        
    }

    public void TutorialReset()
    {
        this.interviewing = false;
        foreach (GameObject monster in monsterList)
        {
            monster.GetComponent<BaseMonster>().interviewable = true;
        }
        foreach (GameObject monster in monsterList)
        {
            monster.GetComponent<BaseMonster>().dialogueRunner.GetComponent<DialogueRunner>().Stop();
        }
        foreach (GameObject monster in monsterList)
        {
            monster.GetComponent<BaseMonster>().dialogueRunner.GetComponent<DialogueRunner>().StartDialogue();
        }

    }

    public void EnableFollowup(string shift) //to be called in EndInterview, unlocks followup
    {
        switch (shift)
        {
            case "shift1": //Goblenn
                if (!this.monsterList[3].GetComponent<BaseMonster>().interviewable)
                {
                    this.monsterList[3].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(true);
                    this.monsterList[3].GetComponent<BaseMonster>().button.gameObject.SetActive(false);
                }
                break;
            case "shift2": //Jeff and Geoff
                if (!this.monsterList[0].GetComponent<BaseMonster>().interviewable && !this.monsterList[1].GetComponent<BaseMonster>().interviewable)
                {
                    this.monsterList[0].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(true);
                    this.monsterList[0].GetComponent<BaseMonster>().button.gameObject.SetActive(false);
                    this.monsterList[1].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(true);
                    this.monsterList[1].GetComponent<BaseMonster>().button.gameObject.SetActive(false);
                }
                break;
            case "shift3": //Nilbog and Gabbin
                if (!this.monsterList[2].GetComponent<BaseMonster>().interviewable && !this.monsterList[4].GetComponent<BaseMonster>().interviewable)
                {
                    this.monsterList[2].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(true);
                    this.monsterList[2].GetComponent<BaseMonster>().button.gameObject.SetActive(false);
                    this.monsterList[4].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(true);
                    this.monsterList[4].GetComponent<BaseMonster>().button.gameObject.SetActive(false);
                }
                break;
        }
    }

    public void DisableFollowup(string shift) //to be called at end of followup conversation
    {
        switch (shift)
        {
            case "shift1": //Goblenn
                this.monsterList[3].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(false);
                this.monsterList[3].GetComponent<BaseMonster>().button.gameObject.SetActive(true);
                break;
            case "shift2": //Jeff and Geoff
                this.monsterList[0].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(false);
                this.monsterList[0].GetComponent<BaseMonster>().button.gameObject.SetActive(true);
                this.monsterList[1].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(false);
                this.monsterList[1].GetComponent<BaseMonster>().button.gameObject.SetActive(true);
                break;
            case "shift3": //Nilbog and Gabbin
                this.monsterList[2].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(false);
                this.monsterList[2].GetComponent<BaseMonster>().button.gameObject.SetActive(true);
                this.monsterList[4].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(false);
                this.monsterList[4].GetComponent<BaseMonster>().button.gameObject.SetActive(true);
                break;
        }
    }

    public void LoadLevelOne()
    {
        //AkSoundEngine.StopAll();
        print("loading level 1");
        SceneManager.LoadScene("Scene2");
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
        if (Input.GetKeyDown(KeyCode.J))
        {
            Save();
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            Load();
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

    //Loads or saves all variable storages
    public void Load()
    {
        foreach (InterviewVariableStorage storage in storages)
            storage.LoadData();
        Debug.Log("Loaded");
    }

    public void Save()
    {
        foreach (InterviewVariableStorage storage in storages)
            storage.SaveData();
        PlayerPrefs.Save();
        Debug.Log("Saved");
    }

    public void DeleteSave()
    {
        foreach (InterviewVariableStorage storage in storages)
            storage.DeleteData();
        PlayerPrefs.Save();
        Debug.Log("Save Deleted");
    }

    public void SetCurrentSpeaker(int speaker)
    {
        switch(speaker)
        {
            case 0: //No current speaker
                goblennInterviewPortrait.SetActive(true);
                gabbinInterviewPortrait.SetActive(true);
                nilbogInterviewPortrait.SetActive(true);
                geoffInterviewPortrait.SetActive(true);
                jeffInterviewPortrait.SetActive(true);
                currentSpeakerPortrait.gameObject.SetActive(false);
                responseBubble.SetActive(false);
                break;
            case 1: //Goblenn
                goblennInterviewPortrait.SetActive(false);
                gabbinInterviewPortrait.SetActive(false);
                nilbogInterviewPortrait.SetActive(false);
                geoffInterviewPortrait.SetActive(false);
                jeffInterviewPortrait.SetActive(false);
                currentSpeakerPortrait.gameObject.SetActive(true);
                currentSpeakerPortrait.sprite = goblennSprite;
                responseBubble.SetActive(true);
                nameTag.text = "Goblenn";
                break;
            case 2: //Gabbin
                goblennInterviewPortrait.SetActive(false);
                gabbinInterviewPortrait.SetActive(false);
                nilbogInterviewPortrait.SetActive(false);
                geoffInterviewPortrait.SetActive(false);
                jeffInterviewPortrait.SetActive(false);
                currentSpeakerPortrait.gameObject.SetActive(true);
                currentSpeakerPortrait.sprite = gabbinSprite;
                responseBubble.SetActive(true);
                nameTag.text = "Gabbin";
                break;
            case 3: //Nilbog
                goblennInterviewPortrait.SetActive(false);
                gabbinInterviewPortrait.SetActive(false);
                nilbogInterviewPortrait.SetActive(false);
                geoffInterviewPortrait.SetActive(false);
                jeffInterviewPortrait.SetActive(false);
                currentSpeakerPortrait.gameObject.SetActive(true);
                currentSpeakerPortrait.sprite = nilbogSprite;
                responseBubble.SetActive(true);
                nameTag.text = "Nilbog";
                break;
            case 4: //Geoff
                goblennInterviewPortrait.SetActive(false);
                gabbinInterviewPortrait.SetActive(false);
                nilbogInterviewPortrait.SetActive(false);
                geoffInterviewPortrait.SetActive(false);
                jeffInterviewPortrait.SetActive(false);
                currentSpeakerPortrait.gameObject.SetActive(true);
                currentSpeakerPortrait.sprite = geoffSprite;
                responseBubble.SetActive(true);
                nameTag.text = "Geoff";
                break;
            case 5: //Jeff
                goblennInterviewPortrait.SetActive(false);
                gabbinInterviewPortrait.SetActive(false);
                nilbogInterviewPortrait.SetActive(false);
                geoffInterviewPortrait.SetActive(false);
                jeffInterviewPortrait.SetActive(false);
                currentSpeakerPortrait.gameObject.SetActive(true);
                currentSpeakerPortrait.sprite = jeffSprite;
                responseBubble.SetActive(true);
                nameTag.text = "Jeff";
                break;
        }
    }
}
