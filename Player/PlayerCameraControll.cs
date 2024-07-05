using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraControll : MonoBehaviour
{
    public float verticalSensivity;
    public float horizontalSensivity;
    public float verticalLimit;
    public Transform Player;

    float xRotation = 0f;

    float mouseX, mouseY;
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        RayCasting();

        mouseX = Input.GetAxis("Mouse X") * horizontalSensivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * verticalSensivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLimit, verticalLimit);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        Player.Rotate(Vector3.up * mouseX);
    }

    public Ray ray;
    public Ray RayCasting()
    {
        Vector3 RayPos = new Vector3(Screen.width / 2, Screen.height / 2, 0);
        ray = Camera.main.ScreenPointToRay(RayPos);
        Debug.DrawRay(transform.position, ray.direction * 10, Color.red);
        return ray;
    }
}
