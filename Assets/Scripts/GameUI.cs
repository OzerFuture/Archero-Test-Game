using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameUI : MonoBehaviour
{
    public GameObject pausemenu;
    public GameObject player;
    public GameObject pause;
    public GameObject coinsCount;
    public void Pause()

    {
        pausemenu.SetActive(true);
        pause.SetActive(false);
        coinsCount.SetActive(false);
        Time.timeScale = 0;
    }
    public void Resume()

    {
        pausemenu.SetActive(false);
        coinsCount.SetActive(true);
        pause.SetActive(true);
        Time.timeScale = 1;
    }

    public void Restart()

    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()

    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void NextLevel()

    {
        SceneManager.LoadScene(2);
    }


}
