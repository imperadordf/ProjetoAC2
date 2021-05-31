using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;
    public Rigidbody rb;
    public bool isOnTheGround = true;

    public Transform isFloorTarget;

    private Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator=GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetButtonDown("Jump") && isOnTheGround)
        {
            animator.SetTrigger("Jump");
           rb.AddForce(new Vector3(0, jumpForce, 0), ForceMode.Impulse);
            isOnTheGround = false;
        }

        RaycastHit hit;

        if (Physics.Raycast(isFloorTarget.position, isFloorTarget.forward * -1, out hit, 10))
        {
            isOnTheGround = hit.collider.CompareTag("Floor");
        }
        else{
            isOnTheGround = false;
        }

        animator.SetBool("IsFloor",isOnTheGround);

    }


    private void OnCollisionEnter(Collision collision)

    {
        if (collision.gameObject.name == "Floor")
        {
            isOnTheGround = true;
        }
    }


}





