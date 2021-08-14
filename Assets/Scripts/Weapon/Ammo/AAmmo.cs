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

    protected virtual void Start()
    {
        if (m_AmmoData == null)
            Debug.LogWarning($"{nameof(m_AmmoData)} is null!", this.gameObject);
        CurrentLifeTime = m_AmmoData.LifeTime;

        m_Ridigbody = GetComponent<Rigidbody>();
    }
    protected void Update()
    {
        //TODO: Lifetime runterzählen.
        CurrentLifeTime -= Time.deltaTime;

        if(CurrentLifeTime <= 0)
            DestroyProjectile();

        Debug.Log(this.gameObject.transform.position, this.gameObject);
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Kollidiert mit: " + collision.gameObject, collision.gameObject);
        DestroyProjectile();
    }

    public virtual void DestroyProjectile()
    {
        Destroy(this.gameObject);
    }
}
