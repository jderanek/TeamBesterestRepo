using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

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
