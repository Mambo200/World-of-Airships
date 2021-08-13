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

    public override void Shoot()
    {
        base.Shoot();
        if (!WeaponEnabled) return;
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        GameObject.Instantiate(go, SpawnProjectileTransform);
        GameObject.Destroy(go);
    }
}
