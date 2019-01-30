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

    public GameObject emptyField; //public to be assigned in editor
    public GameObject breakRoomHeader; //public to be assigned in editor
    public GameObject prHeader; //public to be assigned in editor
    private int prCapacity = 3;
    public GameObject hrHeader; //public to be assigned in editor
    private int hrCapacity = 3;
    private bool applicationOpen = false;
    private bool monsterOpen = false;
    private bool departmentOpen = false;
    private bool assignmentOpen = false;
    public GameObject applicationField; //public to be assigned in editor
    public GameObject monsterField; //public to be assigned in editor
    public GameObject monsterContextField; //public to be assigned in editor
    public GameObject applicationContextField; //public to be assigned in editor

    public GameObject confirmationBox;

    public GameObject roomMenu; //public to be assigned in editor
    public Button roomMenuConfirm; //public to be assigned in editor
    public Button roomMenuCancel; //public to be assigned in editor
    private bool roomMenuOpen = false;
    private int roomOptionSelected;

    private bool pauseMenuOpen = false;
    public GameObject pauseMenu; //public to be assigned in editor
    public GameObject applicantButton; //public to be assigned in editor
    public GameObject constructionButton; //public to be assigned in editor

    public Image stressImage;

    public Text infamyLevelText; //public to be assigned in editor
    public Text infamyXPText; //public to be assigned in editor

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

    //used to toggle the monster's context field
    public void ToggleContext(GameObject mcf) //mcf - monster context field
    {
        if (!mcf.activeInHierarchy)
        {
            mcf.SetActive(true);
        }
        else
        {
            mcf.SetActive(false);
        }
    }

    //function should be called whenever the monsterList is changed
    public void UpdateMonsters()
    {
        //reset panel
        int childNum = 0;
        //Debug.Log("a");
        foreach (Transform child in menus[1].transform.GetChild(0).transform)
        {
            if (childNum != 0)
            {
                Destroy(child.gameObject);
            }
            childNum++;
        }
    }
    
    //call this when changes are made to the Room Menu
    public void UpdateRoomMenu()
    {

    }

    //aggregate stress calculating
    public void UpdateStressMeter()
    {
        float overallStressValue = 0;
        int monsterCount = 0;

        foreach (GameObject monster in gameManager.monsterList)
        {
            BaseMonster m = monster.GetComponent<BaseMonster>();
            //overallStressValue += m.getStress();
            monsterCount++;
        }

        overallStressValue = overallStressValue / monsterCount;

        if (overallStressValue <= 10f)
        {
            stressImage.color = Color.green;
            //stressImage.color = new Color(135, 255, 0, 255);
        }
        else if (overallStressValue < 20f && overallStressValue > 10f)
        {
            stressImage.color = Color.green;
            //stressImage.color = new Color(135, 230, 0, 255);
        }
        else if (overallStressValue < 30f && overallStressValue > 20f)
        {
            stressImage.color = Color.green;
            //stressImage.color = new Color(135, 205, 0, 255);
        }
        else if (overallStressValue < 40f && overallStressValue > 30f)
        {
            stressImage.color = Color.green;
            //stressImage.color = new Color(135, 190, 0, 255);
        }
        else if (overallStressValue < 50f && overallStressValue > 40f)
        {
            stressImage.color = Color.yellow;
            //stressImage.color = new Color(135, 165, 0, 255);
        }
        else if (overallStressValue < 60f && overallStressValue > 50f)
        {
            stressImage.color = Color.yellow;
            //stressImage.color = new Color(135, 140, 0, 255);
        }
        else if (overallStressValue < 70f && overallStressValue > 60f)
        {
            stressImage.color = Color.yellow;
            //stressImage.color = new Color(135, 115, 0, 255);
        }
        else if (overallStressValue < 80f && overallStressValue > 70f)
        {
            stressImage.color = Color.red;
            //stressImage.color = new Color(135, 90, 0, 255);
        }
        else if (overallStressValue < 90f && overallStressValue > 80f)
        {
            stressImage.color = Color.red;
            //stressImage.color = new Color(135, 65, 0, 255);
        }
        else if (overallStressValue >= 90f)
        {
            stressImage.color = Color.red;
            //stressImage.color = new Color(135, 40, 0, 255);
        }
        else
        {
            stressImage.color = Color.green;
            //stressImage.color = new Color(135, 255, 0, 255);
        }

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
