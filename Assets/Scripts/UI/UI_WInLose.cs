using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_WInLose : MonoBehaviour
{
    [SerializeField]
    public string MainMenu;

    public void bttn_Mainmenu_Click()
    {
        SceneManager.LoadScene(MainMenu);
    }
}
