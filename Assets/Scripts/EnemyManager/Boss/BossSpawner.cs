using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawner : MonoBehaviour
{
    public GameObject bossPrefab; // Prefab หรือ GameObject ของบอสที่เอามา SetActive
    public float spawnDistance = 22f; // ระยะที่ต้องการให้บอสโผล่ (กิโลเมตรหรือหน่วยอะไรก็ได้)

    private GameManager gameManager;
    public static bool bossSpawned = false;

    private void Start()
    {
        bossPrefab.SetActive(false); // เริ่มเกม ปิดบอสไว้ก่อน
        gameManager = FindObjectOfType<GameManager>(); // หา GameManager ในฉาก
    }

    private void Update()
    {
        if (bossSpawned) return; // ถ้า spawn ไปแล้ว ไม่ต้องเช็กอีก

        float currentDistance = gameManager.Distance;

        if (currentDistance >= spawnDistance){
            FindObjectOfType<BGMManager>().ChangeBossBGM();
            gameManager.CancelEnemySpawn();
            bossPrefab.SetActive(true); // เปิดบอส
            bossSpawned = true;
        }
    }

    
}
