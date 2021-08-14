using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        
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
        Debug.Log("Du hast das Spiel verloren");
    }
}
