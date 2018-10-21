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

    //Opens any menu
    public void ToggleMenu(int menuToOpen)
    {
        int i = 0;
        int j = 0;
        foreach (GameObject menu in menus)
        {
            if (menuToOpen == i)
            {
                sideBar.SetActive(true);
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
                    HideSideBar();
                    
                }
                menu.SetActive(false);
            }
            i++;
        }
        //eventSystem.SetSelectedGameObject(null);

        /*
        switch (menuToOpen)
        {
            case 1: //Application Menu
                applicationOpen = !applicationOpen;
                applicationPanel.SetActive(applicationOpen);
                monsterOpen = false;
                monsterPanel.SetActive(false);
                departmentPanel.SetActive(false);
                departmentOpen = false;
                assignmentPanel.SetActive(false);
                assignmentOpen = false;
                break;
            case 2: //Monster Menu
                monsterOpen = !monsterOpen;
                monsterPanel.SetActive(monsterOpen);
                applicationPanel.SetActive(false);
                applicationOpen = false;
                departmentPanel.SetActive(false);
                departmentOpen = false;
                assignmentPanel.SetActive(false);
                assignmentOpen = false;
                break;
            case 3: //Department Menu
                departmentOpen = !departmentOpen;
                departmentPanel.SetActive(departmentOpen);
                applicationPanel.SetActive(false);
                applicationOpen = false;
                monsterPanel.SetActive(false);
                monsterOpen = false;
                assignmentPanel.SetActive(false);
                assignmentOpen = false;
                break;
            case 4: //Assignment Menu
                assignmentOpen = !assignmentOpen;
                assignmentPanel.SetActive(assignmentOpen);
                applicationPanel.SetActive(false);
                applicationOpen = false;
                departmentPanel.SetActive(false);
                departmentOpen = false;
                monsterPanel.SetActive(false);
                monsterOpen = false;
                break;
            case 5: //Room Options Menu
                if (roomMenuOpen)
                {
                    gameManager.RoomMenuHandler(0);
                }
                roomMenuOpen = !roomMenuOpen;
                roomMenu.SetActive(roomMenuOpen);
                roomMenuConfirm.onClick.RemoveAllListeners();
                break;
            case 6: //Pause Menu
                pauseMenuOpen = !pauseMenuOpen;
                pauseMenu.SetActive(pauseMenuOpen);
                applicationPanel.SetActive(false);
                applicationOpen = false;
                departmentPanel.SetActive(false);
                departmentOpen = false;
                monsterPanel.SetActive(false);
                monsterOpen = false;
                roomMenuOpen = false;
                roomMenu.SetActive(false);
                assignmentOpen = false;
                assignmentPanel.SetActive(false);
                break;
        }
        */
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

    //function should be called whenever the applicationList is changed
    public void UpdateApplications()
    {
        //reset the panel
        int childNum = 0;
        foreach (Transform child in menus[0].transform.GetChild(0).transform)
        {
            if (childNum != 0)
            {
                Destroy(child.gameObject);
            }
            childNum++;
        }

        //Create a field for each application pending
        foreach (GameObject application in gameManager.applicationsList)
        {
            //Create new field in the Application Panel and set up its Name, size, position, and button function
            var newField = Instantiate(applicationField, new Vector3(0, 0, 0), Quaternion.identity);
            var newFieldCanvas = newField.transform.GetChild(0);
            var newFieldCanvasRect = newField.transform.GetChild(0).GetComponent<RectTransform>();
            var newContext = Instantiate(applicationContextField, new Vector3(0, 0, 0), Quaternion.identity);
            var newContextCanvas = newContext.transform.GetChild(0);
            var newContextCanvasRect = newContextCanvas.GetComponent<RectTransform>();

            newFieldCanvas.transform.GetChild(0).GetComponent<Image>().color = application.GetComponent<SpriteRenderer>().color;
            newFieldCanvas.transform.GetChild(1).GetComponent<Text>().text = application.name;
            newFieldCanvas.transform.GetChild(2).GetComponent<Text>().text = application.GetComponent<BaseMonster>().getType();
            newField.GetComponent<RectTransform>().sizeDelta = new Vector2(210f, 80f);

            newContextCanvas.transform.GetChild(0).GetComponent<Text>().text = "HP: " + application.GetComponent<BaseMonster>().getMaxHealth().ToString();
            newContextCanvas.transform.GetChild(1).GetComponent<Text>().text = "Atk: " + application.GetComponent<BaseMonster>().getBaseDamage().ToString();
            newContextCanvas.transform.GetChild(2).GetComponent<Text>().text = "Def: " + application.GetComponent<BaseMonster>().getArmor().ToString();
            newContextCanvas.transform.GetChild(3).GetComponent<Text>().text = "Thr: " + application.GetComponent<BaseMonster>().getThreat().ToString();

            //stats/interview button
            newFieldCanvas.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(delegate { ToggleContext(newContext); });

            //hire button
            newFieldCanvas.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate { gameManager.HireButton(application, newField); });
            newContextCanvas.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(delegate { gameManager.Interview(application); });

            newField.transform.SetParent(menus[0].transform.GetChild(0).transform, false);
            newContext.transform.SetParent(menus[0].transform.GetChild(0).transform, false);
            //newContext.transform.SetParent(newFieldCanvas.transform, false);

            //manually adjust its position           
            newFieldCanvasRect.anchoredPosition = new Vector2(0, 0);
            newFieldCanvasRect.anchorMin = new Vector2(0.5f, 0.5f);
            newFieldCanvasRect.anchorMax = new Vector2(0.5f, 0.5f);
            newContextCanvasRect.anchoredPosition = new Vector2(0, 0);
            newContextCanvasRect.anchorMin = new Vector2(0.5f, 0.5f);
            newContextCanvasRect.anchorMax = new Vector2(0.5f, 0.5f);
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
        //Debug.Log(gameManager.monsterList.Count);
        //create a field for each Monster
        foreach (GameObject monster in gameManager.monsterList)
        {
            if (!gameManager.deadMonsters.Contains(monster))
            {
                //Create the field and set up its name and button functionality
                var newField = Instantiate(monsterField, new Vector3(0, 0, 0), Quaternion.identity);
                var newFieldCanvas = newField.transform.GetChild(0);
                var newFieldCanvasRect = newFieldCanvas.GetComponent<RectTransform>();
                var newContext = Instantiate(monsterContextField, new Vector3(0, 0, 0), Quaternion.identity);
                var newContextCanvas = newContext.transform.GetChild(0);
                var newContextCanvasRect = newContextCanvas.GetComponent<RectTransform>();
                newField.GetComponent<RectTransform>().sizeDelta = new Vector2(210f, 80f);

                newFieldCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = monster.GetComponent<SpriteRenderer>().color;
                newFieldCanvas.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = monster.name;
                newFieldCanvas.transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = monster.GetComponent<BaseMonster>().getType();
                newContextCanvas.transform.GetChild(0).GetComponent<Text>().text = "HP: " + monster.GetComponent<BaseMonster>().getCurHealth().ToString() + "/" + monster.GetComponent<BaseMonster>().getMaxHealth().ToString();
                newContextCanvas.transform.GetChild(1).GetComponent<Text>().text = "Atk: " + monster.GetComponent<BaseMonster>().getBaseDamage().ToString();
                newContextCanvas.transform.GetChild(2).GetComponent<Text>().text = "Def: " + monster.GetComponent<BaseMonster>().getArmor().ToString();
                newContextCanvas.transform.GetChild(3).GetComponent<Text>().text = "Thr: " + monster.GetComponent<BaseMonster>().getThreat().ToString();

                //Revealing Traits

                if (monster.GetComponent<BaseMonster>().revealedTraits.Count == 3)
                {
                    newContextCanvas.transform.GetChild(5).GetComponent<Text>().text = monster.GetComponent<BaseMonster>().revealedTraits[0];
                    newContextCanvas.transform.GetChild(6).GetComponent<Text>().text = monster.GetComponent<BaseMonster>().revealedTraits[1];
                    newContextCanvas.transform.GetChild(7).GetComponent<Text>().text = monster.GetComponent<BaseMonster>().revealedTraits[2];
                }
                else if (monster.GetComponent<BaseMonster>().revealedTraits.Count == 2)
                {
                    newContextCanvas.transform.GetChild(5).GetComponent<Text>().text = monster.GetComponent<BaseMonster>().revealedTraits[0];
                    newContextCanvas.transform.GetChild(6).GetComponent<Text>().text = monster.GetComponent<BaseMonster>().revealedTraits[1];
                }
                else if (monster.GetComponent<BaseMonster>().revealedTraits.Count == 1)
                {
                    newContextCanvas.transform.GetChild(5).GetComponent<Text>().text = monster.GetComponent<BaseMonster>().revealedTraits[0];
                }


                newFieldCanvas.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(delegate { ToggleContext(newContext); });
                if (monster.GetComponent<BaseMonster>().department == gameManager.breakRoomList)
                {
                    newFieldCanvas.transform.GetChild(1).GetComponentInChildren<Text>().text = "Assign";
                    newFieldCanvas.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate 
                    {
                        gameManager.selectedObjects.Add(monster);
                        //monster.GetComponent<SpriteRenderer>().enabled = true;
                    });
                }
                else
                {
                    newFieldCanvas.transform.GetChild(1).GetComponentInChildren<Text>().text = "Break Time!";
                    newFieldCanvas.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
                    {
                        monster.GetComponent<BaseMonster>().getCurRoom().GetComponent<BaseRoom>().roomMembers.Remove(monster);     
                        gameManager.AddToDepartment(monster, gameManager.breakRoomList);
                        monster.GetComponent<BaseMonster>().setCurRoom(null);
                        monster.GetComponent<SpriteRenderer>().enabled = false;
                    });
                }
                newField.transform.SetParent(menus[1].transform.GetChild(0).transform, false);
                newContext.transform.SetParent(menus[1].transform.GetChild(0).transform, false);

                //manually adjust its position
                newFieldCanvasRect.anchoredPosition = new Vector2(0, 0);
                newFieldCanvasRect.anchorMin = new Vector2(0.5f, 0.5f);
                newFieldCanvasRect.anchorMax = new Vector2(0.5f, 0.5f);
                newContextCanvasRect.anchoredPosition = new Vector2(0, 0);
                newContextCanvasRect.anchorMin = new Vector2(0.5f, 0.5f);
                newContextCanvasRect.anchorMax = new Vector2(0.5f, 0.5f);
            }
        }
        //Debug.Log("c");
    }

    //Call this whenever you change the Department Panel
    public void UpdateDepartments()
    {
        //reset panel
        int childNum = 0;
        foreach (Transform child in menus[2].transform.GetChild(0).transform)
        {
            if (childNum != 0)
            {
                Destroy(child.gameObject);
            }
            childNum++;
        }

        var newBreakRoomHeader = Instantiate(breakRoomHeader, new Vector3(0, 0, 0), Quaternion.identity);
        newBreakRoomHeader.GetComponent<RectTransform>().sizeDelta = new Vector2(217, 30);
        newBreakRoomHeader.transform.SetParent(menus[2].transform.GetChild(0).transform, true);
        newBreakRoomHeader.GetComponent<Image>().enabled = true;
        newBreakRoomHeader.GetComponentInChildren<Text>().enabled = true;
        newBreakRoomHeader.GetComponentInChildren<Text>().text = "Unassigned";

        foreach (GameObject monster in gameManager.breakRoomList)
        {
            var newField = Instantiate(monsterField, new Vector3(0, 0, 0), Quaternion.identity);
            var newFieldCanvas = newField.transform.GetChild(0);
            var newFieldCanvasRect = newFieldCanvas.GetComponent<RectTransform>();
            newFieldCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = monster.GetComponent<SpriteRenderer>().color;
            newFieldCanvas.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = monster.name;
            newFieldCanvas.transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = monster.GetComponent<BaseMonster>().getType();
            newFieldCanvas.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
            newField.transform.SetParent(menus[2].transform.GetChild(0).transform, false);

            //manually adjust its position
            newFieldCanvasRect.anchoredPosition = new Vector2(0, 0);
            newFieldCanvasRect.anchorMin = new Vector2(0.5f, 0.5f);
            newFieldCanvasRect.anchorMax = new Vector2(0.5f, 0.5f);
        }

        var newPRHeader = Instantiate(prHeader, new Vector3(0, 0, 0), Quaternion.identity);
        newPRHeader.GetComponent<RectTransform>().sizeDelta = new Vector2(217, 30);
        newPRHeader.transform.SetParent(menus[2].transform.GetChild(0).transform, false);
        newPRHeader.GetComponent<Image>().enabled = true;
        Text newPRHeaderText = newPRHeader.GetComponentInChildren<Text>();
        newPRHeaderText.enabled = true;
        newPRHeaderText.text = "Pillaging and Raiding " + gameManager.prList.Count + "/3";

        foreach (GameObject monster in gameManager.prList)
        {
            var newField = Instantiate(monsterField, new Vector3(0, 0, 0), Quaternion.identity);
            var newFieldCanvas = newField.transform.GetChild(0);
            var newFieldCanvasRect = newFieldCanvas.GetComponent<RectTransform>();
            newFieldCanvas.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = monster.name;
            newFieldCanvas.transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = monster.GetComponent<BaseMonster>().getType();
            newFieldCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = monster.GetComponent<SpriteRenderer>().color;

            newFieldCanvas.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate { gameManager.AddToDepartment(monster, gameManager.breakRoomList); });

            newFieldCanvas.transform.GetChild(1).GetComponentInChildren<Text>().text = "Remove";
            newField.transform.SetParent(menus[2].transform.GetChild(0).transform, false);

            //manually adjust position
            newFieldCanvasRect.anchoredPosition = new Vector2(0, 0);
            newFieldCanvasRect.anchorMin = new Vector2(0.5f, 0.5f);
            newFieldCanvasRect.anchorMax = new Vector2(0.5f, 0.5f);
        }

        if (gameManager.prList.Count < prCapacity)
        {
            var emptySlotPR = Instantiate(emptyField, new Vector3(0, 0, 0), Quaternion.identity);
            var emptySlotPRCanvas = emptySlotPR.transform.GetChild(0);
            var emptySlotPRCanvasRect = emptySlotPRCanvas.GetComponent<RectTransform>();
            emptySlotPR.GetComponentInChildren<Button>().onClick.AddListener(delegate
            {
                UpdateAssignment(gameManager.prList);
                ToggleMenu(3);
            });
            emptySlotPR.transform.SetParent(menus[2].transform.GetChild(0).transform, false);
            emptySlotPR.GetComponent<Image>().enabled = true;

            emptySlotPRCanvasRect.anchoredPosition = new Vector2(0, 0);
            emptySlotPRCanvasRect.anchorMin = new Vector2(0.5f, 0.5f);
            emptySlotPRCanvasRect.anchorMax = new Vector2(0.5f, 0.5f);
            emptySlotPRCanvas.GetComponent<Canvas>().enabled = true;
            emptySlotPRCanvas.GetComponent<CanvasScaler>().enabled = true;
            emptySlotPRCanvas.GetComponent<GraphicRaycaster>().enabled = true;
            emptySlotPRCanvas.transform.GetChild(0).GetComponent<Image>().enabled = false;
            //emptySlotPRCanvas.transform.GetChild(1).GetComponent<Text>().enabled = false;
            //emptySlotPRCanvas.transform.GetChild(4).GetComponent<Text>().enabled = false;
            //emptySlotPRCanvas.transform.GetChild(3).GetComponent<RectTransform>().position = Vector2.zero;
        }
    }

    //Call this to update the assignment menu with monsters availble to be assigned
    public void UpdateAssignment(List<GameObject> department)
    {
        //reset panel
        int childNum = 0;
        foreach (Transform child in menus[3].transform.GetChild(0).transform)
        {
            if (childNum != 0)
            {
                Destroy(child.gameObject);
            }
            childNum++;
        }

        //create a field for each Monster
        foreach (GameObject monster in gameManager.breakRoomList) //NEED TO USE AN "INACTIVE" LIST FOR THIS LATER
        {
            //Create the field and set up its name and button functionality
            var newField = Instantiate(monsterField, new Vector3(0, 0, 0), Quaternion.identity);
            var newFieldCanvas = newField.transform.GetChild(0);
            var newFieldCanvasRect = newFieldCanvas.GetComponent<RectTransform>();
            newFieldCanvas.transform.GetChild(0).transform.GetChild(1).GetComponent<Text>().text = monster.name;
            newFieldCanvas.transform.GetChild(0).transform.GetChild(3).GetComponent<Text>().text = monster.GetComponent<BaseMonster>().getType();

            newFieldCanvas.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().color = monster.GetComponent<SpriteRenderer>().color;
            newFieldCanvas.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(delegate
            {
                gameManager.AddToDepartment(monster, department);

                ToggleMenu(2);
            });
            newField.transform.SetParent(menus[3].transform.GetChild(0).transform, false);

            //manually adjust its position            
            newFieldCanvasRect.anchoredPosition = new Vector2(0, 0);
            newFieldCanvasRect.anchorMin = new Vector2(0.5f, 0.5f);
            newFieldCanvasRect.anchorMax = new Vector2(0.5f, 0.5f);
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

    public void UpdateCurrency()
    {
        currencyText.text = "Gold: " + gameManager.currentCurrency + " / " + gameManager.maximumCurrency;
    }

    public void UpdateInfamy()
    {
        infamyLevelText.text = "InfamyLevel: " + gameManager.infamyLevel;
        infamyXPText.text = "InfamyXP: " + gameManager.infamyXP + "/" + gameManager.xpToNextInfamyLevel;
    }
}
