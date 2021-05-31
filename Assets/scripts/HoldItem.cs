using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItem : MonoBehaviour
{
    public Transform hand;

    private void FixedUpdate()
    {
        transform.position = hand.position;
    }
}
