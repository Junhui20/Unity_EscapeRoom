using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    public GameObject Player;
    public GameObject TeleportTo;
    public GameObject StartTeleporter;

    private void OnTriggerEnter(Collider collision)
    {
        UnityEngine.Debug.Log($"Collision with: {collision.gameObject.name} with tag: {collision.gameObject.tag}");

        // Change this to check for Player tag instead of Teleporter
        if (collision.gameObject.CompareTag("Player"))
        {
            // Calculate the offset vector
            Vector3 offset = TeleportTo.transform.position - transform.position;
            Player.transform.position += offset;

            UnityEngine.Debug.Log($"Teleported player to: {Player.transform.position}");
        }
    }
}