using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed = 15f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Impulse();
    }

    private void Impulse()
    {
        //rb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
        rb.velocity = transform.forward * projectileSpeed;
    }
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(this.gameObject, 2f);
        if (collision.collider.CompareTag("Player"))
        {
            PlayerController.Instance.TakeDamage(5);
        }
    }
}
