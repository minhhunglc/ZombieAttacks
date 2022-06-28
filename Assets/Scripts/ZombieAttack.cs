using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ZombieAttack : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PlayerController.Instance.TakeDamage(1);
            //Destroy(other.gameObject);
        }
        else if (other.tag == "Bullet")
        {
            Spawn.Instance.TakeZombie(1);
            Destroy(other.gameObject);
            Destroy(gameObject);
        }

    }
}
