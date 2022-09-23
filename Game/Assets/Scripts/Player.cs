using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 _cameraOffset;
    [SerializeField] float SmoothFactor = 0.5f;
    [SerializeField] float RotationsSpeed = 5.0f;
    public Vector2 turn;
    Animator m_Animator;
    float m_TurnAmount;

    [Header("Interaction")]
    [SerializeField] private GameObject targetPoint;
    [SerializeField] private KeyCode interactionKey;
    [SerializeField] private float interactionDistance;

    [Header("Gameplay")]
    [SerializeField] private KeyCode toolSwitchKey;
    [SerializeField] private int initialResourceCount;
    [SerializeField] private float resourceCollectionCooldown;

    [Header("Interface")]
    [SerializeField] private HudController hud;

    private int resources = 0;
    private float resourceCollectionCooldownTimer = 0;
    private ResourcesObject resourceObject;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        resources = initialResourceCount;
        hud.Resources = 0;
        cam = Camera.main;

    }

    // Update is called once per frame
    void Update()
    {
       /*  // Converting the mouse position to a point in 3D-space
         Vector3 point = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1));
         // Using some math to calculate the point of intersection between the line going through the camera and the mouse position with the XZ-Plane
         float t = cam.transform.position.y / (cam.transform.position.y - point.y);
         Vector3 finalPoint = new Vector3(t * (point.x - cam.transform.position.x) + cam.transform.position.x, 1, t * (point.z - cam.transform.position.z) + cam.transform.position.z);
         //Rotating the object to that point
         transform.LookAt(finalPoint, Vector3.up);
        Debug.Log(finalPoint);
        */
      //  transform.LookAt(GetAimPos());
        resourceCollectionCooldownTimer -= Time.deltaTime;
#if UNITY_EDITOR
        // Draw interaction line.
        Debug.DrawLine(targetPoint.transform.position, targetPoint.transform.position + targetPoint.transform.forward * interactionDistance, Color.green);
#endif
       
        if (Input.GetAxis("Fire1") > 0.1f)
        {
            UseToolContinuous();
        }
        


        //Get the Screen positions of the object
        Vector2 positionOnScreen = Camera.main.WorldToViewportPoint(transform.position);

        //Get the Screen position of the mouse
        Vector2 mouseOnScreen = (Vector2)Camera.main.ScreenToViewportPoint(Input.mousePosition);

        //Get the angle between the points
      //  float angle = AngleBetweenTwoPoints(positionOnScreen, mouseOnScreen);

        //Ta Daaa
        //  transform.rotation = Quaternion.Euler(new Vector3( 0, angle,0));

        // m_Animator.SetFloat("Turn", angle, 0.3f, Time.deltaTime);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 5.23f;

        Vector3 objectPos = UnityEngine.Camera.main.WorldToScreenPoint(transform.position);

        mousePos.x = mousePos.x - objectPos.x;
        mousePos.y = mousePos.y - objectPos.y;

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
      //  transform.rotation = Quaternion.Euler(new Vector3(0, 130-angle , 0));


    }


    float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(b.x - a.x, b.y - a.y) * Mathf.Rad2Deg;
    }
    public static Vector3 GetAimPos()
    {
        Physics.Raycast(
        Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit);
        return hit.point;
    }
    void LateUpdate()
    {
      //  m_Animator.SetBool("mine", true);


        m_TurnAmount = Input.GetAxis("Mouse X") * RotationsSpeed;
        transform.Rotate(0, Input.GetAxis("Mouse X") * RotationsSpeed, 0) ;
        m_Animator.SetFloat("Turn", m_TurnAmount, 0.3f, Time.deltaTime);
        


    }
    private void UseToolContinuous()
    {
      
        RaycastHit hit;
            if (Physics.Raycast(targetPoint.transform.position, targetPoint.transform.forward, out hit, interactionDistance))
            {
            //Debug.Log(hit.transform.GetComponent<ResourcesObject>());
                if (resourceCollectionCooldownTimer <= 0 && hit.transform.GetComponent<ResourcesObject>() != null)
            {
                Debug.Log(hit.transform.GetComponent<ResourcesObject>());

                m_Animator.Play("Mining", 0, 0);
                resourceCollectionCooldownTimer = resourceCollectionCooldown;

                    resourceObject = hit.transform.GetComponent<ResourcesObject>();
                   // int collectedResources = collect();

                    //resources += collectedResources;
                   //
                }
            }
        
    }
   void collect()
    {
         
        int collectedResources = resourceObject.Collect();

        resources += collectedResources;
        hud.Resources = resources;
    }


}
