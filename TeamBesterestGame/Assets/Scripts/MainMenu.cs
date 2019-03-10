using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject start;
    public GameObject load;
    public GameObject delete;

    private void Start()
    {
        if (PlayerPrefs.GetString("scene") == "") {
            load.SetActive(false);
            delete.SetActive(false);
        }
    }

	public void Load()
    {
        load.SetActive(false);
        string scene = PlayerPrefs.GetString("scene");
        GlobalVariables.loading = true;
        SceneManager.LoadScene(scene);
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
        load.SetActive(false);
        delete.SetActive(false);
    }
}
