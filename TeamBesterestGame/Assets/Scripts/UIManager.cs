using System.Collections;
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
    
    public void ToggleMenusOff()
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }
    }

    public void OpenMenu(string menu)
    {
        switch (menu)
        {
            case "Main":
                ToggleMenusOff();
                menus[0].SetActive(true);
                break;
            case "Options":
                ToggleMenusOff();
                menus[1].SetActive(true);
                menus[2].SetActive(true);
                break;
            case "Gameplay":
                ToggleMenusOff();
                menus[1].SetActive(true);
                menus[3].SetActive(true);
                break;
            case "Video":
                ToggleMenusOff();
                menus[1].SetActive(true);
                menus[4].SetActive(true);
                break;
            case "Audio":
                ToggleMenusOff();
                menus[1].SetActive(true);
                menus[5].SetActive(true);
                break;
            default:
                ToggleMenusOff();
                break;

        }
    }

    public void SetTextSpeed(int textSpeed)
    {
        switch(textSpeed)
        {
            case 1: //slow
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
                journalPrompt.SetActive(false);
                break;
            case "2":
                clockPrompt.SetActive(false);
                break;
            case "3":
                journalPrompt.SetActive(true);
                break;
            case "4":
                clockPrompt.SetActive(true);
                break;
        }
    }
}
