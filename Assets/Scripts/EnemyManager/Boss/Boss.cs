using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    [Header("Health")]
    public float health = 150f;

    [Header("Movement")]
    public float moveCooldown = 2f;
    public float moveSpeed = 5f;
    private float moveTimer;
    private int currentLane = 1;
    private Transform targetLane;

    [Header("Shooting")]
    public float shootCooldown = 1.5f;
    private float shootTimer;
    private int shootStep = 0; // ยิงตาม pattern step

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] lanePositions;// จุดยิงตามเลน (ขนาด = 4)

    [Header("Lane Settings")]
    public float laneHeight = 2f; // ระยะห่างแนวตั้งระหว่างเลน

     [Header("Intro Movement")]
    public Vector3 entryTargetPosition; // จุดที่บอสจะเคลื่อนมาถึงก่อนเริ่มโจมตี
    public float entrySpeed = 3f; // ความเร็วช่วงเข้า

    private bool hasEntered = false; // เข้าไปจุดกลางจอแล้วหรือยัง

    [Header("VisualSprite")]
    [SerializeField] private Sprite normalSprite; // sprite ปกติ
    [SerializeField] private Sprite hitSprite; // sprite ขาวล้วน โชว์ว่ายิงโดน

    [Header("HitCountdown")]
    public float hitTimer = 0f;


    private void Start()
    {
        moveTimer = moveCooldown;
        shootTimer = shootCooldown;
        currentLane = 2; // เริ่มที่เลน 2 (จะได้ขยับขึ้นหรือลงได้)

        if (entryTargetPosition == Vector3.zero)
        {
            entryTargetPosition = new Vector3(6f, transform.position.y, 0f); // ปรับตำแหน่งตามที่ต้องการ
        }
    }

    private void Update()
    {
        if (!hasEntered)
        {
            MoveToEntryPoint();
        }
        else
        {
            HandleMovement();
            HandleShooting();
            HandleHit();
        }
    }

    private void MoveToEntryPoint()
    {
        transform.position = Vector3.MoveTowards(transform.position, entryTargetPosition, entrySpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, entryTargetPosition) < 0.01f)
        {
            hasEntered = true;
        }
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
            StartCoroutine(SmoothMoveToLane(lanePositions[currentLane - 1].position.y));
        }
    }

    private IEnumerator SmoothMoveToLane(float targetY)
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        Vector3 targetPos = new Vector3(transform.position.x, targetY, transform.position.z);

        while (elapsed < 0.2f)
        {
            transform.position = Vector3.Lerp(startPos, targetPos, elapsed / 0.2f);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPos;
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
            bullet.tag = "EnemyBullet";

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

    private void HandleHit(){

        if (hitTimer <= 0) return;

        hitTimer -= Time.deltaTime;
        // print("Hit");

        gameObject.GetComponent<Animator>().SetBool("hit", hitTimer > 0);
        if (hitTimer <= 0) hitTimer = 0f;

    }

    public void Hit(float Damage){
        health -= Damage;

        if(health <= 0){
            // WIN THE GAME
            Player.enemyPresence = false;
            return;
        }
        gameObject.GetComponent<Animator>().SetBool("hit", true);
        hitTimer = 0.1f;
    }

}
