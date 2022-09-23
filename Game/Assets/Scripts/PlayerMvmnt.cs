using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rotation();


    }
     void Rotation()
    {
        transform.Rotate(new Vector3(
            0, Input.GetAxis("Mouse X") * 4f, 0
            ));
    }
}
