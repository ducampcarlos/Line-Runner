using System.Collections;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] obstaclesPrefabs;
    [SerializeField] private float spawnInterval = 2f;
    Vector3 spawnPos;

    private void Start()
    {
        spawnPos = transform.position;
        StartCoroutine(SpawnObstacles());
    }

    void Spawn()
    {
        Instantiate(obstaclesPrefabs[Random.Range(0, obstaclesPrefabs.Length)], spawnPos, transform.rotation);
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
            yield return new WaitForSeconds(spawnInterval);
            Spawn();
            GameManager.Instance.UpdateScore();
        }
    }
}
