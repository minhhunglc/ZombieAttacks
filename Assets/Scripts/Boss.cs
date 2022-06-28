using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [SerializeField] float turretRange = 13f;
    [SerializeField] float turretRotationSpeed = 5f;
    [SerializeField] private float slowless = 11f;
    private float fireRate;
    private float fireRateDelta;
    public float maxHealth = 5;
    public float curHealth;

    [SerializeField] private bool isPlaying = true;

    public GameObject explosion;
    public GameObject Victory;

    private Transform playerTransform;

    private Gun currentGun;
    private VictoryGame victoryGame;
    private void Start()
    {
        GetStart();
    }
    void GetStart()
    {
        playerTransform = FindObjectOfType<PlayerController>().transform;
        currentGun = GetComponentInChildren<Gun>();
        fireRate = currentGun.GetRateOfFire();
        curHealth = maxHealth;
        isPlaying = true;
    }
    private void Update()
    {
        BossShooting();
        CheckDead();
    }
    void BossShooting()
    {
        Vector3 playerGroundPos = new Vector3(playerTransform.position.x,
                                transform.position.y, playerTransform.position.z);


        if (Vector3.Distance(transform.position, playerGroundPos) > turretRange)
        {
            return;
        }

        Vector3 playerDirection = playerGroundPos - transform.position;
        float turretRotationStep = turretRotationSpeed * Time.deltaTime;
        Vector3 newLookDirection = Vector3.RotateTowards(transform.forward, playerDirection,
                                   turretRotationStep, 0f);
        transform.rotation = Quaternion.LookRotation(newLookDirection);

        fireRateDelta -= Time.deltaTime;
        if (fireRateDelta <= 0 && isPlaying)
        {
            currentGun.Fire();
            fireRateDelta = fireRate;
        }
    }
    void CheckDead()
    {

        if (curHealth > maxHealth)
        {
            curHealth = maxHealth;
        }

        if (curHealth <= 0 && isPlaying)
        {
            isPlaying = !isPlaying;
            StartCoroutine(RestartLevel());
            GameObject explosionClone = Instantiate(explosion, this.gameObject.transform.position, gameObject.transform.rotation);
        }

    }
    public void TakeDamage(int damage)
    {
        curHealth -= damage;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, turretRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            TakeDamage(20);
        }
    }
    IEnumerator RestartLevel()
    {
        Time.timeScale = 1f / slowless;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowless;

        yield return new WaitForSeconds(1f / slowless);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowless;

        Victory.SetActive(true);
    }
}
