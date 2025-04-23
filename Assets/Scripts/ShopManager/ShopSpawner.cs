using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSpawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject shopSignPrefab;
    public GameObject shopPrefab;
    [SerializeField]private ObjectMove mover;

    [Header("Spawn Settings")]
    public Transform spawnPoint; // จุด spawn ของทั้ง Sign และ Shop

    private GameManager gameManager; // ตัวที่เก็บระยะทางวิ่ง

    private bool signSpawned = false;
    private bool shopSpawned = false;
    private bool isShopMoving = false; // <<< เพิ่มตัวนี้

    private float signSpawnDistance = 0.5f; // 0.5 กิโลเมตร หรือ 500 หน่วย
    private float shopSpawnDistance = 1f; // 1 กิโลเมตร หรือ 1000 หน่วย

    public float shopMoveSpeed = 5f; // ความเร็วในการเลื่อน

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>(); 
    }

    void Update()
    {
        float currentDistance = gameManager.Distance; 

        if (!signSpawned && currentDistance >= signSpawnDistance)
        {
            SpawnSign();
            signSpawned = true;
        }

        if (!shopSpawned && currentDistance >= shopSpawnDistance)
        {

            mover.enabled = true;
            
            isShopMoving = true;
            shopSpawned = true;
            Debug.Log("Spawned Shop at distance: " + shopSpawnDistance);
        }

        if (isShopMoving && shopPrefab != null)
        {
            shopPrefab.transform.Translate(Vector3.left * shopMoveSpeed * Time.deltaTime);
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
        isShopMoving = true; // <<< ให้เริ่มขยับใน Update
        Debug.Log("Spawned Shop at distance: " + shopSpawnDistance);
    }
}
