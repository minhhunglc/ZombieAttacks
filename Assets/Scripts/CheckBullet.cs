using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBullet : MonoBehaviour
{
    public GameObject spark;
    private void OnCollisionEnter(UnityEngine.Collision other)
    {

        GameObject Spark = Instantiate(spark, this.transform.position, Quaternion.identity);
        this.gameObject.GetComponent<Rigidbody>().useGravity = true;
        Destroy(Spark, 0.1f);
        Destroy(this.gameObject, 0.2f);
    }

}
