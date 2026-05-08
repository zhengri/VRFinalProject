using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    public GameObject gunEnemyPrefab;


    public Vector3 spawnAreaSize = new Vector3(10f, 1f, 10f);

   
    public float spawnInterval = 3f;
    public int maxEnemies = 10;

    private int currentEnemies;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (currentEnemies >= maxEnemies)
            return;

        //random position inside spawn area
        Vector3 randomPosition = transform.position + new Vector3(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2),
            Random.Range(-spawnAreaSize.z / 2, spawnAreaSize.z / 2)
        );

        Instantiate(gunEnemyPrefab, randomPosition, Quaternion.identity);

        currentEnemies++;
    }

    //draw spawn zone in editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
