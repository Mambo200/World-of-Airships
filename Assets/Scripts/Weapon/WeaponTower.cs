using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponTower : MonoBehaviour
{
    [SerializeField]
    private Transform m_RotationTransformX;
    [SerializeField]
    private Vector2 m_RangeX;
    [Space]
    [SerializeField]
    private Transform m_RotationTransformY;
    [SerializeField]
    private Vector2 m_RangeY;

    /// <summary>
    /// Placed weapon on this tower
    /// </summary>
    public AWeapon AttachedWeapon { get; private set; }

    /// <summary>
    /// Is tower empty or is weapon placed on it
    /// </summary>
    public bool IsWeaponPlaced
    {
        get => AttachedWeapon != null;
    }

    // Start is called before the first frame update
    void Start()
    {

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

    public bool PlaceWeapon(AWeapon _weapon)
    {
        if (IsWeaponPlaced)
        {
            Debug.LogWarning("You tried to add a Weapon to this Tower, but it already has a weapon attached", this.gameObject);
            return false;
        }

        //TODO: Create Weapon in Tower?
        AttachedWeapon = _weapon;
        return true;
    }

    public void RotateX(float _newValue)
    {
        _newValue = ConvertIn180Space(_newValue);
        _newValue = CheckMinMaxRotation(_newValue, m_RangeX);
        
        Vector3 finalRotationX = new Vector3(_newValue, m_RotationTransformX.eulerAngles.y, m_RotationTransformX.eulerAngles.z);
        m_RotationTransformX.rotation = Quaternion.Euler(finalRotationX);
    }
    public void RotateY(float _newValue)
    {
        _newValue = ConvertIn180Space(_newValue);
        _newValue = CheckMinMaxRotation(_newValue, m_RangeY);

        Vector3 finalRotationY = new Vector3(m_RotationTransformY.rotation.eulerAngles.x, _newValue, m_RotationTransformY.rotation.eulerAngles.z);
        Debug.Log($"Current: {m_RotationTransformY.rotation.eulerAngles} | After: {finalRotationY}");
        //m_RotationTransformY.Rotate(finalRotationY, Space.Self);
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
