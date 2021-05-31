using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseManager : MonoBehaviour
{
    public LayerMask mask;
    Player player;
    public UnityEvent<Vector3> onMouseClicked;

    public Texture2D pointer;
    public Texture2D target;
    public Texture2D doorWay;
    public Texture2D doorWayBack;

    public Transform doorWayTarget;
    public Transform doorWayBackTarget;

    Ray ray;
    RaycastHit hit;
    bool isColliding;
    bool canWalk = true;

    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        isColliding = Physics.Raycast(ray, out hit, 200, mask);
        Debug.DrawRay(ray.origin, ray.direction * 200, Color.red);

        if (isColliding)
        {
            switch (hit.collider.tag)
            {
                case "Rock":
                    Cursor.SetCursor(pointer, new Vector2(0, 0), CursorMode.Auto);
                    break;
                case "DoorWay":
                    Cursor.SetCursor(doorWay, new Vector2(0, 0), CursorMode.Auto);
                    break;
                case "DoorWayBack":
                    Cursor.SetCursor(doorWayBack, new Vector2(0, 0), CursorMode.Auto);
                    break;
                default:
                    Cursor.SetCursor(target, new Vector2(16, 16), CursorMode.Auto);
                    break;
            }

            if (Input.GetMouseButton(0) && canWalk)
            {
                if (!hit.collider.CompareTag("Rock"))
                {
                    onMouseClicked.Invoke(hit.point);
                }
                if (hit.collider.CompareTag("DoorWay"))
                {
                    onMouseClicked.Invoke(doorWayTarget.position);
                    canWalk = false;
                }
                if (hit.collider.CompareTag("DoorWayBack"))
                {
                    onMouseClicked.Invoke(doorWayBackTarget.position);
                    canWalk = false;
                }
            }
        }
        else
        {
            Cursor.SetCursor(pointer, new Vector2(0, 0), CursorMode.Auto);
        }

        if(player.transform.position.x == doorWayTarget.position.x && player.transform.position.z == doorWayTarget.position.z || player.transform.position.x == doorWayBackTarget.position.x && player.transform.position.z == doorWayBackTarget.position.z)
        {
            canWalk = true;
        }
    }
}
