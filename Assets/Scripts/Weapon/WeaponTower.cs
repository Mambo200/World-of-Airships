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
        get => AmountAttachedWeapons == m_WeaponTowerData.WeaponCount;
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

    // Start is called before the first frame update
    void Start()
    {
        if (m_WeaponTowerData == null)
            Debug.LogWarning("m_WeaponTowerData is null");

        AttachedWeapons = new AWeapon[m_WeaponTowerData.WeaponCount];
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
        else if(Input.GetKey(KeyCode.D))
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

    }

    /// <summary>
    /// Place Weapon
    /// </summary>
    /// <param name="_weapon"></param>
    /// <returns></returns>
    public bool PlaceWeapon(AWeapon _weapon)
    {
        if (IsTowerFull)
        {
            Debug.LogWarning("You tried to add a Weapon to this Tower, but it already has a weapon attached", this.gameObject);
            return false;
        }
        // get free Index
        int towerIndex = GetFirstFreeIndex();
        if(towerIndex < 0)
        {
            Debug.Log("TowerIndex was -1, but it seems like tower is not full. Please check!", this.gameObject);
            return false;
        }

        m_AmountAttachedWeapons++;

        //TODO: Create Weapon in Tower?
        AttachedWeapons[towerIndex] = _weapon;
        return true;
    }

    /// <summary>
    /// Remove weapon
    /// </summary>
    /// <param name="_index">weapon to remove</param>
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
        AttachedWeapons[index] = null;

        return true;
    }

    /// <summary>
    /// Remove weapon at index
    /// </summary>
    /// <param name="_index">index</param>
    /// <returns>true if weapon was removed; else false</returns>
    public bool RemoveWeapon(int _index)
    {
        return false;
    }
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
