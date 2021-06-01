using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;


public class PlayerPointClick : MonoBehaviour
{
    public float maxSpeed;
    public float minSpeed;

    public GameObject vfxPoint;

    public AnimationClip clipAnimationGetItem;

    public AnimationClip clipAnimationButton;

    private bool pegouItem;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        agent.speed = maxSpeed;
    }

    private void Update()
    {
        if (AnimationOn())
            return;

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 500))
            {

                switch (hit.collider.tag)
                {
                    case "Floor":
                        SetDestination(hit);
                        GameObject gameObject = Instantiate(vfxPoint, hit.point, Quaternion.identity);
                        Destroy(gameObject, 1);
                        StopAllCoroutines();
                        SetDestination(hit);
                        operationIA = false;
                        break;
                    case "Door":
                        Vector3 destino = hit.collider.GetComponent<Door>().DestinoDoor().position;
                        operationIA = true;
                        if (destino != null)
                            agent.destination = destino;
                        break;
                    case "Item":
                        if (!operationIA)
                        {
                            SetDestination(hit);
                            collectableItem = hit.collider.GetComponent<I_Collectable>();
                            StartCoroutine(ItemDistance(hit.collider.transform.position, PegarItem, 0.5f));
                            operationIA = true;
                        }
                        break;
                    case "Loading":
                        if (!operationIA && pegouItem)
                        {
                            Vector3 destinoBotao = hit.collider.GetComponent<LoadingScene>().targetDirection.position;
                            agent.destination = destinoBotao;
                            StartCoroutine(ItemDistance(hit.collider.transform.position, FinishMapLevel1, 3.2f));
                            operationIA = true;
                        }
                        break;

                }
            }
        }

        if (Input.GetKey(KeyCode.LeftShift))
        {
            agent.speed = minSpeed;
        }
        else
        {
            agent.speed = maxSpeed;
        }

        animator.SetFloat("Speed", agent.velocity.magnitude);
        Debug.DrawRay(Camera.main.ScreenPointToRay(Input.mousePosition).origin, Camera.main.ScreenPointToRay(Input.mousePosition).direction * 100, Color.grey);
    }

    private void SetDestination(RaycastHit hit)
    {
        destination = hit.point;
        agent.destination = hit.point;
    }

    private bool AnimationOn()
    {
        bool isGettingItem = animator.GetCurrentAnimatorStateInfo(0).IsName("GetItem");
        return isGettingItem;
    }


    I_Collectable collectableItem;

    IEnumerator ItemDistance(Vector3 position, Action action, float distance)
    {
        while (true)
        {
            Vector3 direction = position - transform.position;
            print(direction.magnitude);
            if (direction.magnitude < distance)
            {
                action();
                break;
            }

            yield return new WaitForSeconds(0.2f);
        }

    }

    private void PegarItem()
    {
        animator.SetTrigger("GetItem");

        AnimationEvent animationEvent;
        animationEvent = new AnimationEvent();
        animationEvent.time = 1.5f;
        animationEvent.functionName = "GetItemInHand";

        AnimationEvent animationEvent2;
        animationEvent2 = new AnimationEvent();
        animationEvent2.time = clipAnimationGetItem.length - 0.2f;
        animationEvent2.functionName = "DestroyItem";

        clipAnimationGetItem.AddEvent(animationEvent);
        clipAnimationGetItem.AddEvent(animationEvent2);

    }

    private void GetItemInHand()
    {
        collectableItem.Get();
    }

    private void DestroyItem()
    {
        collectableItem.DestroyItem();
        operationIA = false;
        pegouItem=true;
    }

    private void FinishMapLevel1()
    {
        animator.SetTrigger("Button");

        AnimationEvent animationEvent;
        animationEvent = new AnimationEvent();
        animationEvent.time = clipAnimationButton.length;
        animationEvent.functionName = "LoadingMapa";

        clipAnimationButton.AddEvent(animationEvent);
    }

    private void LoadingMapa()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Level 2");
    }

    private NavMeshAgent agent;

    private Animator animator;

    private Vector3 destination;
    bool operationIA = false;
}
