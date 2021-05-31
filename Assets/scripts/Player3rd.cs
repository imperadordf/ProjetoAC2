using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player3rd : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // animator.SetBool();
        }
    }
}
