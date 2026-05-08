using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int startingEnemiesCount;
    private int totalKilled;

    private bool isLoading = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneSetup();
    }

    void SceneSetup()
    {
        int gunEnemyCount = GameObject.FindGameObjectsWithTag("GunEnemy").Length;
        int swordEnemyCount = GameObject.FindGameObjectsWithTag("SwordEnemy").Length;
        int nadeEnemyCount = GameObject.FindGameObjectsWithTag("NadeEnemy").Length;

        startingEnemiesCount = gunEnemyCount + swordEnemyCount + nadeEnemyCount;
        totalKilled = 0;

        isLoading = false;
    }

    void Update()
    {
        if (isLoading) return;

        if (startingEnemiesCount > 0 && totalKilled >= startingEnemiesCount)
        {
            isLoading = true;
            LoadNextScene();
        }
    }

    public void EnemyKilled()
    {
        totalKilled++;
    }

    void LoadNextScene()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
    }
}
