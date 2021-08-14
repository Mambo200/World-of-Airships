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

    [SerializeField]
    private Engine engine;
    [SerializeField]
    private List<Pinwheels> pinewheels;
    [SerializeField]
    private List<WeaponTower> turrets;
    [SerializeField]
    private List<AWeapon> weapons;

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
            if (hitpoints > data.Hitpoints)
            {
                hitpoints = data.Hitpoints;
            }
            if (hitpoints <= 0)
            {
                //TODO: Lose Condition einbauen
            }
        }
    }

    [Header("Hover Settings")]
    private Rigidbody rigid;
    public Rigidbody Rigid
    {
        get
        {
            return rigid;
        }
    }

    [Header("Navmesh Settings")]
    private NavMeshAgent agent;

    public virtual void Start()
    {
        rigid = GetComponent<Rigidbody>();
        if (rigid == null)
        {
            Debug.LogWarning("Schiff braucht ein Rigidbody");
        }
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogWarning("Schiff braucht ein Rigidbody oder NavmeshAgent");
        }

        engine.Init(this);

        foreach (WeaponTower tower in GetComponentsInChildren<WeaponTower>())
        {
            turrets.Add(tower);
        }

        foreach (AWeapon weapon in GetComponentsInChildren<AWeapon>())
        {
            weapons.Add(weapon);
        }
    }

    /// <summary>
    /// Bewegt das Schiff vor oder zurück
    /// </summary>
    /// <param name="_dir">Richtung pos == vor und neg == zurück</param>
    public virtual void MoveShip(float _dir)
    {
        if (rigid != null)
        {
            engine.CurrentThrust = 0;
            if (_dir > engine.Data.DeadZone)
            {
                engine.CurrentThrust = _dir * engine.Data.ForwardAcl;
            }
            else if (_dir < -engine.Data.DeadZone)
            {
                engine.CurrentThrust = _dir * engine.Data.BackwardAcl;
            }
        }
        else
        {
            Debug.LogWarning("Beim Schiff fehlt der Rigidbody");
        }
        //TODO: Move Ship
    }

    /// <summary>
    /// Bewegt das Schiff nach links oder rechts
    /// </summary>
    /// <param name="_dir">Richtung</param>
    public virtual void RotateShip(float _dir)
    {
        if (rigid != null)
        {
            engine.CurrentTurn = 0;
            if (Mathf.Abs(_dir) > engine.Data.DeadZone)
                engine.CurrentTurn = _dir;
        }
        else
        {
            Debug.LogWarning("Beim Schiff fehlt der Rigidbody");
        }
        //TODO: Rotate Ship
    }

    /// <summary>
    /// Dreht die Geschütztürme
    /// </summary>
    /// <param name="_x">X Axis</param>
    /// <param name="_y">Y Axis</param>
    public virtual void RotateWeapon(float _x, float _y)
    {
        foreach (WeaponTower tower in turrets)
        {
            tower.RotateX(_x);
            tower.RotateY(_y);
        }
        //TODO: Rotate WeaponTower
    }

    public virtual void Fire()
    {
        foreach (AWeapon weapon in weapons)
        {
            weapon.Shoot();
        }
        //TODO: Weapon Fire
    }

    /// <summary>
    /// Move to Position via <see cref="NavMeshAgent"/>
    /// </summary>
    /// <param name="_pos">position to move to</param>
    /// <returns>true if object reached its target; else false</returns>
    public bool MoveToPosition(Vector3 _pos)
    {
        //TODO: Add Movement from navemeshAgent
        agent.SetDestination(_pos);

        return agent.remainingDistance <= agent.stoppingDistance;
    }
}
