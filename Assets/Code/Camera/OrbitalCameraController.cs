using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalCameraController : MonoBehaviour
{
    [SerializeField] float panSensitivity = 0.2f;
    [SerializeField] float tiltSensitivity = 0.2f;

    new Transform camera;

    float cameraTiltAngle;

    void Start()
    {
        camera = Camera.main.transform;
        cameraTiltAngle = camera.eulerAngles.x;
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
        cameraTiltAngle = Mathf.Clamp(cameraTiltAngle + mouseMove * tiltSensitivity, 30, 70);
        //cameraTiltAngle += mouseMove;
        camera.localRotation = Quaternion.Euler(cameraTiltAngle, 0f, 0f); //Tilt
    }

    //void OnGUI()
    //{
    //    GUI.Label( new Rect(20, 20, 600, 20), "camera.eulerAngles: " + camera.eulerAngles);
    //    GUI.Label( new Rect(20, 40, 600, 20), "cameraTiltAngle: " + cameraTiltAngle);
    //}
}
