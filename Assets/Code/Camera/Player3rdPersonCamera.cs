using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-100)]
public class Player3rdPersonCamera : MonoBehaviour
{
    const float MinAngle = 20;
    const float MaxAngle = 60;
    const float MinDist = 5;
    const float MaxDist = 20;

    [SerializeField] Transform followTarget;
    [SerializeField] float panSensitivity = 0.2f;
    [SerializeField] float tiltSensitivity = 0.2f;
    [SerializeField] float zoomSensitivity = 0.2f;


    Transform camTransform;


    public float TiltAngle { get; private set; }
    public float PanAngle { get; private set; }
    public float Distance { get; private set; }
    public Vector3 NonTiltedDirectionTowardsPlayer { get; private set; }
    public Quaternion NonTiltedRotationTowardsPlayer { get; private set; }

    void Start()
    {
        camTransform = Camera.main.transform;
        PanAngle = camTransform.eulerAngles.y;
        Distance = (MinDist + MaxDist) / 2f;
        TiltAngle = (MinAngle + MaxAngle) / 2f;
    }

    void Update()
    {
        Orbit(Input.GetAxis("Mouse X"));
        TiltCamera(Input.GetAxis("Mouse Y"));
        Zoom(-Input.mouseScrollDelta.y);
        CacheNonTiltedRotation();
    }

    private void Orbit(float mouseMove)
    {
        PanAngle += mouseMove;

        //panningPivot.Rotate
        //transform.Rotate(new Vector3(0f, mouseMove, 0f), Space.World);
        //camera.Rotate(new Vector3(0f, mouseMove, 0f), Space.World);
    }

    private void TiltCamera (float mouseMove)
    {
        TiltAngle = Mathf.Clamp(TiltAngle + mouseMove * tiltSensitivity, MinAngle, MaxAngle);
        camTransform.localRotation = Quaternion.Euler(TiltAngle, PanAngle, 0f); //Tilt
    }

    private void Zoom(float zoomAmount)
    {
        Distance = Mathf.Clamp(Distance + zoomAmount * zoomSensitivity, MinDist, MaxDist);
        Vector3 newPosition = camTransform.localRotation * new Vector3(0f, 0f, -Distance) + followTarget.position; //Executes the camera distance change, and it's position change (based on character's position, making it following the character).
        camTransform.position = newPosition;
    }
    //void OnGUI()
    //{
    //    GUI.Label( new Rect(20, 20, 600, 20), "camera.eulerAngles: " + GetComponent<Camera>().eulerAngles);
    //    GUI.Label( new Rect(20, 40, 600, 20), "cameraTiltAngle: " + cameraTiltAngle);
    //}

    private void CacheNonTiltedRotation ()
    {
        Vector3 tgt = followTarget.position - camTransform.position;
        tgt.y = 0f;
        NonTiltedDirectionTowardsPlayer= tgt;

        NonTiltedRotationTowardsPlayer = Quaternion.LookRotation(tgt, Vector3.up);

        //version 1
        //NonTiltedRotationTowardsPlayer = Quaternion.Euler(0f, PanAngle, 0f);

        ////version 2
        //Vector3 tgt = followTarget.position - camTransform.position;
        //tgt.y = 0f;
        //NonTiltedRotation = Quaternion.LookRotation(tgt, Vector3.up);
        //Debug.DrawRay(camTransform.position, tgt, Color.red);
    }
}
