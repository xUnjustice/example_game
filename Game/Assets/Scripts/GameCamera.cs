using System.Collections;
using System.Collections.Generic;
using UnityEngine;







public class GameCamera : MonoBehaviour
{
    //public Transform target;
    [SerializeField] GameObject player;
    public float smoothing = 5f;
    Vector3 offset;
    [SerializeField] float sideOffset;
    private bool toggleSideOffset;
    private Vector3 finalOffset;
    private bool Zoomed;
    public Camera CameraObject;

    public GameObject targetObject;
    private Vector3 playerPos;
    private float targetAngle = 0;
    const float rotationAmount = 1.5f;
    public float rDistance = 1.0f;
    public float rSpeed = 1.0f;

    [SerializeField] private GameObject target;
    [SerializeField] private GameObject rotationAnchorObject;
    [SerializeField] private Vector3 tanslationOffset;
    [SerializeField] private Vector3 followOffset;
    [SerializeField] private float maxViewingAngle;
    [SerializeField] private float minViewingAngle;
    [SerializeField] private float rotationSensitivity;

    private float verticalRotationAngle;

    public float duration = 1.0f;
    private float elapsed = 0.0f;
    private float scrollData;
    private float targetZoom;
    private float ZoomFactor = 3f;
    [SerializeField] private float zoomSmoothness=10f;


    [SerializeField] Transform PlayerTransform;

    private Vector3 _cameraOffset;

    [Range(0.01f, 1.0f)]
    [SerializeField] float SmoothFactor = 0.5f;

    [SerializeField] bool LookAtPlayer = false;

    [SerializeField] bool RotateAroundPlayer = true;

    [SerializeField] float RotationsSpeed = 5.0f;

    public Vector3 FollowOffset { get { return followOffset; } }
    // Start is called before the first frame update
    void Start()
    {
        //offset = transform.position - player.transform.position;
        finalOffset = offset;
      //  Zoomed = false;
      //  targetZoom = CameraObject.orthographicSize;
      //  _cameraOffset = transform.position - PlayerTransform.position;

         CameraObject = Camera.main;

        //  toggleSideOffset = true;

    }

    // Update is called once per frame
    void Update()
    {
         scrollData = Input.GetAxis("Mouse ScrollWheel");
      //  targetZoom = targetZoom - scrollData * ZoomFactor;
      //  targetZoom = Mathf.Clamp(targetZoom, 1f, 5f);
      //  CameraObject.orthographicSize = Mathf.Lerp(CameraObject.orthographicSize, targetZoom, Time.deltaTime * zoomSmoothness);


    }

    private void FixedUpdate()
    {

    }
    void LateUpdate()
    {
         transform.position = player.transform.position+ followOffset;  
        Rotate();

    }

    void zoom()
    {
        
    }

protected void Rotate()
{ 
        Quaternion camTurnAngle =
                Quaternion.AngleAxis(Input.GetAxis("Mouse X") * RotationsSpeed, Vector3.up);

            _cameraOffset = camTurnAngle * _cameraOffset;
        

        Vector3 newPos = PlayerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);


        finalOffset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * 4f,Vector3.up)*finalOffset;
        transform.RotateAround(player.transform.position, Vector3.up, rotationAmount);
        transform.LookAt(player.transform);


}

    
}

