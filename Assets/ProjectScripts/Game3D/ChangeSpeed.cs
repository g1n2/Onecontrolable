using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSpeed : MonoBehaviour
{
    [SerializeField] private float newDistance;
    [SerializeField] private GameObject player;
    private float normalDistance,newSpeed,normalSpeed;
    private Move3D move3d;

    private void Start()
    {
        move3d = player.GetComponent<Move3D>();
        normalDistance = move3d.walkDistance;
        normalSpeed = move3d.walkSpeed;
        newSpeed = 0.5f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            move3d.walkDistance = newDistance;
            move3d.walkSpeed = normalSpeed * newSpeed;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
        {
            move3d.walkDistance = normalDistance;
            move3d.walkSpeed = normalSpeed;
        }
    }

}
