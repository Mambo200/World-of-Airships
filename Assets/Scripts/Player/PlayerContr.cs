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

    [SerializeField]
    private UI_AUnit ingameUI;

    [Header("X Axis Settings")]
    private float cam_X = 0;
    [SerializeField]
    private float cam_X_Speed = 10f;
    [SerializeField]
    private float cam_X_min = 0;
    [SerializeField]
    private float cam_X_max = 1;

    [Header("Y Axis Settings")]
    private float cam_Y = 0;
    [SerializeField]
    private float cam_Y_Speed = 10f;

    [SerializeField]
    private Transform cam_Anchor;

    [Header("Zoom Settings")]
    [SerializeField]
    private Transform cam_zoom_cam;
    [SerializeField]
    private float cam_zoom_min;
    [SerializeField]
    private float cam_zoom_max;
    [SerializeField]
    private float cam_zoom_position;
    [SerializeField]
    private float cam_zoom_speed;

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
        GameManager.Instance.SetIngameMouse();
        cam_zoom_cam.transform.localPosition = Vector3.forward * ((cam_zoom_max - cam_zoom_min) / 2);

        ControlledShip.UI = ingameUI;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = ControlledShip.transform.position;
        transform.eulerAngles = new Vector3(0, ControlledShip.transform.eulerAngles.y,0);

        ControlledShip.MoveShip(Input.GetAxis("Vertical"));
        ControlledShip.RotateShip(Input.GetAxis("Horizontal"));

        cam_X += Input.GetAxis("Mouse Y") * cam_X_Speed * Time.deltaTime;
        cam_Y += Input.GetAxis("Mouse X") * cam_Y_Speed * Time.deltaTime;

        cam_X = Mathf.Clamp(cam_X, cam_X_min, cam_X_max);
        cam_Anchor.localEulerAngles = new Vector3(cam_X, cam_Y, 0);
        ControlledShip.RotateWeapon(cam_X, cam_Y);

        if (Input.GetMouseButtonDown(0))
        {
            ControlledShip.Fire();
        }

        cam_zoom_position += Input.GetAxis("Mouse ScrollWheel") * cam_zoom_speed * Time.deltaTime;
        cam_zoom_position = Mathf.Clamp(cam_zoom_position, cam_zoom_min, cam_zoom_max);
        cam_zoom_cam.transform.localPosition = Vector3.forward * cam_zoom_position;

        if (Input.GetKeyDown(KeyCode.R))
        {
            ControlledShip.AddDamage(15f);
        }
    }
}
