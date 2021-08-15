using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [Tooltip("Ship to Control")]
    [SerializeField]
    private AShip m_Ship;

    [Range(-1, 1)]
    [SerializeField]
    public float m_RotateDirection;

    /// <summary>
    /// All Towers attached to <see cref="m_Ship"/>.
    /// </summary>
    private WeaponTower[] WeaponTowers { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        if (m_Ship == null)
            Debug.LogError($"{nameof(m_Ship)} is null!");

        WeaponTowers = m_Ship.GetComponentsInChildren<WeaponTower>();

        foreach (WeaponTower wt in WeaponTowers)
        {
            wt.PlaceWeapon(WEAPONTYPE.CANNON);
            wt.PlaceWeapon(WEAPONTYPE.CANNON);
        }
    }

    // Update is called once per frame
    void Update()
    {
        bool reachedTarget = DoMovementShip();
        Debug.Log(DoRotationShip());

        if (reachedTarget)
        {
            DoShooting();
        }
            
    }

    private bool DoMovementShip()
    {
        return m_Ship.MoveToPosition(GetPlayer().ControlledShip.gameObject.transform.position);
    }

    /// <summary>
    /// Rotate the ship
    /// </summary>
    /// <returns>true if ship looks at player; else false</returns>
    private bool DoRotationShip()
    {
        Transform LookAtEnemy = PlayerContr.Get.ControlledShip.gameObject.transform;  // Who is the enemy?

        //  If you find the enemy, do this:
        var rotate = Quaternion.LookRotation(LookAtEnemy.position - m_Ship.transform.position);  //  This is the amount to rotate
        float rotationLeft = rotate.eulerAngles.y - m_Ship.transform.rotation.eulerAngles.y;

        if (rotationLeft <= 5
            && rotationLeft >= -5)
        {
            m_Ship.RotateShip(0);
            return true;
        }
        else
        {
            m_Ship.RotateShip(Mathf.Clamp(rotationLeft, -1, 1));
            return false;
        }

    }

    private void DoShooting()
    {
        foreach (WeaponTower tower in WeaponTowers)
        {
            AWeapon[] weapons = tower.CurrentAttachedWeapons;

            foreach (AWeapon w in weapons)
            {
                if (w.WeaponEnabled)
                    w.Shoot();
            }
        }
        //TODO: Shoot
    }

    private void DoRotationCannon()
    {

    }


    private PlayerContr GetPlayer()
    {
        return PlayerContr.Get;
    }
}
