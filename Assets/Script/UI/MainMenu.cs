using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        Debug.Log("its been clicked");
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        if (EditorApplication.isPlaying)
        {
            EditorApplication.isPlaying = false;
        }
        Application.Quit();
    }
}
