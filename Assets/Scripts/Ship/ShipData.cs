using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/ShipData")]
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
    [SerializeField]
    private ShipType type;
    public ShipType Type
    {
        get
        {
            return type;
        }
    }
    [SerializeField]
    private float hitpoints;
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
