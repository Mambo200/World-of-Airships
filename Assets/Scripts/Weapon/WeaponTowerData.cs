using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponTowerData")]
public class WeaponTowerData : ScriptableObject
{
    [SerializeField]
    private Vector2 m_RangeX;
    public Vector2 RangeX
    {
        get
        {
            return m_RangeX;
        }
    }

    [SerializeField]
    private Vector2 m_RangeY;
    public Vector2 RangeY
    {
        get
        {
            return m_RangeY;
        }
    }



    [SerializeField]
    private int m_WeaponCount;
    public int WeaponCount
    {
        get { return m_WeaponCount; }
    }

}
