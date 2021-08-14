using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/AmmoData")]
public class AmmoData : ScriptableObject
{
    [SerializeField]
    private float m_Damage;
    public float Damage
    {
        get => m_Damage;
    }

    [SerializeField]
    private float m_lifeTime;
    public float LifeTime
    {
        get => m_lifeTime;
    }
}
