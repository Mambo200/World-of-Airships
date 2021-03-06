using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class AShip : MonoBehaviour
{
    [Header("Shipsettings")]
    [SerializeField]
    private ShipData data;

    public bool IsPlayer = false;
    private AIController aiController;

    public void Init(AIController _controller)
    {
        aiController = _controller;
    }

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
    /// L?st Losecondition automatisch aus wenn 0
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
                if (IsPlayer)
                {
                    GameManager.Instance.ActivateLoseCondition();
                }
                else
                {
                    Destroy(aiController.gameObject);
                    GameManager.Instance.RemoveEnemyShip(this);
                }
            }
        }
    }

    private float maxEnergie = 0;

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
            if(energiepoints > maxEnergie)
            {
                energiepoints = maxEnergie;
            }

            if (ui != null)
                ui.SetEnergiebar(energiepoints, maxEnergie);

            if(energiepoints < 0)
            {
                energiepoints = 0;
            }
        }
    }

    public float Modifire
    {
        get
        {
            return (Energiepoints / maxEnergie);
        }
    }

    public float WeaponModifire
    {
        get
        {
            return 1 + (1 - (Energiepoints / maxEnergie));
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
        if (rigid == null)
        {
            Debug.LogWarning("Schiff braucht ein Rigidbody");
        }
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogWarning("Schiff braucht ein Rigidbody oder NavmeshAgent");
        }

        Hitpoints = data.Hitpoints;
        Energiepoints = data.Energierpoints;

        engine.Init(this);

        maxEnergie = 0;
        foreach(Pinwheels pinewhel in GetComponentsInChildren<Pinwheels>())
        {
            pinewheels.Add(pinewhel);
            maxEnergie += pinewhel.Data.MaxEnergie;
        }

        foreach (WeaponTower tower in GetComponentsInChildren<WeaponTower>())
        {
            turrets.Add(tower);
        }

        foreach (AWeapon weapon in GetComponentsInChildren<AWeapon>())
        {
            weapons.Add(weapon);
            weapon.Init(this);
        }
    }

    private void Update()
    {
        //Berechne Energie
        energiepoints = 0;
        foreach(Pinwheels wheel in pinewheels)
        {
            energiepoints += wheel.ProducedEnergie;
        }
        Energiepoints = energiepoints;


    }

    /// <summary>
    /// Bewegt das Schiff vor oder zur?ck
    /// </summary>
    /// <param name="_dir">Richtung pos == vor und neg == zur?ck</param>
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
    }

    /// <summary>
    /// Dreht die Gesch?tzt?rme
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
    }

    public virtual void Fire()
    {
        foreach (AWeapon weapon in weapons)
        {
            weapon.Shoot();
        }
    }

    /// <summary>
    /// Move to Position via <see cref="NavMeshAgent"/>
    /// </summary>
    /// <param name="_pos">position to move to</param>
    /// <returns>true if object reached its target; else false</returns>
    public bool MoveToPosition(Vector3 _pos)
    {
        agent.SetDestination(_pos);

        return agent.remainingDistance <= agent.stoppingDistance;
    }

    public virtual void AddDamage(float _damage)
    {
        Hitpoints -= _damage;
    }
}
