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

    [Header("Hover Settings")]
    private Rigidbody rigid;
    [SerializeField]
    private float deadZone = 0.1f;

    [SerializeField]
    private float forwardAcl = 100f;
    [SerializeField]
    private float backwardAcl = 25f;
    [SerializeField]
    private float currentThrust = 0f;

    [SerializeField]
    private float turnStrength = 10f;
    [SerializeField]
    private float currentTurn = 0f;

    [SerializeField]
    private LayerMask IgnoreMask;
    [SerializeField]
    private float hoverForce = 9f;
    [SerializeField]
    private float hoverHeight = 2f;

    [SerializeField]
    private GameObject[] hoverPoints;

    private GameObject currentHoverPoint;

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

        IgnoreMask = ~IgnoreMask;

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
            currentThrust = 0;
            if(_dir > deadZone)
            {
                currentThrust = _dir * forwardAcl;
            }else if(_dir < -deadZone)
            {
                currentThrust = _dir * backwardAcl;
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
            currentTurn = 0;
            if (Mathf.Abs(_dir) > deadZone)
                currentTurn = _dir;
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

    public virtual void FixedUpdate()
    {
        if(rigid != null)
        {
            RaycastHit hit;
            for(int x = 0; x < hoverPoints.Length; x++)
            {
                currentHoverPoint = hoverPoints[x];
                if(Physics.Raycast(currentHoverPoint.transform.position,
                    -Vector3.up, out hit,
                    hoverHeight,
                    IgnoreMask))
                {
                    rigid.AddForceAtPosition(Vector3.up
                        * hoverForce
                        *  (1f - (hit.distance / hoverHeight)),
                        currentHoverPoint.transform.position);
                }
                else
                {
                    if(transform.position.y > currentHoverPoint.transform.position.y)
                    {
                        rigid.AddForceAtPosition(currentHoverPoint.transform.up
                            * hoverForce,
                            currentHoverPoint.transform.position);
                    }
                    else
                    {
                        rigid.AddForceAtPosition(currentHoverPoint.transform.up
                            * -hoverForce,
                            currentHoverPoint.transform.position);
                    }
                }
            }

            if(Mathf.Abs(currentThrust) > 0)
            {
                rigid.AddForce(transform.forward * currentThrust);
            }

            if(currentTurn > 0)
            {
                rigid.AddRelativeTorque(Vector3.up * currentTurn * turnStrength);
            }else if(currentTurn < 0)
            {
                rigid.AddRelativeTorque(Vector3.up * currentTurn * turnStrength);
            }
        }
    }
}
