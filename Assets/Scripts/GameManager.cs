using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string LoseScene = "Lose";
    public string WinScene = "WinScene";

    public List<AShip> livingEnemys = new List<AShip>();

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetIngameMouse();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetIngameMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
#if UNITY_EDITOR
        Debug.Log("Cursor set to Ingamemode");
#endif
    }

    public void SetMenuMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
#if UNITY_EDITOR
        Debug.Log("Cursor set to Menuemode");
#endif
    }

    public void ActivateLoseCondition()
    {
        SetMenuMouse();
        SceneManager.LoadScene(LoseScene);
    }

    public void RemoveEnemyShip(AShip _ship)
    {
        livingEnemys.Remove(_ship);
        Destroy(_ship.gameObject);

        if (livingEnemys.Count <= 0)
        {
            SetMenuMouse();
            SceneManager.LoadScene(WinScene);
        }
    }
}