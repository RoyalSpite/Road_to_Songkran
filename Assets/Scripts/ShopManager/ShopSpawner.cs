using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject shopSignPrefab;
    public GameObject shopPrefab;

    [Header("Spawn Settings")]
    public Transform spawnPoint; // จุด spawn ของทั้ง Sign และ Shop

    private GameManager gameManager; // ตัวที่เก็บระยะทางวิ่ง

    private bool signSpawned = false;
    private bool shopSpawned = false;

    // ระยะที่ต้อง Spawn (ตั้งแบบกำหนดตายตัว)
    private float signSpawnDistance = 0.5f; // 0.5 กิโลเมตร หรือ 500 หน่วย
    private float shopSpawnDistance = 1f; // 0.7 กิโลเมตร หรือ 700 หน่วย

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); // หา DistanceCounter ในฉาก
    }

    void Update()
    {
        float currentDistance = gameManager.Distance; // สมมุติมีฟังก์ชัน GetDistance()

        if (!signSpawned && currentDistance >= signSpawnDistance)
        {
            SpawnSign();
            signSpawned = true;
        }

        if (!shopSpawned && currentDistance >= shopSpawnDistance)
        {
            SpawnShop();
            shopSpawned = true;
        }
    }

    private void SpawnSign()
    {
        Instantiate(shopSignPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Spawned Shop Sign at distance: " + signSpawnDistance);
    }

    private void SpawnShop()
    {
        Instantiate(shopPrefab, spawnPoint.position, Quaternion.identity);
        Debug.Log("Spawned Shop at distance: " + shopSpawnDistance);
    }
}
