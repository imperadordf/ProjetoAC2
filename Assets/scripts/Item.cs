using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, I_Collectable
{
    public GameObject itemScene;
    public GameObject itemHand;
    public GameObject socket;

    SphereCollider collider;

    private void Start()
    {
        collider = GetComponent<SphereCollider>();
    }

    public void Get()
    {
        collider.enabled = false;

        itemScene.SetActive(false);
        itemHand.SetActive(true);

        transform.SetParent(socket.transform);
        transform.localPosition = Vector3.zero;
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

    public void DestroyItem()
    {
        Destroy(this.gameObject);
    }

}
