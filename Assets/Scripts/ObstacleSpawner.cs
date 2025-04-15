using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float overrideObstacleSpeed = 7f;
    [SerializeField] private Transform topSpawnPoint;
    [SerializeField] private Transform bottomSpawnPoint;

    private Coroutine spawnCoroutine;
    private float timer = 0f;

    private void Start() => timer = 0f;

    public void StartSpawner()
    {
        spawnCoroutine = StartCoroutine(SpawnObstacles());
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            timer += spawnInterval;
            bool spawnBoth = timer >= 30f;

            int indexTop = Random.Range(0, ObstaclePoolManager.Instance.obstaclePrefabs.Length);
            int indexBottom = Random.Range(0, ObstaclePoolManager.Instance.obstaclePrefabs.Length);

            if (spawnBoth)
            {
                // Evita doble obstáculo impenetrable (por ejemplo el tipo 0)
                while (indexTop == 0 && indexBottom == 0)
                {
                    indexBottom = Random.Range(0, ObstaclePoolManager.Instance.obstaclePrefabs.Length);
                }

                Spawn(indexTop, topSpawnPoint.position, 0);
                Spawn(indexBottom, bottomSpawnPoint.position, 180);
            }
            else
            {
                bool spawnTop = Random.Range(0, 2) == 0;
                int index = Random.Range(0, ObstaclePoolManager.Instance.obstaclePrefabs.Length);
                Vector3 pos = spawnTop ? topSpawnPoint.position : bottomSpawnPoint.position;
                float rot = spawnTop ? 0 : 180;
                Spawn(index, pos, rot);
            }

            yield return new WaitForSeconds(spawnInterval);
            GameManager.Instance.UpdateScore();
        }
    }

    void Spawn(int index, Vector3 position, float rotationZ)
    {
        GameObject obstacle = ObstaclePoolManager.Instance.GetPooledObstacle(index);
        if (obstacle != null)
        {
            obstacle.transform.position = position;
            obstacle.transform.rotation = Quaternion.Euler(0, 0, rotationZ);
            obstacle.transform.SetParent(transform); // Parent al spawner
            obstacle.SetActive(true);
            obstacle.GetComponent<Obstacle>().SetSpeed(overrideObstacleSpeed);
        }
    }

    public void StopSpawner()
    {
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = null;
        }
    }

    private void Update()
    {
        if (spawnCoroutine != null)
        {
            if (spawnInterval > 0.5f)
                spawnInterval -= Time.deltaTime * 0.01f;
            if (overrideObstacleSpeed < 30f)
                overrideObstacleSpeed += Time.deltaTime * 0.05f;
        }
    }
}
