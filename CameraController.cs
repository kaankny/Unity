using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CameraController : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] Camera cam;

    [Header("Movement")]
    [SerializeField] float panSpeed;

    [Header("Zoom")]
    [SerializeField] float zoomSpeed;

    [Header("Rotation")]
    [SerializeField] float rotSpeed;
    private Vector2 mouseLook;

    [Header("Float")]
    [SerializeField] float zoomFloat;


    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            transform.position = startPos;
        }
        Rot();
        Zoom();
        Pan();

    }
    void Rot()
    {
        float horizontal = 0f;

        if (Input.GetMouseButton(2))
        {
            horizontal = Input.GetAxis("Mouse X");
        }

        if (Input.GetKey(KeyCode.Q))
        {
            horizontal = -1f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            horizontal = 1f;
        }

        

        Vector2 look = new Vector2(horizontal, 0f);
        mouseLook += look * rotSpeed;
        transform.localRotation = Quaternion.AngleAxis(mouseLook.x, transform.up);
    }

    void Zoom()
    {
        float zoom = Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;
        zoomFloat += zoom;
        if (Mathf.Abs(zoomFloat) < 40)
        {
            cam.transform.position += cam.transform.forward * zoom;
        }
        else
        {
            if(zoomFloat < 0)
            {
                zoomFloat = -40;
            }
            else
            {
                zoomFloat = 40;
            }
        }
        
    }

    void Pan()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");
            float mouseZ = Input.GetAxis("Mouse Y");

            transform.position += transform.forward * -mouseZ * panSpeed * Time.deltaTime;
            transform.position += transform.right * -mouseX * panSpeed * Time.deltaTime;
        }
    }
}
