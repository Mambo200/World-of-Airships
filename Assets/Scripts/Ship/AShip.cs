using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    private Rigidbody rigid;
    private NavMeshAgent agent;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        if(rigid == null)
        {
            agent = GetComponent<NavMeshAgent>();
            if(agent == null)
            {
                Debug.LogWarning("Schiff braucht ein Rigidbody oder NavmeshAgent");
            }
        }
    }

    /// <summary>
    /// Bewegt das Schiff vor oder zurück
    /// </summary>
    /// <param name="_dir">Richtung pos == vor und neg == zurück</param>
    public virtual void MoveShip(float _dir)
    {
        //TODO: Move Ship
    }

    /// <summary>
    /// Bewegt das Schiff nach links oder rechts
    /// </summary>
    /// <param name="_dir">Richtung</param>
    public virtual void RotateShip(float _dir)
    {
        //TODO: Rotate Ship
    }

    /// <summary>
    /// Dreht die Geschütztürme
    /// </summary>
    /// <param name="_x">X Axis</param>
    /// <param name="_y">Y Axis</param>
    public virtual void RotateWeapon(float _x, float _y)
    {
        //TODO: Rotate WeaponTower
    }

    public virtual void Fire()
    {
        //TODO: Weapon Fire
    }

    public void MoveToPosition(Vector3 _pos)
    {
        //TODO: Add Movement from navemeshAgent
    }
}
