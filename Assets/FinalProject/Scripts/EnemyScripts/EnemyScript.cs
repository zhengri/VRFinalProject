using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyScript : MonoBehaviour
{
    public NavMeshAgent navAgent;
    public Transform xrOrigin;

    public float attackDistance = .7f;

    public bool hasTriggered = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        navAgent.speed = 0.4f;
        navAgent.acceleration = 5f;

        FindPlayer();
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
        FindPlayer();
    }

    void FindPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            xrOrigin = player.transform;
        }
    }

    void Update()
    {
        if (xrOrigin == null)
        {
            FindPlayer();
            return;
        }

        if (hasTriggered) return;

        navAgent.SetDestination(xrOrigin.position);

        float distance = Vector3.Distance(transform.position, xrOrigin.position);

        if (distance <= attackDistance)
        {
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        hasTriggered = true;
        SceneManager.LoadScene(4);
    }
}