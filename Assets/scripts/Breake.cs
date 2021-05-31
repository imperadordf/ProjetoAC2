using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breake : MonoBehaviour
{
    GameObject original;
    GameObject broken;
    BoxCollider boxCollider;
    AudioSource audioSource;

    private void Start()
    {
        original = transform.Find("Original").gameObject;
        broken = transform.Find("Broken").gameObject;
        boxCollider = GetComponent<BoxCollider>();
        audioSource = GetComponent<AudioSource>();
        broken.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        audioSource.Play();
        boxCollider.enabled = false;
        original.SetActive(false);
        broken.SetActive(true);
        Destroy(broken, 3);
    }
}
