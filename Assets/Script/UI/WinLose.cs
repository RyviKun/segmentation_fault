using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
public class WinLose : MonoBehaviour
{
    public GameObject gameWin;
    public GameObject gameOver;
    [SerializeField] private LevelLoader _levelLoader;

    public void GameWin() 
    { 
        gameWin.SetActive(true);
    }

    public void GameOver() 
    { 
        gameOver.SetActive(true);
    }

    public void BackToSelect()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        _levelLoader.level = _levelLoader.level += 1;
        _levelLoader.LoadLevel();
    }
}
