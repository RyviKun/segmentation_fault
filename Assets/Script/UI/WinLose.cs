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
        gameWin.SetActive(false);
        gameOver.SetActive(false);
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        if (_levelLoader.level < 3)
        {
            _levelLoader.level += 1;
            gameWin.SetActive(false);
            gameOver.SetActive(false);
            _levelLoader.LoadLevel();
        }
        else
        {
            // Sudah di level maksimal, biarkan gameWin tetap menyala
            Debug.Log("Sudah mencapai level terakhir.");
        }
    }

    public void Retry()
    {
        _levelLoader.level = _levelLoader.level;
        gameWin.SetActive(false);
        gameOver.SetActive(false);
        _levelLoader.LoadLevel();
    }
}
