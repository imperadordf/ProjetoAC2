using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Parasite : MonoBehaviour
{
    NavMeshAgent agent;
    Animator animator;
    GameObject player;

    public Transform[] targets;
    int index;
    float distanceP, distanceT;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<CharacterMovement>().gameObject;
    }

    private void Update()
    {
        distanceP = Vector3.Distance(player.transform.position, transform.position);
        if (distanceP < 10)
        {
            agent.SetDestination(player.transform.position);
        }
        else
        {
            agent.SetDestination(targets[index].position);
        }

        distanceT = Vector3.Distance(targets[index].position, transform.position);
        if (distanceT<1)
        {
            index++;
        }
        if (index >= targets.Length)
        {
            index = 0;
        }
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }
}
