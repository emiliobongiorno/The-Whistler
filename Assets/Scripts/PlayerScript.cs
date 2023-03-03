using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Animator anim;
    public float walkSpeed = 2.0f;
    public float runSpeed = 4.0f;
    private float speed = 2.0f;
    private Rigidbody rb;

    public float range = 100f;
    public Camera fpsCam;

    //Player Look Rotation
    public float mouseSensibility = 1000f;

    public float xRotacion = 0;

    public float yRotacion = 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Debería ser FixedUpdate si se usan fisicas, pero no me permite direccionar al jugador a los costados con el cursor
    void Update() 
    {
       Move();
       MouseLook();
       if (Input.GetMouseButtonDown(0)) 
       {
            PickUpThings();
       }

    }

    void PickUpThings()
    {
       RaycastHit hit;
       Debug.Log("Mouse down");
       if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) {
            if (hit.transform.tag == "Hint") {
                Debug.Log("This is a hint");
            } else {
                Debug.Log("This is " + hit.transform.tag);
            }
       }
    }



    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveInput = new Vector3(horizontal,0,vertical);

        if (moveInput == Vector3.zero)
        {
            anim.SetBool("isWalking", false);
            anim.SetBool("isRunning", false);
        }
        else
        {
            if(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {
                speed = runSpeed;
                anim.SetBool("isRunning", true);
                anim.SetBool("isWalking", false);
            }
            else 
            {
                speed = walkSpeed;
                anim.SetBool("isWalking", true);
                anim.SetBool("isRunning", false);
            }
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * mouseSensibility, 0);
            //transform.rotation = Quaternion.LookRotation(moveInput);
        }

        transform.Translate(moveInput * speed * Time.deltaTime);
        //rb.MovePosition(transform.position + moveInput * speed * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision col) 
    {
        Debug.Log("Collision with " + col.gameObject.name);
        switch(col.gameObject.tag)
        {
        case "Hint":
            
            
            GameManager.instance.FindHint();

            HintScript script = col.gameObject.GetComponent<HintScript>();
            script.TurnOffLight();
            script.DestroyPaper();
            Debug.Log("Hint picked up");
            //Destroy(col.gameObject);
            break;
        case "Whistler":
            Debug.Log("End game: You loose");
            break;
        case "Exit":
            Debug.Log("End game: You win");
            break;
        }

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
