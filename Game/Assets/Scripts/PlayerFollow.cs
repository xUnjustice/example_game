using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerFollow : MonoBehaviour {

    public Transform PlayerTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    [SerializeField] float SmoothFactor = 0.5f;

    [SerializeField] bool LookAtPlayer = false;

    [SerializeField] bool RotateAroundPlayer = true;

    [SerializeField] float RotationsSpeed = 5.0f;



    [SerializeField] private float maxViewingAngle;
    [SerializeField] private float minViewingAngle;
    public Camera CameraObject;

    private float scrollData;
    private float targetZoom;
    private float ZoomFactor = 3f;
    [SerializeField] private float zoomSmoothness = 10f;

    private Quaternion Cameararotation;
    // Use this for initialization
    void Start () {
        _cameraOffset = transform.position - PlayerTransform.position;
        Cameararotation = transform.rotation;
        targetZoom = CameraObject.orthographicSize;

    }
    private void Update()
    {
        Cameararotation.y += Input.GetAxis("Mouse X") * RotationsSpeed;
        
        scrollData = Input.GetAxis("Mouse ScrollWheel");

        targetZoom = targetZoom - scrollData * ZoomFactor;
        targetZoom = Mathf.Clamp(targetZoom, 1f, 5f);
        CameraObject.orthographicSize = Mathf.Lerp(CameraObject.orthographicSize, targetZoom, Time.deltaTime * zoomSmoothness);
    }

    void FixedUpdate () {

       
       
        if (Input.GetMouseButton(1))
        {
            Quaternion camTurnAngle =
                Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, Vector3.up);

            
            Quaternion camUpDown =
                 Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * RotationsSpeed, transform.right);
            var value = camUpDown * camTurnAngle * _cameraOffset;

            if (value.y > minViewingAngle && value.y < maxViewingAngle)
            {
                _cameraOffset = camUpDown * camTurnAngle * _cameraOffset;
            }
            else {
                _cameraOffset = camTurnAngle * _cameraOffset;


            }

        }
       // else { }
        Vector3 newPos = PlayerTransform.position + _cameraOffset;
        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
         if (LookAtPlayer || RotateAroundPlayer)
            transform.LookAt(PlayerTransform);
	}
}
