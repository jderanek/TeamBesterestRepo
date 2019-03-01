using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public abstract class BaseMonster : BaseEntity {
    public List<GameObject> department;

    //Private variables for this monsters stats
    //Some made TEMPORARILY public for test purposes
    public string monName;
    int mood = 3;
    Text damageText;
    GameManager gameManager;
    UIManager uiManager;
    int breakdowns = 3;

    public int cNum;//made to cut corners temporarily get rid of this later

    //public Animator anim; //= this.gameObject.GetComponentInChildren<Animator>();

    public bool interviewable = true;
    public GameObject[] interviewOptions;
    public GameObject response;
    public int points = 0;
    public GameObject dialogueRunner;
    public Image picture;

    //bool to check if monster is fleeing
    private bool isFleeing = false;

    public GameObject notes;
    public GameObject newNote;

    void Awake() {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        uiManager = gameManager.GetComponent<UIManager>();
        
        //moved damage text here bc it was throwing error on pre-instantiated monsters
        damageText = this.gameObject.GetComponentInChildren<Text>();
        //anim = this.gameObject.GetComponentInChildren<Animator>();
    }

    ///<summary>
    ///Assigns all stats to this monster, to be used in place of super.
    /// Defaults stress and morale, as well as gain, loss and infamy
    ///</summary>
    /// <param name="nm">Name of Monster</param>
    /// <param name="hp">Total Health</param>
    /// <param name="dam">Base Damage</param>
    /// <param name="trait">Personality Trait</param>
    /// <param name="sal">Base Salary</param>
    /// <param name="thr">Threat</param>
    /// <param name="arm">Base Armor</param>
    /// <param name="ethic">Work Ethic</param>
    /// <param name="sz">Monster Size</param>
    public void AssignStats(string nm, int hp, int dam, int sal, int thr, int arm, int ethic, int sz) {
        this.monName = nm;
    }
    
    public int GetMood()
    {
        return mood;
    }
    public void SetMood(int newMood)
    {
        mood = newMood;
    }
    public string getName() {
        return this.monName;
    }
    public void setName(string nm) {
        this.name = nm;
    }
    public int getBreakdowns() {
        return this.breakdowns;
    }
    public void setBreakdowns(int breaks) {
        this.breakdowns = breaks;
    }


    //Returns this monster to life and to the room they were in previously
    public void Reset()
    {
        this.mood = 3;
        this.interviewable = true;
        picture.color = new Color(picture.color.r, picture.color.g, picture.color.b, 1.0f);
    }
    
    //Clears list of previous traits, and adds the new trait
    [YarnCommand("SwapTrait")]
    public void SwapTrait(string toSwap) {
    }

    [YarnCommand("addPoint")]
    public void addPoint()
    {
        this.points++;
    }

    //subtracts 1 from mood then checks if mood is <= to 0 if so, end's interview and makes monster uninterviewable till next day
    [YarnCommand("MoodHit")]
    public void MoodHit()
    {
        this.mood -= 1;
        //TODO: add slider for mood on the interview menu
        if (this.mood <= 0)
        {
            StormOut();
        }
    }

    [YarnCommand("StormOut")]
    public void StormOut()
    {
        this.interviewable = false;
        //use helper function to disable monster's dialogue options and grey out portrait
        this.gameManager.strikes += 1;
        if (this.gameManager.strikes >= 3)
        {
            //function that ends the interview phase
            this.gameManager.EndInterview();
        }
        this.EndInterview();
    }

    //ends conversation and makes monster uninterviewable till next day with no penalty
    [YarnCommand("EndInterview")]
    public void EndInterview()
    {
        this.interviewable = false;
        this.gameManager.interviewing = false;
        //use helper function to disable monster's dialogue options and grey out portrait
        foreach (GameObject option in interviewOptions)
        {
            option.SetActive(false);
        }
        response.SetActive(false);
        this.uiManager.SpeechBubblesOff();
        //grey out portrait
        uiManager.speaker.SetActive(false);
        picture.color = new Color (picture.color.r, picture.color.g, picture.color.b, 0.5f);
    }

    [YarnCommand("EndTutorial")]
    public void EndTutorial()
    {
        this.gameManager.interviewing = false;
        this.gameManager.LoadLevelOne();
    }

    [YarnCommand("ChangeShift")]
    public void ChangeShift()
    {
        this.gameManager.ChangeShift();
    }

    //Starts invoking flee movement, then disables it and the monster
    //Prevents disabling until movement finishes
    public void StartFleeing()
    {
        this.isFleeing = true;
        InvokeRepeating("Flee", 0f, .05f);
        Invoke("StopFleeing", 5f);
    }

    //Stops monster fleeing
    private void StopFleeing()
    {
        CancelInvoke("Flee");
        gameObject.SetActive(false);
    }

    //Moves monster a tiny amount to the left.
    private void Flee()
    {
        Vector3 pos = transform.position;
        pos.x = pos.x - .5f;
        transform.position = pos;
    }

    [YarnCommand("RevealText")]
    public void RevealText(string text)
    {
        GameObject note = Instantiate(newNote, notes.transform.GetChild(0).transform);
        //here goes avery code
        note.GetComponent<Text>().text = text;
    }
}
