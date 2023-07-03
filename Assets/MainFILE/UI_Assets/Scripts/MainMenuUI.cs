using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private GameObject mainUI, settingsUI;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSurgery()
    {
        Debug.Log("Starting");
        SceneManager.LoadScene("Main");
    }

    public void Instructions()
    {

    }

    public void Settings()
    {
        settingsUI.SetActive(true);
        mainUI.SetActive(false);
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }

    public void Sound()
    {

    }

    public void Back()
    {
        mainUI.SetActive(true);
        settingsUI.SetActive(false);
    }
}
