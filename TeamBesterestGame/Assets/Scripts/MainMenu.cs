using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

    public GameObject start;
    public GameObject load;
    public GameObject delete;

    public GameObject transition;

    public Scene scene;

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
        StartCoroutine(LoadLevel());
    }

    IEnumerator LoadLevel()
    {
        transition.SetActive(true);
        yield return new WaitForSeconds(3);

        AsyncOperation async = SceneManager.LoadSceneAsync(1);

        while (!async.isDone)
        {
            yield return null;
        }
    }

    public void Delete()
    {
        PlayerPrefs.DeleteAll();
        load.SetActive(false);
        delete.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
