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
    [SerializeField] private LevelLoader _levelLoader;
    public void PlayButton()
    {
        mainMenu.SetActive(false);
        levelSelector.SetActive(true);
    }
    public void PlayGame()
    {
        Debug.Log("its been clicked");
        _levelLoader.LoadLevel();
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

    public void SelectLevel(int level)
    {
        _levelLoader.level = level;
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
