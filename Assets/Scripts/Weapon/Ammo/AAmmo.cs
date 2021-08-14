using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public abstract class AAmmo : MonoBehaviour
{
    private Rigidbody m_Ridigbody;
    public Rigidbody GetRigidbody
    {
        get => m_Ridigbody;
    }

    [SerializeField]
    private AmmoData m_AmmoData;

    private float CurrentLifeTime { get; set; }

    protected virtual void Awake()
    {
        m_Ridigbody = GetComponent<Rigidbody>();
    }

    protected virtual void Start()
    {
        if (m_AmmoData == null)
            Debug.LogWarning($"{nameof(m_AmmoData)} is null!", this.gameObject);
        CurrentLifeTime = m_AmmoData.LifeTime;

    }
    protected void Update()
    {
        CurrentLifeTime -= Time.deltaTime;

        if(CurrentLifeTime <= 0)
        {
            Debug.Log($"{this} destroyes itself after {m_AmmoData.LifeTime} seconds.");
            DestroyProjectile();
        }
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        AShip ship = GetShip(collision.gameObject);
        if(ship == null)
        {
            Debug.LogWarning($"{collision.gameObject} does not have a {nameof(AShip)}-Component.", collision.gameObject);
            return;
        }
        ship.AddDamage(m_AmmoData.Damage);
        DestroyProjectile();
    }

    public virtual void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }

    protected AShip GetShip(GameObject _hit)
    {

        GameObject go = _hit;

        while(go != null)
        {
            AShip tr = go.GetComponent<AShip>();
            if (tr != null)
                return tr;
            else
                go = go.GetComponentInParent<GameObject>();
        }

        return null;
    }
}
