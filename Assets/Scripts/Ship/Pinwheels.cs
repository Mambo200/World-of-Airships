using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Die Windenergie wird Aktuell über die Höhe definiert
/// </summary>
public class Pinwheels : MonoBehaviour
{
    [SerializeField]
    private PinewheelData data;

    public PinewheelData Data
    {
        get
        {
            return data;
        }
    }

    private Transform wheelposition;

    public float ProducedEnergie
    {
        get
        {
            return Mapmanager.Instance.GetEnergie(wheelposition.position.y);
        }
    }

    private void Start()
    {
        wheelposition = new GameObject().transform;
        wheelposition.parent = this.transform;
        wheelposition.localPosition = Vector3.up * data.Height;
    }

    //private void Update()
    //{
    //    wheelposition.localPosition = Vector3.up * Height;
    //    Debug.Log(ProducedEnergie);
    //}
}
