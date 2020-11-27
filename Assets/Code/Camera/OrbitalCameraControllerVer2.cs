using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalCameraControllerVer2 : MonoBehaviour
{
    [SerializeField] float panSensitivity = 0.2f;
    [SerializeField] float tiltSensitivity = 0.2f;

    float tiltAngle;

    void Start()
    {
        tiltAngle = transform.eulerAngles.x;
    }

    void Update()
    {
        float y = Input.GetAxis("Mouse X");
        float x = Input.GetAxis("Mouse Y");

        Orbit(y);
        TiltCamera(-x);
    }

    public void Orbit(float mouseMove)
    {
        transform.Rotate(new Vector3(0f, mouseMove, 0f), Space.World);
    }

    void TiltCamera (float mouseMove)
    {
        tiltAngle = Mathf.Clamp(tiltAngle + mouseMove * tiltSensitivity, 30, 70);
        transform.localRotation = Quaternion.Euler(tiltAngle, 0f, 0f); //Tilt
    }

    void OnGUI()
    {
        GUI.Label( new Rect(20, 20, 600, 20), "camera.eulerAngles: " + camera.eulerAngles);
        GUI.Label( new Rect(20, 40, 600, 20), "cameraTiltAngle: " + cameraTiltAngle);
    }
}
