using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContr : MonoBehaviour
{
    public AShip ControlledShip;

    private float cam_X = 0;
    [SerializeField]
    private float cam_X_Speed = 10f;
    [SerializeField]
    private float cam_X_min = 0;
    [SerializeField]
    private float cam_X_max = 1;


    private float cam_Y = 0;
    [SerializeField]
    private float cam_Y_Speed = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ControlledShip.MoveShip(Input.GetAxis("Vertical"));
        ControlledShip.RotateShip(Input.GetAxis("Horizontal"));

        cam_X += Input.GetAxis("Mouse Y") * cam_X_Speed;
        cam_Y += Input.GetAxis("Mouse X") * cam_Y_Speed;
        ControlledShip.RotateWeapon(cam_X, cam_Y);

        if (Input.GetMouseButtonDown(0))
        {
            ControlledShip.Fire();
        }
    }
}
