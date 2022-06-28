using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float horizontal;
    float vertical;
    public GameObject explosion;
    public float currentHeath, maxHealth = 5;
    public float playerSpeed = 0.3f;
    [SerializeField] private float slowless = 11f;
    [SerializeField] private bool isPlaying = true;
    public GameOverScreen gameOverScreen;
    public Renderer rd;
    public Rigidbody rb;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    public static PlayerController Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    private void Start()
    {
        GetStart();
    }

    private void GetStart()
    {
        rd = GetComponent<Renderer>();
        currentHeath = maxHealth;
        isPlaying = true;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        CheckDead();

    }

    private void CheckDead()
    {
        if (currentHeath > maxHealth)
        {
            currentHeath = maxHealth;
        }

        if (currentHeath <= 0 && isPlaying)
        {
            isPlaying = !isPlaying;
            StartCoroutine(RestartLevel());
            Explode();
        }
        PlayerMovement();
    }

    void FixedUpdate()
    {
        rb.velocity = moveVelocity;
    }
    void PlayerMovement()
    {
        if (isPlaying)
        {
            moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            moveVelocity = moveInput * playerSpeed;
        }
    }
    void Explode()
    {
        GameObject explosionClone = Instantiate(explosion, this.gameObject.transform.position, gameObject.transform.rotation);
    }
    public void TakeDamage(int damage)
    {
        currentHeath -= damage;
        StartCoroutine(PlayerCheck(Color.white, Color.red));
    }
    public void Healing(int heal)
    {
        currentHeath += heal;
    }

    IEnumerator PlayerCheck(Color cl1, Color cl2)
    {
        rd.material.color = cl1;
        yield return new WaitForSeconds(0.1f);
        rd.material.color = cl2;
        yield return new WaitForSeconds(0.1f);
        rd.material.color = cl1;
        yield return new WaitForSeconds(0.1f);
        rd.material.color = cl2;
    }
    IEnumerator RestartLevel()
    {
        Time.timeScale = 1f / slowless;
        Time.fixedDeltaTime = Time.fixedDeltaTime / slowless;

        yield return new WaitForSeconds(1f / slowless);

        Time.timeScale = 1f;
        Time.fixedDeltaTime = Time.fixedDeltaTime * slowless;

        gameOverScreen.Setup(Spawn.Instance.zombieKilled);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Heal")
        {
            Healing(1);
            Destroy(other.gameObject);
        }
    }

}

