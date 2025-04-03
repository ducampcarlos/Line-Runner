using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclesPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float overrideObstacleSpeed = 7f;
    Vector3 spawnPos;
    Coroutine spawnCoroutine;

    private void Start()
    {
        spawnPos = transform.position;
    }

    public void StartSpawner ()
    {
        spawnCoroutine = StartCoroutine(SpawnObstacles());
    }

    void Spawn()
    {
        var obstacle = Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)], spawnPos, transform.rotation);
        obstacle.GetComponent<Obstacle>().SetSpeed(overrideObstacleSpeed);
        if (Random.Range(0, 2) == 0)
        {
            spawnPos.y = -spawnPos.y;
            transform.Rotate(0, 0, 180);
        }
    }

    IEnumerator SpawnObstacles()
    {
        //Spawns obstacles at a set interval and has a 50% chance to invert Y position of the spawnPos
        while (true)
        {
            Spawn();
            yield return new WaitForSeconds(spawnInterval);
            GameManager.Instance.UpdateScore();
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
            {
                spawnInterval -= Time.deltaTime * 0.01f;
            }
            if (overrideObstacleSpeed < 30f)
            {
                overrideObstacleSpeed += Time.deltaTime * 0.05f;
            }
        }
    }
}
