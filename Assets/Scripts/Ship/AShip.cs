using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AShip : MonoBehaviour
{
    [SerializeField]
    private ShipData data;

    public string ClassName
    {
        get
        {
            return data.ClassName;
        }
    }

    public ShipType Type
    {
        get
        {
            return data.Type;
        }
    }

    private float hitpoints;

    /// <summary>
    /// Sind die Aktuellen Hitpoints des Schiffs
    /// Begrenzt automatisch beim Maximum
    /// Löst Losecondition automatisch aus wenn 0
    /// </summary>
    public float Hitpoints
    {
        get
        {
            return hitpoints;
        }
        set
        {
            hitpoints = value;
            if(hitpoints > data.Hitpoints)
            {
                hitpoints = data.Hitpoints;
            }
            if(hitpoints <= 0)
            {
                //TODO: Lose Condition einbauen
            }
        }
    }

    public virtual void Init()
    {
        
    }


    /// <summary>
    /// Bewegt das Schiff vor oder zurück
    /// </summary>
    /// <param name="_dir">Richtung pos == vor und neg == zurück</param>
    public virtual void MoveShip(float _dir)
    {

    }

    /// <summary>
    /// Bewegt das Schiff nach links oder rechts
    /// </summary>
    /// <param name="_dir">Richtung</param>
    public virtual void RotateShip(float _dir)
    {

    }

    /// <summary>
    /// Dreht die Geschütztürme
    /// </summary>
    /// <param name="_x">X Axis</param>
    /// <param name="_y">Y Axis</param>
    public virtual void RotateWeapon(float _x, float _y)
    {

    }

    public virtual void Fire()
    {

    }
}
