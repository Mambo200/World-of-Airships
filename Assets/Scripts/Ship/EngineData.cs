using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/EngineData")]
public class EngineData : ScriptableObject
{
    [SerializeField]
    private float deadZone = 0.1f;
    public float DeadZone
    {
        get
        {
            return deadZone;
        }
    }
    [SerializeField]
    private float forwardAcl = 100f;
    public float ForwardAcl
    {
        get
        {
            return forwardAcl;
        }
    }
    [SerializeField]
    private float backwardAcl = 25f;
    public float BackwardAcl
    {
        get
        {
            return backwardAcl;
        }
    }
    [SerializeField]
    private float turnStrength = 10f;
    public float TurnStrength
    {
        get
        {
            return turnStrength;
        }
    }
    [SerializeField]
    private float hoverForce = 9f;
    public float HoverForce
    {
        get
        {
            return hoverForce;
        }
    }
    [SerializeField]
    private float hoverHeight = 2f;
    public float HoverHeight
    {
        get
        {
            return hoverHeight;
        }
    }
}

