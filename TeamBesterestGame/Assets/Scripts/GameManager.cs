using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Yarn.Unity;
using System.IO;
using System.Text;

public class GameManager : MonoBehaviour
{
    #region Declarations
    UIManager uiManager;

    public List<GameObject> monsterList = new List<GameObject>();

    [HideInInspector]
    public bool interviewing = false; //public to be accessed in interview script

    public GameObject combatCanvas;

    /*
    public GameObject shiftOnePortraits;
    public GameObject shiftTwoPortraits;
    public GameObject shiftThreePortraits;
    */

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
    public GameObject gordonNotes;

    public Text nameTag;
    public GameObject responseBubble;
    public Image currentSpeakerPortrait;
    public Sprite goblennSprite;
    public Sprite gabbinSprite;
    public Sprite nilbogSprite;
    public Sprite geoffSprite;
    public Sprite jeffSprite;
    public Sprite gordonSprite;
    public GameObject goblennInterviewPortrait;
    public GameObject gabbinInterviewPortrait;
    public GameObject nilbogInterviewPortrait;
    public GameObject geoffInterviewPortrait;
    public GameObject jeffInterviewPortrait;
    public GameObject gordonInterviewPortrait;
    public Clock clock;

    public GameObject responseBox;

    private DialogueRunner[] dialogueRunners;
    private InterviewVariableStorage[] storages;

    void Awake()
    {
        phaseButton.GetComponentInChildren<Text>().text = "Start";
        uiManager = this.GetComponent<UIManager>();

        dialogueRunners = FindObjectsOfType<DialogueRunner>();
        storages = FindObjectsOfType<InterviewVariableStorage>();

        //Loads data if the game is supposed to
        if (GlobalVariables.loading)
            Load();
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
        SoundManager.StopDialogue();
        if (!canSkip)
            return;

        switch (phase)
        {
            case "Start":
                phase = "Combat";
                this.shift = 3;
                //ChangeShift();
                canSkip = true;

                if (interviewing)
                {
                    foreach (GameObject monster in monsterList)
                    {
                        monster.GetComponent<BaseMonster>().EndInterview();
                    }
                }

                this.DisableFollowup("shift1");
                this.DisableFollowup("shift2");
                this.DisableFollowup("shift3");

                this.SetCombatScore();
                interviewing = false;
                interviewCanvas.SetActive(false);
                break;
            case "Combat":
                combatCanvas.SetActive(true);
                combatManager.GetComponent<DialogueRunner>().StartDialogue("Shift11");
                phaseButton.GetComponentInChildren<Text>().text = "Skip Combat";
                phase = "Interview";
                break;
            case "Interview":
                canSkip = true;
                combatManager.GetComponent<DialogueRunner>().Stop();
                combatCanvas.SetActive(false);
                ResetPhase();
                phase = "Transition";
                phaseButton.gameObject.SetActive(false);
                clock.Run();
                Invoke("StartPhase", 1.5f);
                break;
            case "Transition":
                phase = "Start";
                this.interviewsRemaining = 3;
                phaseButton.gameObject.SetActive(true);
                phaseButton.GetComponentInChildren<Text>().text = "Start Combat";
                //phaseButton.gameObject.SetActive(false);
                ToggleInterviewMenu();
                break;
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
        interviewCanvas.SetActive(false);
        //Invoke("EnableInterviewCanvas", 1.4f);
    }

    void EnableInterviewCanvas()
    {
        interviewCanvas.SetActive(true);
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
            case "shift1": //Goblenn and Gordon
                if (!this.monsterList[3].GetComponent<BaseMonster>().interviewable && !this.monsterList[5].GetComponent<BaseMonster>().interviewable)
                {
                    /*this.monsterList[3].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(true);
                    this.monsterList[3].GetComponent<BaseMonster>().button.gameObject.SetActive(false);
                    this.monsterList[5].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(true);
                    this.monsterList[5].GetComponent<BaseMonster>().button.gameObject.SetActive(false);*/
                }
                break;
            case "shift2": //Jeff and Geoff
                if (!this.monsterList[0].GetComponent<BaseMonster>().interviewable && !this.monsterList[1].GetComponent<BaseMonster>().interviewable)
                {
                    this.monsterList[0].GetComponent<BaseMonster>().dialogueRunner.GetComponent<DialogueRunner>().StartDialogue("JeffGeoffSolution1");
                    /*this.monsterList[0].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(true);
                    this.monsterList[0].GetComponent<BaseMonster>().button.gameObject.SetActive(false);
                    this.monsterList[1].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(true);
                    this.monsterList[1].GetComponent<BaseMonster>().button.gameObject.SetActive(false);*/
                }
                break;
            case "shift3": //Nilbog and Gabbin
                if (!this.monsterList[2].GetComponent<BaseMonster>().interviewable && !this.monsterList[4].GetComponent<BaseMonster>().interviewable)
                {
                    this.monsterList[2].GetComponent<BaseMonster>().dialogueRunner.GetComponent<DialogueRunner>().StartDialogue("NilbogGabbinSolution1");
                    /*this.monsterList[2].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(true);
                    this.monsterList[2].GetComponent<BaseMonster>().button.gameObject.SetActive(false);
                    this.monsterList[4].GetComponent<BaseMonster>().followUpButton.gameObject.SetActive(true);
                    this.monsterList[4].GetComponent<BaseMonster>().button.gameObject.SetActive(false);*/
                }
                break;
        }
    }

