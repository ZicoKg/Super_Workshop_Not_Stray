using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    public bool isOpen;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        Open();
    }

    public void OnTriggerEnter(Collider collision)
    {
        Debug.Log("Collision with: " + collision.gameObject.name);
        if (collision.gameObject.tag == player.tag && isOpen)
        {
            Debug.Log("You win!");
        }
    }

    public void Open()
    {
        if (isOpen)
        {
            return;
        }

        transform.Rotate(0, 90f, 0);
        isOpen = true;
    }
}
