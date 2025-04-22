using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Movement")]
    public float moveCooldown = 2f;
    private float moveTimer;
    private int currentLane = 1;

    [Header("Shooting")]
    public float shootCooldown = 1.5f;
    private float shootTimer;
    private int shootStep = 0; // ยิงตาม pattern step

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] lanePositions;// จุดยิงตามเลน (ขนาด = 4)

    [Header("Lane Settings")]
    public float laneHeight = 2f; // ระยะห่างแนวตั้งระหว่างเลน

    private void Start()
    {
        moveTimer = moveCooldown;
        shootTimer = shootCooldown;
        currentLane = 2; // เริ่มที่เลน 2 (จะได้ขยับขึ้นหรือลงได้)
    }

    private void Update()
    {
        HandleMovement();
        HandleShooting();
    }

    private void HandleMovement()
    {
        moveTimer -= Time.deltaTime;

        if (moveTimer <= 0)
        {
            moveTimer = moveCooldown;

            List<int> possibleMoves = new List<int>();

            // ดูว่าขยับได้ทางไหนบ้าง
            if (currentLane > 1) possibleMoves.Add(currentLane - 1); // ขยับขึ้น
            if (currentLane < 4) possibleMoves.Add(currentLane + 1); // ขยับลง

            int newLane = possibleMoves[Random.Range(0, possibleMoves.Count)];
            currentLane = newLane;

            // ย้ายตำแหน่งตามเลน
            Vector3 newPosition = transform.position;
            newPosition.y = lanePositions[currentLane - 1].position.y;
            transform.position = newPosition;
        }
    }

    private void HandleShooting()
    {
        shootTimer -= Time.deltaTime;

        if (shootTimer <= 0)
        {
            shootTimer = shootCooldown;

            FirePattern(shootStep);

            shootStep = (shootStep + 1) % 9; // 9 pattern วนกลับไป step 0
        }
    }

    private void FirePattern(int step)
    {
        switch (step)
        {
            case 0: FireAtLanes(1); break;
            case 1: FireAtLanes(1, 3); break;
            case 2: FireAtLanes(2, 4); break;
            case 3: FireAtLanes(4, 1); break;
            case 4: FireAtLanes(3, 2); break;
            case 5: FireAtLanes(2); break;
            case 6: FireAtLanes(3); break;
            case 7: FireAtLanes(4); break;
            case 8: FireAtLanes(2, 3, 4); break;
        }
    }

    private void FireAtLanes(params int[] lanes)
    {
        foreach (int lane in lanes)
        {
            // Spawn กระสุน
            GameObject bullet = Instantiate(bulletPrefab, lanePositions[lane - 1].position, Quaternion.identity);

            // กำหนดทิศทางให้กระสุน (ไปทางซ้าย)
            Vector2 direction = Vector2.left;

            // หมุนกระสุนให้หันไปตามทิศทาง
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // ส่งทิศทางให้กระสุน (ถ้ากระสุนมีสคริปต์ควบคุมการเคลื่อนที่)
            Projectile bulletController = bullet.GetComponent<Projectile>();
            if (bulletController != null)
            {
                bulletController.SetDirection(direction);
            }
        }
    }
}
