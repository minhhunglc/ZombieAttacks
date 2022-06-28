using UnityEngine;

public class ZombieMovement : MonoBehaviour
{
    public int EnemySpeed;
    int MoveSpeed = 7;
    Transform Player;

    private void Awake()
    {
        Player = GameObject.Find("Player").GetComponent<Transform>();
    }

    void Update()
    {

        transform.LookAt(Player);
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;

        //if (player == null)
        //{
        //    this.GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);
        //    Destroy(this.gameObject);
        //}
        //else
        //{
        //    Vector3 localPosition = player.transform.position - transform.position;
        //    localPosition = localPosition.normalized;
        //    transform.Translate(localPosition.x * Time.deltaTime * EnemySpeed,
        //                        0f,
        //                        localPosition.z * Time.deltaTime * EnemySpeed);
        //}
    }

}
