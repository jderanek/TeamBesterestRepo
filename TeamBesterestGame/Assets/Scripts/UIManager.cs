﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Yarn.Unity;

public class UIManager : MonoBehaviour {

    public GameManager gameManager;

    //UI stuff
    public Canvas canvas;
    public Font arial;

    public GameObject[] menus;
    //public GameObject sideBar;

    public GameObject confirmationBox;

    private bool pauseMenuOpen = false;
    public static bool eligibleForClick = true;

    // public Text currencyText; //public to assign reference in editor

    public GameObject hourSwivel;
    //public EventSystem eventSystem;

    public GameObject speaker = null;

    public float slowTextSpeed;
    public Text slowText;
    public float mediumTextSpeed;
    public Text mediumText;
    public float fastTextSpeed;
    public Text fastText;

    public InterviewUI[] monsters;

    public GameObject notification;

    //List<InterviewUI> monsters = new List<InterviewUI>();

    public GameObject journalPrompt;
    public GameObject clockPrompt;

    private void Start()
    {
        monsters = FindObjectsOfType<InterviewUI>();
    }

    public void SetEligibleForClick(bool c)
    {
        eligibleForClick = c;
    }
    
    public void ToggleMenusOff()
    {
        GameManager.paused = false;
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }
    }

    public void OpenMenu(string menu)
    {
        ToggleMenusOff();
        GameManager.paused = true;
        AkSoundEngine.PostEvent("Pause", gameObject);
        switch (menu)
        {
            case "Main":
                menus[0].SetActive(true);
                break;
            case "Options":
                menus[1].SetActive(true);
                menus[2].SetActive(true);
                break;
            case "Gameplay":
                menus[1].SetActive(true);
                menus[3].SetActive(true);
                break;
            case "Video":
                menus[1].SetActive(true);
                menus[4].SetActive(true);
                break;
            case "Audio":
                menus[1].SetActive(true);
                menus[5].SetActive(true);
                break;
            case "Exit":
                menus[6].SetActive(true);
                break;
            default:
                break;

        }
    }

    public void UnpauseWwise()
    {
        AkSoundEngine.PostEvent("Resume", gameObject);
    }

    public void SetTextSpeed(int textSpeed)
    {
        switch(textSpeed)
        {
            case 1: //slow
                print("beep");
                slowText.fontStyle = FontStyle.Bold;
                mediumText.fontStyle = FontStyle.Normal;
                fastText.fontStyle = FontStyle.Normal;
                foreach (InterviewUI monster in monsters)
                {
                    monster.textSpeed = slowTextSpeed;
                }
                break;
            case 2: //medium
                mediumText.fontStyle = FontStyle.Bold;
                slowText.fontStyle = FontStyle.Normal;
                fastText.fontStyle = FontStyle.Normal;
                foreach (InterviewUI monster in monsters)
                {
                    monster.textSpeed = mediumTextSpeed;
                }
                break;
            case 3: //fast
                fastText.fontStyle = FontStyle.Bold;
                slowText.fontStyle = FontStyle.Normal;
                mediumText.fontStyle = FontStyle.Normal;
                foreach (InterviewUI monster in monsters)
                {
                    monster.textSpeed = fastTextSpeed;
                }
                break;
        }
    }

    public void ToggleNotification()
    {
        print("sound should be called");
        if (!notification.activeInHierarchy)
        {
            AkSoundEngine.PostEvent("Pencil_Scratch", notification);
        }
        notification.SetActive(!notification.activeInHierarchy);
    }

    [YarnCommand ("TogglePrompts")]
    public void TogglePrompts(string prompt)
    {
        switch(prompt)
        {
            case "1":
                GameManager.paused = false;
                UnpauseWwise();
                journalPrompt.SetActive(false);
                break;
            case "2":
                GameManager.paused = false;
                UnpauseWwise();
                clockPrompt.SetActive(false);
                break;
            case "3":
                //AkSoundEngine.PostEvent("Pause", gameObject);
                GameManager.paused = true;
                notification.SetActive(false);
                journalPrompt.SetActive(true);
                break;
            case "4":
                //AkSoundEngine.PostEvent("Pause", gameObject);
                GameManager.paused = true;
                clockPrompt.SetActive(true);
                break;
        }
    }
}
