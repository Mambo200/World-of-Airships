using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AWeapon : MonoBehaviour
{
    #region Life
    [SerializeField]
    private float m_life;
    public float Life
    {
        get => m_life;
        protected set
        {
            m_life -= value;
            OnReceivedDamage(value);

            if (m_life <= 0
                && !m_OnDeathCalled)
                OnDeath();
        }
    }

    /// <summary>
    /// Gets called after weapon took damage
    /// </summary>
    /// <param name="_damage"></param>
    protected virtual void OnReceivedDamage(float _damage)
    {

    }

    private bool m_OnDeathCalled;
    /// <summary>
    /// Gets called when weapon gets destroyed
    /// </summary>
    protected virtual void OnDeath()
    {
        m_OnDeathCalled = true;
    }
    #endregion

    [SerializeField]
    private GameObject m_ProjectileGameObject;
    protected GameObject ProjectileGameObject
    {
        get => m_ProjectileGameObject;
    }
    [SerializeField]
    private float m_ShootForce;
    public float ShootForce
    {
        get => m_ShootForce;
        protected set => m_ShootForce = value; 
    }

    #region Weapon Status
    [Tooltip("Value for en- and disabling this weapon")]
    [SerializeField]
    private bool m_weaponEnabled = true;
    /// <summary>Weapon active status</summary>
    public bool WeaponEnabled
    {
        get => m_weaponEnabled;
        private set => m_weaponEnabled = value;
    }

    /// <summary>
    /// Activate weapon
    /// </summary>
    public void ActivateWeapon()
    {
        WeaponEnabled = true;
    }

    /// <summary>
    /// Deactivate weapon
    /// </summary>
    public void DeactivateWeapon()
    {
        WeaponEnabled = false;
    }

    public abstract WEAPONTYPE WeaponType { get; }
    #endregion

    [Tooltip("Transform where the projectile will spawn after weapon shot")]
    [SerializeField]
    private Transform m_SpawnProjectileLocation;
    /// <summary>
    /// Projectile Spawn Transform
    /// </summary>
    public Transform SpawnProjectileTransform
    {
        get => m_SpawnProjectileLocation;
    }

    // Start is called before the first frame update
    protected virtual void Start()
    {
        if (SpawnProjectileTransform == null)
        {
            Debug.LogWarning("This object does not has an assigned SpawnProjectileLocation", this.gameObject);
        }
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (CurrentShootCooldownTimer > 0)
        {
            // reloading
            CurrentShootCooldownTimer -= Mathf.Max(0, Time.deltaTime * CurrentShootCooldownMultiplier);

            if (CurrentShootCooldownTimer == 0)
            {
                // reloading weapon done
                CanShootAgain();
            }
        }
    }

    #region Shoot and Timer
    [Tooltip("Absolute time it takes to load to reload weapon.")]
    [SerializeField]
    private float m_shootCooolDownTime;
    /// <summary>
    /// Time it takes to reload weapon when weapon shot. <see cref="CurrentShootCooldownTimer"/> will be set to this value if weapon shot.
    /// </summary>
    public float ShootCooldownTime
    {
        get => m_shootCooolDownTime;
        protected set => m_shootCooolDownTime = value;
    }

    private float m_currentShootCooldownTimer;
    /// <summary>
    /// The current reload time. If this hits zero, it can shoot.
    /// </summary>
    public float CurrentShootCooldownTimer
    {
        get => m_currentShootCooldownTimer;
        protected set => m_currentShootCooldownTimer = value;
    }

    [Tooltip("The multiplier with which decrease time gets multiplied")]
    [SerializeField]
    private float m_currentShootCooldownMultiplier = 1;
    /// <summary>
    /// The current reload time
    /// </summary>
    public float CurrentShootCooldownMultiplier
    {
        get => m_currentShootCooldownMultiplier;
        protected set => m_currentShootCooldownMultiplier = value;
    }

    /// <summary>
    /// Can current weapon shoot
    /// </summary>
    public bool CanShoot
    {
        get => CurrentShootCooldownTimer <= 0;
    }

    /// <summary>
    /// This method gets called when weapon can shoot again
    /// </summary>
    public virtual void CanShootAgain()
    {

    }

    /// <summary>
    /// Shoot the weapon. Do not forget to check <see cref="WeaponEnabled"/>
    /// </summary>
    public virtual void Shoot()
    {
        if (WeaponEnabled)
        {
            CurrentShootCooldownTimer = ShootCooldownTime;
        }
    }
    #endregion
}

public enum WEAPONTYPE
{
    CANNON
}
