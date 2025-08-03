using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelector;
    public GameObject levelPreview;
    public GameObject credit;
    public void PlayGame()
    {
        Debug.Log("its been clicked");
        // SceneManager.LoadSceneAsync(1);
        mainMenu.SetActive(false);
        levelSelector.SetActive(true);
    }

    public void Credit()
    {
        credit.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void QuitGame()
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
        Application.Quit();
    }

    public void BackToMainMenu()
    {
        Debug.Log("Clicked");
        //SceneManager
        mainMenu.SetActive(true);
        levelSelector.SetActive(false);
        credit.SetActive(false);
    }

    public void SelectLevel()
    {
        levelPreview.SetActive(true);
        levelSelector.SetActive(false);
    }
    public void BackToLevelSelect()
    {
        Debug.Log("Clicked");
        //SceneManager
        levelSelector.SetActive(true);
        levelPreview.SetActive(false);
    }
}
