using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapmanager : MonoBehaviour
{
    public static Mapmanager Instance = null;

    [SerializeField]
    private List<Windzone> windzones = new List<Windzone>();

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            Debug.Log("Mapmanager ist gesetzt");
        }
    }

    public float GetEnergie(float _height)
    {
        float temp = 0;
        foreach(Windzone zone in windzones)
        {
            if (zone.InsideZone(_height))
            {
                temp = zone.Energie;
                break;
            }
        }
        return temp;
    }
}

[System.Serializable]
public class Windzone
{
    [SerializeField]
    private float minHeight;
    public float MinHeight
    {
        get
        {
            return minHeight;
        }
    }
    [SerializeField]
    private float maxHeight;
    public float MaxHeight
    {
        get
        {
            return maxHeight;
        }
    }
    [SerializeField]
    private float energie;
    public float Energie
    {
        get
        {
            return energie;
        }
    }

    public bool InsideZone(float _height)
    {
        return (_height > minHeight && _height < maxHeight) ? true : false;
    }
}
