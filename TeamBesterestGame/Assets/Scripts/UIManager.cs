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
    public GameObject sideBar;

    public GameObject confirmationBox;

    private bool pauseMenuOpen = false;
    public GameObject pauseMenu; //public to be assigned in editor

    public Text currencyText; //public to assign reference in editor

    public GameObject hourSwivel;
    //public EventSystem eventSystem;

    public GameObject[] speechBubbles;

    public GameObject speaker = null;

    public GameObject optionsMenu;
    public Text slowText;
    public Text mediumText;
    public Text fastText;

    //Opens any menu
    public void ToggleMenu(int menuToOpen)
    {
        int i = 0;
        int j = 0;
        foreach (GameObject menu in menus)
        {
            if (menuToOpen == i)
            {
                //sideBar.SetActive(true);
                menu.SetActive(!menu.activeInHierarchy);
                //eventSystem.SetSelectedGameObject(null);
                if (menu.activeInHierarchy == true)
                {
                    j++;
                }
                
            }
            else
            {
                if (j == 0)
                {
                    //HideSideBar();
                    
                }
                menu.SetActive(false);
            }
            i++;
        }
    }
    
    public void ToggleMenusOff()
    {
        foreach (GameObject menu in menus)
        {
            sideBar.SetActive(false);
            menu.SetActive(false);
        }
    }

    public void HideSideBar()
    {
        sideBar.SetActive(false);
    }

    public void ToggleSpeechBubbles(int goblin)
    {
        if (!this.gameManager.GetComponent<GameManager>().interviewing)
        {
            GameObject speaker = null;
            for (int i = 0; i < speechBubbles.Length; i++)
            {
                if (i == goblin)
                {
                    speaker = speechBubbles[i];
                }
                else
                {
                    speechBubbles[i].SetActive(false);
                }
            }
            speaker.SetActive(true);
        }
    }

    public void SpeechBubblesOff()
    {
        foreach (GameObject bubble in speechBubbles)
        {
            bubble.SetActive(false);
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
                InterviewUI.textSpeed = .025f; //load whatever interviewui is ontop of.
                break;
            case 2: //medium
                mediumText.fontStyle = FontStyle.Bold;
                slowText.fontStyle = FontStyle.Normal;
                fastText.fontStyle = FontStyle.Normal;
                break;
            case 3: //fast
                fastText.fontStyle = FontStyle.Bold;
                slowText.fontStyle = FontStyle.Normal;
                mediumText.fontStyle = FontStyle.Normal;
                break;
        }
    }
}
