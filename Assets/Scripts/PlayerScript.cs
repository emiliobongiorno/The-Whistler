using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public Animator anim;
    public float moveSpeed = 1.0f;

    void Start()
    {
    }

    void Update()
    {
       Move();
    }


    void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveInput = new Vector3(horizontal,0,vertical);

        if (moveInput == Vector3.zero)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * 10, 0);
            //transform.rotation = Quaternion.LookRotation(moveInput);
        }

        transform.Translate(moveInput * moveSpeed * Time.deltaTime);

        
        //rb.AddForce(moveInput * moveSpeed * Time.deltaTime);
    }
}
