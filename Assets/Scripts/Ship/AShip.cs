using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AShip : MonoBehaviour
{
    [SerializeField]
    private ShipData data;

    private UI_AUnit ui;
    public UI_AUnit UI
    {
        get
        {
            return ui;
        }
        set
        {
            ui = value;
        }
    }

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

            if (hitpoints > data.Hitpoints)
            {
                hitpoints = data.Hitpoints;
            }

            if (ui != null)
                ui.SetHealthbar(hitpoints, data.Hitpoints);

            if (hitpoints <= 0)
            {
                hitpoints = 0;
                GameManager.Instance.ActivateLoseCondition();
            }
        }
    }

    private float energiepoints;
    public float Energiepoints
    {
        get
        {
            return energiepoints;
        }
        set
        {
            energiepoints = value;
            if(energiepoints > data.Energierpoints)
            {
                energiepoints = data.Energierpoints;
            }

            if (ui != null)
                ui.SetEnergiebar(energiepoints, data.Energierpoints);

            if(energiepoints < 0)
            {
                energiepoints = 0;
            }
        }
    }

    [SerializeField]
    private Engine engine;
    [SerializeField]
    private List<Pinwheels> pinewheels;
    [SerializeField]
    private List<WeaponTower> turrets;
    [SerializeField]
    private List<AWeapon> weapons;

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
        if(rigid == null)
        {
            agent = GetComponent<NavMeshAgent>();
            if(agent == null)
            {
                Debug.LogWarning("Schiff braucht ein Rigidbody oder NavmeshAgent");
            }
        }

        Hitpoints = data.Hitpoints;
        Energiepoints = data.Energierpoints;

        engine.Init(this);

        foreach(WeaponTower tower in GetComponentsInChildren<WeaponTower>())
        {
            turrets.Add(tower);
        }

        foreach(AWeapon weapon in GetComponentsInChildren<AWeapon>())
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
        if(rigid != null)
        {
            engine.CurrentThrust = 0;
            if(_dir > engine.Data.DeadZone)
            {
                engine.CurrentThrust = _dir * engine.Data.ForwardAcl;
            }else if(_dir < -engine.Data.DeadZone)
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
        if(rigid != null)
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
        foreach(WeaponTower tower in turrets)
        {
            tower.RotateX(_x);
            tower.RotateY(_y);
        }
        //TODO: Rotate WeaponTower
    }

    public virtual void Fire()
    {
        foreach(AWeapon weapon in weapons)
        {
            weapon.Shoot();
        }
        //TODO: Weapon Fire
    }

    public void MoveToPosition(Vector3 _pos)
    {
        //TODO: Add Movement from navemeshAgent
    }

    public virtual void AddDamage(float _damage)
    {
        Hitpoints -= _damage;
    }
}
