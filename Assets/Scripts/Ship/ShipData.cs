using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipData : ScriptableObject
{
    [SerializeField]
    private string className;
    public string ClassName
    {
        get
        {
            return className;
        }
    }
    private ShipType type;
    [SerializeField]
    public ShipType Type
    {
        get
        {
            return type;
        }
    }
    private float hitpoints;
    [SerializeField]
    public float Hitpoints
    {
        get
        {
            return hitpoints;
        }
    }
}

public enum ShipType
{
    NONE,
}
