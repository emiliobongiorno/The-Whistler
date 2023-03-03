using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    //Player Look Rotation
    public float mouseSensibility = 1000f;

    public float xRotacion = 0;

    public float yRotacion = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();
    }

    void MouseLook()

    {

        float mouseX = Input.GetAxis("Mouse X") * mouseSensibility * Time.deltaTime;

        float mouseY = Input.GetAxis("Mouse Y") * mouseSensibility * Time.deltaTime;

        xRotacion -= mouseY;

        xRotacion = Mathf.Clamp(xRotacion, -70,70);

        yRotacion += mouseX;//esto es asi sino funciona al revez

        //yRotacion = Mathf.Clamp(yRotacion,-360,360);

        transform.localRotation= Quaternion.Euler(xRotacion,yRotacion,0);

    }
}
