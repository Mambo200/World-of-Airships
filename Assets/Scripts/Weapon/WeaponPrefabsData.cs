using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/WeaponPrefabsData")]
public class WeaponPrefabsData : ScriptableObject
{
    [SerializeField]
    private GameObject m_CannonPrefab;
    public GameObject GameObjectCannonPrefab
    {
        get => m_CannonPrefab;
    }
}
