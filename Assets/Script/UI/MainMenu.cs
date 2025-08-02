using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject levelSelector;
    public void PlayGame()
    {
        Debug.Log("its been clicked");
        // SceneManager.LoadSceneAsync(1);
        mainMenu.SetActive(false);
        levelSelector.SetActive(true);
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
    }
}
