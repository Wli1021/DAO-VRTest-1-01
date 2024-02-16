using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportManager : MonoBehaviour
{
    public Transform[] teleportTargets;

    // Method to teleport the player to a specific target by index
    public void TransportPlayerToTarget(int targetIndex)
    {
        if (targetIndex >= 0 && targetIndex < teleportTargets.Length)
        {
            Transform target = teleportTargets[targetIndex];
            Debug.Log($"Teleporting to: {target.position}");
            transform.position = target.position;
            //transform.rotation = target.rotation;
        }
        else
        {
            Debug.LogError("Invalid teleport target index.");
        }
    }
}