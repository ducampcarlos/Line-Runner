using System.Collections.Generic;
using UnityEngine;

public class ObstaclePoolManager : MonoBehaviour
{
    public static ObstaclePoolManager Instance;

    public GameObject[] obstaclePrefabs;
    [SerializeField] private int poolSize = 15;

    private List<GameObject> pooledObstacles = new List<GameObject>();

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < poolSize; i++)
        {
            int prefabIndex = i % obstaclePrefabs.Length;
            GameObject obj = Instantiate(obstaclePrefabs[prefabIndex]);
            obj.SetActive(false);
            pooledObstacles.Add(obj);
        }
    }

    public GameObject GetPooledObstacle(int prefabIndex)
    {
        foreach (GameObject obj in pooledObstacles)
        {
            if (!obj.activeInHierarchy && obj.name.Contains(obstaclePrefabs[prefabIndex].name))
            {
                return obj;
            }
        }

        // Reutiliza uno si todos están ocupados (puedes cambiar esto si preferís no hacer esto)
        foreach (GameObject obj in pooledObstacles)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        return null;
    }
}
