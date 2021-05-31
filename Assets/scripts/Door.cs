using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Door : MonoBehaviour
{
    public Transform transformDoor;
    public Transform destinationTransform;

    public bool portaOpen;

    public Transform DestinoDoor()
    {
        if (portaOpen)
            return null;  

        OpenDoor(6);

        return destinationTransform;
    }

    private void OpenDoor(float tempoFechar)
    {
        portaOpen = true;
        transformDoor.DOLocalMoveX(2, 1.3f).OnComplete(() =>
       {
           portaOpen = false;
           Invoke("FecharPorta",tempoFechar);
       });
    }

    private void FecharPorta()
    {
        transformDoor.DOLocalMoveX(0, 1.3f).OnComplete(() => portaOpen = false);
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            OpenDoor(5);
            print("Oi");
        }
    }
}