    public void DisableFollowup(string shift) //to be called at end of followup conversation
    {
        /*switch (shift)
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
        }*/
    }

    public void LoadLevelOne()
    {
        //AkSoundEngine.StopAll();
        print("loading level 1");
        AkSoundEngine.StopAll();
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
        AkSoundEngine.PostEvent("Page_Flip", gameObject);

        switch(tab)
        {
            case 1:
                goblennNotes.SetActive(true);
                nilbogNotes.SetActive(false);
                geoffNotes.SetActive(false);
                jeffNotes.SetActive(false);
                gabbinNotes.SetActive(false);
                gordonNotes.SetActive(false);
                break;
            case 2:
                goblennNotes.SetActive(false);
                nilbogNotes.SetActive(true);
                geoffNotes.SetActive(false);
                jeffNotes.SetActive(false);
                gabbinNotes.SetActive(false);
                gordonNotes.SetActive(false);
                break;
            case 3:
                goblennNotes.SetActive(false);
                nilbogNotes.SetActive(false);
                geoffNotes.SetActive(true);
                jeffNotes.SetActive(false);
                gabbinNotes.SetActive(false);
                gordonNotes.SetActive(false);
                break;
            case 4:
                goblennNotes.SetActive(false);
                nilbogNotes.SetActive(false);
                geoffNotes.SetActive(false);
                jeffNotes.SetActive(true);
                gabbinNotes.SetActive(false);
                gordonNotes.SetActive(false);
                break;
            case 5:
                goblennNotes.SetActive(false);
                nilbogNotes.SetActive(false);
                geoffNotes.SetActive(false);
                jeffNotes.SetActive(false);
                gabbinNotes.SetActive(true);
                gordonNotes.SetActive(false);
                break;
            case 6:
                goblennNotes.SetActive(false);
                nilbogNotes.SetActive(false);
                geoffNotes.SetActive(false);
                jeffNotes.SetActive(false);
                gabbinNotes.SetActive(false);
                gordonNotes.SetActive(true);
                break;
        }
    }

    [YarnCommand("CombatOff")]
    public void CombatOff()
    {
        this.combatCanvas.SetActive(false);
    }

    public void ToggleNotebook()
    {
        AkSoundEngine.PostEvent("Notebook_Open", gameObject);
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
        PlayerPrefs.SetString("scene", SceneManager.GetActiveScene().name);
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
                gordonInterviewPortrait.gameObject.SetActive(true);
                currentSpeakerPortrait.gameObject.SetActive(false);
                responseBubble.SetActive(false);
                break;
            case 1: //Goblenn
                goblennInterviewPortrait.SetActive(false);
                gabbinInterviewPortrait.SetActive(false);
                nilbogInterviewPortrait.SetActive(false);
                geoffInterviewPortrait.SetActive(false);
                jeffInterviewPortrait.SetActive(false);
                gordonInterviewPortrait.gameObject.SetActive(false);
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
                gordonInterviewPortrait.gameObject.SetActive(false);
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
                gordonInterviewPortrait.gameObject.SetActive(false);
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
                gordonInterviewPortrait.gameObject.SetActive(false);
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
                gordonInterviewPortrait.gameObject.SetActive(false);
                currentSpeakerPortrait.gameObject.SetActive(true);
                currentSpeakerPortrait.sprite = jeffSprite;
                responseBubble.SetActive(true);
                nameTag.text = "Gorgo";
                break;
            case 6: //Gordon
                goblennInterviewPortrait.SetActive(false);
                gabbinInterviewPortrait.SetActive(false);
                nilbogInterviewPortrait.SetActive(false);
                geoffInterviewPortrait.SetActive(false);
                jeffInterviewPortrait.SetActive(false);
                gordonInterviewPortrait.gameObject.SetActive(false);
                currentSpeakerPortrait.gameObject.SetActive(true);
                currentSpeakerPortrait.sprite = gordonSprite;
                responseBubble.SetActive(true);
                nameTag.text = "Gordon";
                break;
        }
    }
}

