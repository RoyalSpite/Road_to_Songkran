using UnityEngine;

public class Spawner : MonoBehaviour
{
    [System.Serializable]
    public class SpawnableObject
    {
        public GameObject prefab;
        public float spawnChance = 1f;
    }

    public SpawnableObject[] objectsToSpawn;
    public Transform[] spawnPoints;  // จุดเกิดทั้งหมด
    public float spawnInterval = 1.5f;

    void Start()
    {
        InvokeRepeating("SpawnObject", 1f, spawnInterval);
    }

    void SpawnObject()
    {
        GameObject prefabToSpawn = GetRandomPrefab();
        Transform spawnPoint = GetRandomSpawnPoint();

        if (prefabToSpawn != null && spawnPoint != null)
        {
            Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);
        }
    }

    GameObject GetRandomPrefab()
    {
        float totalChance = 0f;
        foreach (var obj in objectsToSpawn)
        {
            totalChance += obj.spawnChance;
        }

        float randomPoint = Random.value * totalChance;
        float current = 0f;

        foreach (var obj in objectsToSpawn)
        {
            current += obj.spawnChance;
            if (randomPoint <= current)
            {
                return obj.prefab;
            }
        }

        return null;
    }

    Transform GetRandomSpawnPoint()
    {
        if (spawnPoints.Length == 0)
            return null;

        int index = Random.Range(0, spawnPoints.Length);
        return spawnPoints[index];
    }
}
