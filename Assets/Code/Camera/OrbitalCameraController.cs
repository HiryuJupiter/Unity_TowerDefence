using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitalCameraController : MonoBehaviour
{
    const float MinAngle = 30;
    const float MaxAngle = 70;
    const float MinDist = 5;
    const float MaxDist = 20;

    [SerializeField] Transform followTarget;
    [SerializeField] float panSensitivity = 0.2f;
    [SerializeField] float tiltSensitivity = 0.2f;
    [SerializeField] float zoomSensitivity = 0.2f;

    new Transform camera;

    float cameraTiltAngle;
    float cameraDistance;

    void Start()
    {
        camera = Camera.main.transform;
        //cameraTiltAngle = camera.eulerAngles.x;
        cameraDistance = (MinDist + MaxDist) / 2f;
        cameraTiltAngle = (MinAngle + MaxAngle) / 2f;
    }

    void Update()
    {
        Orbit(Input.GetAxis("Mouse X"));
        TiltCamera(Input.GetAxis("Mouse Y"));
        Zoom(Input.mouseScrollDelta.y);
    }

    private void Zoom (float zoomAmount)
    {
        cameraDistance = Mathf.Clamp(cameraDistance + zoomAmount * zoomSensitivity, MinDist, MaxDist);
        Vector3 newPosition = camera.localRotation * new Vector3(0f, 0f, -cameraDistance) + followTarget.position; //Executes the camera distance change, and it's position change (based on character's position, making it following the character).
        camera.position = newPosition;
    }

    private void Orbit(float mouseMove)
    {
        camera.Rotate(new Vector3(0f, mouseMove, 0f), Space.World);
    }

    private void TiltCamera (float mouseMove)
    {
        cameraTiltAngle = Mathf.Clamp(cameraTiltAngle + mouseMove * tiltSensitivity, MinAngle, MaxAngle);
        //cameraTiltAngle += mouseMove;
        camera.localRotation = Quaternion.Euler(cameraTiltAngle, 0f, 0f); //Tilt
    }

    //void OnGUI()
    //{
    //    GUI.Label( new Rect(20, 20, 600, 20), "camera.eulerAngles: " + camera.eulerAngles);
    //    GUI.Label( new Rect(20, 40, 600, 20), "cameraTiltAngle: " + cameraTiltAngle);
    //}
}
