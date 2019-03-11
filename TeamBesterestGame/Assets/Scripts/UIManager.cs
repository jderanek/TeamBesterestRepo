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
    public GameObject pauseMenu; //public to be assigned in editor

   // public Text currencyText; //public to assign reference in editor

    public GameObject hourSwivel;
    //public EventSystem eventSystem;

    public GameObject[] speechBubbles;

    public GameObject speaker = null;

    public GameObject optionsMenu;
    public float slowTextSpeed;
    public Text slowText;
    public float mediumTextSpeed;
    public Text mediumText;
    public float fastTextSpeed;
    public Text fastText;

    public InterviewUI[] monsters;

    public GameObject notification;

    //List<InterviewUI> monsters = new List<InterviewUI>();

    private void Start()
    {
        monsters = FindObjectsOfType<InterviewUI>();
    }

    //Opens any menu
    public void ToggleMenu(int menuToOpen)
    {
        menus[menuToOpen].SetActive(!menus[menuToOpen].activeInHierarchy);
    }
    
    public void ToggleMenusOff()
    {
        foreach (GameObject menu in menus)
        {
            menu.SetActive(false);
        }
    }

    public void ToggleOptionsMenu()
    {
        optionsMenu.SetActive(!optionsMenu.gameObject.activeInHierarchy);
        pauseMenu.SetActive(!pauseMenu.gameObject.activeInHierarchy);
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
}
