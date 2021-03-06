using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : AWeapon
{
    public override WEAPONTYPE WeaponType => WEAPONTYPE.CANNON;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)
            && CanShoot)
        {
            Shoot();
        }
    }

    public override bool Shoot()
    {
        if(!base.Shoot()) return false;
        //Debug.Log($"{nameof(WeaponEnabled)} ==> {WeaponEnabled} || {nameof(CanShoot)} ==> {CanShoot}");

        AAmmo ammo = GameObject.Instantiate(ProjectileGameObject, SpawnProjectileTransform.position, SpawnProjectileTransform.rotation).GetComponent<AAmmo>();
        ammo.GetRigidbody.AddForce(SpawnProjectileTransform.forward * ShootForce, ForceMode.VelocityChange);

        return true;
    }
}
