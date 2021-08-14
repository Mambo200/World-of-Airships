using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engine : MonoBehaviour
{
    [SerializeField]
    private EngineData data;
    public EngineData Data
    {
        get
        {
            return data;
        }
    }
    private AShip contrShip;

    [HideInInspector]
    public float CurrentThrust;
    [HideInInspector]
    public float CurrentTurn;

    [SerializeField]
    private LayerMask IgnoreMask;

    [SerializeField]
    private GameObject[] hoverPoints;
    private GameObject currentHoverPoint;

    public void Init(AShip _controllShip)
    {
        contrShip = _controllShip;
        IgnoreMask = ~IgnoreMask;
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        for (int x = 0; x < hoverPoints.Length; x++)
        {
            currentHoverPoint = hoverPoints[x];
            if (Physics.Raycast(currentHoverPoint.transform.position,
                -Vector3.up, out hit,
                data.HoverHeight,
                IgnoreMask))
            {
                contrShip.Rigid.AddForceAtPosition(Vector3.up
                    * data.HoverForce
                    * (1f - (hit.distance / data.HoverHeight)),
                    currentHoverPoint.transform.position);
            }
            else
            {
                if (contrShip.transform.position.y > currentHoverPoint.transform.position.y)
                {
                    contrShip.Rigid.AddForceAtPosition(currentHoverPoint.transform.up
                        * data.HoverForce,
                        currentHoverPoint.transform.position);
                }
                else
                {
                    contrShip.Rigid.AddForceAtPosition(currentHoverPoint.transform.up
                        * -data.HoverForce,
                        currentHoverPoint.transform.position);
                }
            }
        }

        if (Mathf.Abs(CurrentThrust) > 0)
        {
            contrShip.Rigid.AddForce(contrShip.transform.forward * CurrentThrust);
        }

        if (CurrentTurn > 0)
        {
            contrShip.Rigid.AddRelativeTorque(Vector3.up * CurrentTurn * data.TurnStrength);
        }
        else if (CurrentTurn < 0)
        {
            contrShip.Rigid.AddRelativeTorque(Vector3.up * CurrentTurn * data.TurnStrength);
        }
    }
}
