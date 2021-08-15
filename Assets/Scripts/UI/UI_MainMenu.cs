using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    //TODO: Show Gamestuff in the Background
    [SerializeField]
    private GameObject win_levelOptions;
    [SerializeField]
    private GameObject win_controll;
    [SerializeField]
    private GameObject win_credits;

    public string Level0 = "";

    private void Start()
    {
        bttn_ShowControls_Click();
    }

    public void bttn_Play_Click()
    {
        win_levelOptions.SetActive(true);
        win_controll.SetActive(false);
        win_credits.SetActive(false);
    }

    public void bttn_ShowControls_Click()
    {
        win_levelOptions.SetActive(false);
        win_controll.SetActive(true);
        win_credits.SetActive(false);
    }

    public void bttn_Credit_Click()
    {
        win_levelOptions.SetActive(false);
        win_controll.SetActive(false);
        win_credits.SetActive(true);
    }

    public void bttn_Exit_Click()
    {
        Application.Quit();
    }

    public void bttn_LoadLevelOne_Click()
    {
        SceneManager.LoadScene(Level0);
    }
}
