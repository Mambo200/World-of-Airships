using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerContr : MonoBehaviour
{
    public static PlayerContr m_instance;
    public static PlayerContr Get
    {
        get => m_instance;
    }

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

    private void Awake()
    {
        if (m_instance == null)
            m_instance = this;
        else
            Debug.LogWarning($"There are two instances of {nameof(PlayerContr)}. This instance was not applied!", this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ControlledShip.transform.position;

        ControlledShip.MoveShip(Input.GetAxis("Vertical"));
        ControlledShip.RotateShip(Input.GetAxis("Horizontal"));

        cam_X += Input.GetAxis("Mouse Y") * cam_X_Speed * Time.deltaTime;
        cam_Y -= Input.GetAxis("Mouse X") * cam_Y_Speed * Time.deltaTime;
        transform.eulerAngles = new Vector3(
            Mathf.Clamp(cam_X, cam_X_min, cam_X_max)
            ,cam_Y,0);
        ControlledShip.RotateWeapon(cam_X, cam_Y);

        if (Input.GetMouseButtonDown(0))
        {
            ControlledShip.Fire();
        }
    }
}
