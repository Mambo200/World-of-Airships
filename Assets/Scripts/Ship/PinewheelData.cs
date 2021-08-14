using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/PinewheelData")]
public class PinewheelData : ScriptableObject
{
    [SerializeField, Min(0)]
    private float height;
    public float Height
    {
        get
        {
            return height;
        }
    }
}
