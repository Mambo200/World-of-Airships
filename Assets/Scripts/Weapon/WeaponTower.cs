using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTower : MonoBehaviour
{
    [SerializeField]
    private Transform m_RotationTransformX;
    [SerializeField]
    private Transform m_RotationTransformY;
    [Space]
    [SerializeField]
    private WeaponTowerData m_WeaponTowerData;

    [SerializeField]
    private Transform[] m_WeaponSpawnTransform;
    [SerializeField]
    private WeaponPrefabsData m_WeaponPrefabsData;

    /// <summary>
    /// Placed weapon on this tower
    /// </summary>
    public AWeapon[] AttachedWeapons { get; private set; }

    private int m_AmountAttachedWeapons;
    public int AmountAttachedWeapons
    {
        get => m_AmountAttachedWeapons;
    }
    public bool IsTowerFull
    {
        get => AmountAttachedWeapons == AttachedWeapons.Length;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (m_WeaponTowerData == null)
            Debug.LogWarning("m_WeaponTowerData is null");

        if (m_WeaponSpawnTransform == null)
            Debug.LogWarning($"{nameof(m_WeaponSpawnTransform)} is null.", this.gameObject);

        for (int i = 0; i < m_WeaponSpawnTransform.Length; i++)
        {
            if(m_WeaponSpawnTransform[i] == null)
                Debug.LogWarning($"{nameof(m_WeaponSpawnTransform)} at index {i.ToString()} is null.", this.gameObject);
        }

        AttachedWeapons = new AWeapon[m_WeaponSpawnTransform.Length];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 yRotation = m_RotationTransformY.eulerAngles;
        float rotationAdd = Time.deltaTime * 30f;
        if (Input.GetKey(KeyCode.A))
            //left
            RotateY(
                yRotation.y - rotationAdd
            );
        else if (Input.GetKey(KeyCode.D))
            //right
            RotateY(
                yRotation.y + rotationAdd
            );
        rotationAdd = Time.deltaTime * 15;
        if (Input.GetKey(KeyCode.W))
            //up
            RotateX(
                m_RotationTransformX.eulerAngles.x - rotationAdd
            );
        else if (Input.GetKey(KeyCode.S))
            //up
            RotateX(
                m_RotationTransformX.eulerAngles.x + rotationAdd
            );

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            //add
            PlaceWeapon(WEAPONTYPE.CANNON);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            //remove
            RemoveLatestWeapon(out _);
        }
    }


    /// <summary>
    /// Check if weapon is placed at index
    /// </summary>
    /// <param name="_index">index</param>
    /// <returns>true if at that index a weapon is placed; else false</returns>
    public bool IsWeaponPlaced(int _index)
    {
        return AttachedWeapons[_index] == null;
    }

    /// <summary>
    /// Check if any weapon is placed on this tower
    /// </summary>
    /// <returns>true if no weapon is on this tower; else false</returns>
    public bool IsAnyWeaponPlaced()
    {
        for (int i = 0; i < AttachedWeapons.Length; i++)
        {
            if (AttachedWeapons[i] != null)
                return true;
        }

        return false;
    }

    #region Place, create and remove weapon
    /// <summary>
    /// Place Weapon
    /// </summary>
    /// <param name="_weapon">weapon to place</param>
    /// <returns></returns>
    [System.Obsolete("This Method does not work properly. Please use () instead.", true)]
    public int PlaceWeapon(AWeapon _weapon)
    {
        if (IsTowerFull)
        {
            Debug.LogWarning("You tried to add a Weapon to this Tower, but it already has a weapon attached", this.gameObject);
            return -1;
        }
        // get free Index
        int towerIndex = GetFirstFreeIndex();
        if(towerIndex < 0)
        {
            Debug.Log("TowerIndex was -1, but it seems like tower is not full. Please check!", this.gameObject);
            return -1;
        }

        m_AmountAttachedWeapons++;

        AttachedWeapons[towerIndex] = _weapon;
        return towerIndex;
    }

    /// <summary>
    /// Create and place weapon in <see cref="AttachedWeapons"/>.
    /// </summary>
    /// <param name="_type">weapon type</param>
    /// <returns>index of weapon in <see cref="AttachedWeapons"/>. If failed to add, return -1.</returns>
    public int PlaceWeapon(WEAPONTYPE _type)
    {
        int index = GetFirstFreeIndex();

        if (index < 0)
            return -1;

        AWeapon w = CreateWeapon(_type, index);

        AttachedWeapons[index] = w;

        m_AmountAttachedWeapons++;
        return index;
    }

    /// <summary>
    /// Instatciate weapon
    /// </summary>
    /// <param name="_type">weapon type</param>
    /// <param name="_index">spawn index for <see cref="m_WeaponSpawnTransform"/></param>
    /// <returns><see cref="AWeapon"/> component of spawned gameObject</returns>
    private AWeapon CreateWeapon(WEAPONTYPE _type, int _index)
    {
        GameObject go = GameObject.Instantiate(m_WeaponPrefabsData.GameObjectCannonPrefab, m_WeaponSpawnTransform[_index]);
        AWeapon to = go.GetComponent<AWeapon>();

        return to;
    }

    /// <summary>
    /// Remove and destroy weapon
    /// </summary>
    /// <param name="_weapon">weapon to remove</param>
    /// <returns>true if weapon was removed; else false</returns>
    public bool RemoveWeapon(AWeapon _weapon)
    {
        int index = 1;
        for (int i = 0; i < AttachedWeapons.Length; i++)
        {
            if (AttachedWeapons[i] == _weapon)
            {
                index = i;
                break;
            }
        }

        if(index < 0)
            return false;

        // remove
        GameObject.Destroy(AttachedWeapons[index].gameObject);
        AttachedWeapons[index] = null;

        m_AmountAttachedWeapons--;
        return true;
    }

    /// <summary>
    /// Remove and destroy weapon
    /// </summary>
    /// <param name="_index">weapon to remove</param>
    /// <returns>true if weapon was removed; else false</returns>
    public bool RemoveWeapon(int _index)
    {
        if (_index < 0)
            return false;

        // remove
        GameObject.Destroy(AttachedWeapons[_index].gameObject);
        AttachedWeapons[_index] = null;

        m_AmountAttachedWeapons--;
        return true;
    }

    /// <summary>
    /// Remove latest weapon
    /// </summary>
    /// <returns>true if weapon was removed; else false</returns>
    public bool RemoveLatestWeapon(out int _removedIndex)
    {
        _removedIndex = -1;
        for (int i = AttachedWeapons.Length - 1; i >= 0; i--)
        {
            if(AttachedWeapons[i] != null)
            {
                _removedIndex = i;
                break;
            }
        }

        if (_removedIndex < 0)
            return false;
        return RemoveWeapon(_removedIndex);
    }

    /// <summary>
    /// Remove latest weapon
    /// </summary>
    /// <returns>true if weapon was removed; else false</returns>
    public bool RemoveFirstWeapon(out int _removedIndex)
    {
        _removedIndex = -1;
        for (int i = 0; i < AttachedWeapons.Length; i++)
        {
            if (AttachedWeapons[i] != null)
            {
                _removedIndex = i;
                break;
            }

        }

        if (_removedIndex < 0)
            return false;
        return RemoveWeapon(_removedIndex);
    }
    #endregion

    /// <summary>
    /// Get first index with null in <<see cref="AttachedWeapons"/>.
    /// </summary>
    /// <returns>first index with no weapon. If full return -1</returns>
    public int GetFirstFreeIndex()
    {
        for (int i = 0; i < AttachedWeapons.Length; i++)
        {
            if (AttachedWeapons[i] == null)
                return i;
        }

        return -1;
    }

    /// <summary>
    /// Rotate Tower in x rotation
    /// </summary>
    /// <param name="_newValue">new absolute rotation</param>
    public void RotateX(float _newValue)
    {
        _newValue = ConvertIn180Space(_newValue);
        _newValue = CheckMinMaxRotation(_newValue, m_WeaponTowerData.RangeX);
        
        Vector3 finalRotationX = new Vector3(_newValue, m_RotationTransformX.eulerAngles.y, m_RotationTransformX.eulerAngles.z);
        m_RotationTransformX.rotation = Quaternion.Euler(finalRotationX);
    }

    /// <summary>
    /// Rotate Tower in y Rotation
    /// </summary>
    /// <param name="_newValue">new absolute rotation</param>
    public void RotateY(float _newValue)
    {
        _newValue = ConvertIn180Space(_newValue);
        _newValue = CheckMinMaxRotation(_newValue, m_WeaponTowerData.RangeY);

        Vector3 finalRotationY = new Vector3(m_RotationTransformY.rotation.eulerAngles.x, _newValue, m_RotationTransformY.rotation.eulerAngles.z);
        m_RotationTransformY.rotation = Quaternion.Euler(finalRotationY);
    }

    private float ConvertIn180Space(float _value)
    {
        if (_value > 180)
        {
            _value = (360 - _value) * -1;
        }

        return _value;
    }

    private Vector2 ConvertIn180Space(Vector2 _value)
    {
        return new Vector2(
            ConvertIn180Space(_value.x),
            ConvertIn180Space(_value.y)
            );
    }

    private Vector3 ConvertIn180Space(Vector3 _value)
    {
        return new Vector3(
            ConvertIn180Space(_value.x),
            ConvertIn180Space(_value.y),
            ConvertIn180Space(_value.z)
            );
    }

    private float CheckMinMaxRotation(float _currentRotation, Vector2 _minMaxRotation)
    {
        return Mathf.Min(Mathf.Max(_currentRotation, _minMaxRotation.x), _minMaxRotation.y);
    }
}
