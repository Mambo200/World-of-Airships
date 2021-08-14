using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    [Tooltip("Ship to Control")]
    [SerializeField]
    private AShip m_Ship;

    [Range(-1, 1)]
    [SerializeField]
    public float m_RotateDirection;

    // Start is called before the first frame update
    void Start()
    {
        if (m_Ship == null)
            Debug.LogError($"{nameof(m_Ship)} is null!");

        //m_Ship.RotateShip(100);
    }

    // Update is called once per frame
    void Update()
    {
        bool reachedTarget = DoMovement();
        //DoRotation();

        if (reachedTarget)
        {
            DoShooting();
        }
            
    }

    private bool DoMovement()
    {
        return m_Ship.MoveToPosition(GetPlayer().ControlledShip.gameObject.transform.position);
    }

    private void DoRotation()
    {
        Transform LookAtEnemy = PlayerContr.Get.ControlledShip.gameObject.transform;  // Who is the enemy?

        //  If you find the enemy, do this:
        var rotate = Quaternion.LookRotation(LookAtEnemy.position - m_Ship.transform.position);  //  This is the amount to rotate
        float rotationLeft = rotate.eulerAngles.y - m_Ship.transform.rotation.eulerAngles.y;

        if (rotationLeft <= 5
            && rotationLeft >= -5)
            m_Ship.RotateShip(0);
        else
        {
            m_Ship.RotateShip(Mathf.Clamp(rotationLeft, -1, 1));
        }

    }

    private void DoShooting()
    {
        //TODO: Shoot
    }


    private PlayerContr GetPlayer()
    {
        return PlayerContr.Get;
    }
}
