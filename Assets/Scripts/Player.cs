using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform spawnPoint;
    public Rigidbody rb;

    private void OnCollisionEnter(Collision collision)
    {
    if (collision.gameObject.CompareTag("Void"))
        {
            transform.position = spawnPoint.position;
            rb.velocity =  Vector3.zero;
        }
    }
}
