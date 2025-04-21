using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public readonly int fullHealth = 100;
    [SerializeField] public float health;

    [SerializeField] private float moveSpeed = 15f;
    
    [Header("Item")]
    [SerializeField] private readonly int baseScoreMultiplier = 1;
    [SerializeField] public int scoreMultiplier;
    public float scoreCountDown = 0f;
    [SerializeField] public readonly int baseCarSpeedMultiplier = 1;
    [SerializeField] public float carSpeedMultiplier;
    public float speedCountDown = 0f;


    [Header("Child on truck")]
    [SerializeField] private GameObject Child;
    [SerializeField] private int  animIndex = 0;

    // Shooting variables
    [Header("Shooting Variables")]
    [SerializeField] private GameObject[] projectilesList;
    public int bulletIndex = 0;
    public Transform firePoint;   // จุดที่ยิงกระสุนออกไป
    public float fireRate = 0.2f;  // เวลาหน่วงระหว่างยิงแต่ละครั้ง
    private float fireTimer = 0f;

    public bool enemyPresence = false;

    private Vector3 mousePosition;

    public Vector3 target = Vector3.zero;

    void Start(){
        health = fullHealth;
        scoreMultiplier = baseScoreMultiplier;
        carSpeedMultiplier = baseCarSpeedMultiplier;

    }

    // Update is called once per frame
    void Update()
    {
        // Update Mouse Position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // move
        if (target != Vector3.zero)
        {

            float step = moveSpeed * Time.deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                // Swap the position of the cylinder.
                target = Vector3.zero;
            }

        }

        // fire and cooldown
        fireTimer += Time.deltaTime;
        if (fireTimer >= fireRate && enemyPresence)
        {
            Shoot();
            fireTimer = 0f;
        }

        // child animation control
        Vector3 dir = (mousePosition - transform.position).normalized;
        float faceAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // print(faceAngle);

        if(faceAngle >= -150f && faceAngle <= -40f){
            animIndex = 1;
        }
        else if(faceAngle > -40f && faceAngle < 40f){
            animIndex = 0;
        }
        else if(faceAngle <= 150f && faceAngle >= 40f){
            animIndex = 2;
        }
        else if(
            (faceAngle > 150f && faceAngle <= 180f) || 
            (faceAngle >= -180f && faceAngle < -150f)
        ){
            animIndex = 3;
        }

        Child.GetComponent<Animator>().SetInteger("AnimIndex", animIndex);

        // speed modify contdown
        if(speedCountDown > 0){
            speedCountDown -= Time.deltaTime;

            if(speedCountDown <= 0){
                carSpeedMultiplier = baseCarSpeedMultiplier;
                speedCountDown = 0;
            }
        }

        // score modify countdown
        if(scoreCountDown > 0){
            scoreCountDown -= Time.deltaTime;

            if(scoreCountDown <= 0){
                scoreMultiplier = baseScoreMultiplier;
                scoreCountDown = 0;
            }

        }

        // game over
        if(health <= 0){
            Time.timeScale = 0;
        }

    }

    void Shoot(){

        // สร้างกระสุน
        projectilesList[bulletIndex].SetActive(true);
        projectilesList[bulletIndex].transform.position = firePoint.position;

        // หาทิศทางจาก firePoint ไปที่ตำแหน่งเมาส์
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;  // ล็อคไม่ให้มีค่า z
        Vector3 direction = (mousePosition - firePoint.position).normalized;

        // ส่งทิศทางให้กระสุน
        projectilesList[bulletIndex].GetComponent<Projectile>().SetDirection(direction);

        bulletIndex = (bulletIndex + 1 == projectilesList.Length) ? 0 : bulletIndex + 1;

    }
    
}
