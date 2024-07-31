using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject MenuUI;
    public GameObject ShopUI;
    public GameObject OptionsUI;
    public GameObject PlayUI;
   
    void Start()
    { 
    }

    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Shop()
    {

    }

    public void Settings()
    {
    }


    public void ToMainMenu()
    {
    }
}
