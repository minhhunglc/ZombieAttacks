using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{
    public GameObject[] zombiesPrefab;
    public GameObject boss;

    private Vector3 spawnPosition;

    public int zombieKilled;
    public static int zombieCounter;
    public int maxZombies = 20;

    public Text zombieKilledText;
    public Text Waves;

    public Image Dangers;

    public Material[] materials;

    public static bool gameIsPaused;

    public GameObject pauseText;
    public static Spawn Instance { get; private set; }

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

    void Start()
    {
        //InvokeRepeating("Spawner", spawnTime, spawnTime);
        CheckMapSelect();
    }

    private void CheckMapSelect()
    {
        if (GameManager.Instance.ChoosePink == true)
        {
            gameObject.GetComponent<Renderer>().material = materials[1];
        }
        else
        {
            gameObject.GetComponent<Renderer>().material = materials[0];
        }
        zombieKilled = 0;
        WavesSelect(zombieKilled);
    }

    void Update()
    {
        zombieKilledText.text = "Zombies: " + zombieKilled.ToString();

        if (Input.GetKeyDown(KeyCode.Escape))
        {

            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (gameIsPaused)
        {
            pauseText.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pauseText.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void TakeZombie(int amount)
    {
        zombieKilled += amount;
        WavesSelect(zombieKilled);
    }
    public void WavesSelect(int amountZombie)
    {
        switch (amountZombie)
        {
            case 0:
                Waves.text = "WAVE 1";
                Waves.enabled = true;
                StartCoroutine(SpawnZombies(5, 4, zombiesPrefab[0]));
                StartCoroutine(Wait(2f));
                break;
            case 5:
                Waves.enabled = true;
                Waves.text = "WAVE 2";
                StartCoroutine(SpawnZombies(5, 3, zombiesPrefab[1]));
                StartCoroutine(Wait(2f));
                break;
            case 10:
                Waves.enabled = true;
                Waves.text = "WAVE 3";
                StartCoroutine(SpawnZombies(5, 2, zombiesPrefab[2]));
                StartCoroutine(Wait(2f));
                break;
            case 15:
                Waves.enabled = true;
                Waves.text = "WAVE 4";
                StartCoroutine(SpawnZombies(5, 1, zombiesPrefab[3]));
                StartCoroutine(Wait(2f));
                break;
            case 20:
                Dangers.enabled = true;
                Waves.enabled = true;
                Waves.text = "BOSS WARNING!!";
                boss.SetActive(true);
                StartCoroutine(Wait(2f));
                break;
        }

    }
    IEnumerator Wait(float delayInSecs)
    {
        yield return new WaitForSeconds(delayInSecs);
        Waves.enabled = false;
        if (zombieKilled == 20)
        {
            Dangers.enabled = false;
        }
    }
    IEnumerator SpawnZombies(int count, float delay, GameObject zombie)
    {
        spawnPosition.x = Random.Range(-19, 19);
        spawnPosition.y = 0.5f;
        spawnPosition.z = Random.Range(-19, 19);
        for (int i = 0; i < count; i++)
        {
            Instantiate(zombie, spawnPosition, Quaternion.identity);
            yield return new WaitForSeconds(delay);
        }
    }
}
