using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGetItem : MonoBehaviour
{
    public Transform holdItem;

    I_Collectable collectableItem;

    List<Item> lstItens;

    Animator animator;

    public AnimationClip clipAnimationGetItem;

    bool isGettingItem;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<I_Collectable>(out collectableItem))
        {
            if (other.CompareTag("Item") && !isGettingItem)
            {
                Vector3 direction = other.transform.position - transform.position;
                transform.forward = direction;
                animator.SetTrigger("GetItem");

                AnimationEvent animationEvent;
                animationEvent = new AnimationEvent();
                animationEvent.time = 1.5f;
                animationEvent.functionName = "GetItem";

                AnimationEvent animationEvent2;
                animationEvent2 = new AnimationEvent();
                animationEvent2.time = clipAnimationGetItem.length - 0.2f;
                animationEvent2.functionName = "DestroyItem";

                clipAnimationGetItem.AddEvent(animationEvent);
                clipAnimationGetItem.AddEvent(animationEvent2);

                isGettingItem=true;

            }
        }
    }

    private void GetItem()
    {
        collectableItem.Get();
    }

    private void DestroyItem()
    {
        collectableItem.DestroyItem();
        isGettingItem=false;
    }

}
