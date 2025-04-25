using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] public readonly int fullHealth = 100;
    [SerializeField] public float health;

    private float DamageTakenVisualCountDown = 0;

    [SerializeField] private float moveSpeed = 15f;
    
    [Header("Item")]
    // score
    [SerializeField] public int baseScoreMultiplier = 1;
    [SerializeField] public int scoreMultiplier;
    public float scoreCountDown = 0f;

    // speed
    [SerializeField] public float baseCarSpeedMultiplier = 1;
    [SerializeField] public float carSpeedMultiplier;
    public float speedCountDown = 0f;

    // weapon
    [SerializeField] public int baseWeaponDamage = 1;
    [SerializeField] public int weaponDamage;
    public float weaponCountDown = 0f;


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
    
    public static bool enemyPresence = true;

    private Vector3 mousePosition;

    public Vector3 target = Vector3.zero;

    // inventory
    [Header("Inventory")]
    [SerializeField] GroundItemType pickupItem = GroundItemType.None;
    [SerializeField] Sprite[] ItemImgs;
    [SerializeField] Image shownPickUp;
    [SerializeField] GameObject[] PowerUpIndicator;
    

    void Start(){
        health = fullHealth;
        scoreMultiplier = baseScoreMultiplier;
        carSpeedMultiplier = baseCarSpeedMultiplier;
        weaponDamage = baseWeaponDamage;
    }

    // Update is called once per frame
    void Update(){

        // game over
        if(GameManager.isGameOver) return;

        float deltaTime = Time.deltaTime;

        if(health <= 0){
            Child.SetActive(false);
            GetComponent<Animator>().SetBool("isDestroyed", true);
            transform.Find("ExplosionAnim").
                gameObject.GetComponent<Animator>().SetBool("isDestroyed", true);

            GameObject.Find("GameManager").GetComponent<GameManager>().BadEnd();
            return;
            // Time.timeScale = 0;
        }


        // Update Mouse Position
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        // move
        if (target != Vector3.zero)
        {

            float step = moveSpeed * deltaTime; // calculate distance to move
            transform.position = Vector3.MoveTowards(transform.position, target, step);

            // Check if the position of the cube and sphere are approximately equal.
            if (Vector3.Distance(transform.position, target) < 0.001f)
            {
                // Swap the position of the cylinder.
                target = Vector3.zero;
            }

        }

        // fire and cooldown
        if (fireTimer >= fireRate){
            if(enemyPresence){
                Shoot();
                fireTimer = 0f;
            }
        }
        else{
            fireTimer += deltaTime;
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
            speedCountDown -= deltaTime;

            if(speedCountDown <= 0){
                carSpeedMultiplier = baseCarSpeedMultiplier;
                ParallaxBG.scrollSpeed = ParallaxBG.baseScrollSpeed;
                PowerUpIndicator[0].SetActive(false);
                speedCountDown = 0;
            }
        }

        // score modify countdown
        if(scoreCountDown > 0){
            scoreCountDown -= deltaTime;

            if(scoreCountDown <= 0){
                scoreMultiplier = baseScoreMultiplier;
                PowerUpIndicator[1].SetActive(false);
                scoreCountDown = 0;
            }

        }

        // weapon modify countdown
        if(weaponCountDown > 0){
            weaponCountDown -= deltaTime;

            if(weaponCountDown <= 0){
                weaponDamage = baseWeaponDamage;
                PowerUpIndicator[2].SetActive(false);
                weaponCountDown = 0;
            }

        }

        // damage from obstacle visual
        if(DamageTakenVisualCountDown > 0){
            DamageTakenVisualCountDown -= deltaTime;

            Color statusColor = (
                DamageTakenVisualCountDown <= 0
            )? Color.white : Color.red;

            gameObject.GetComponent<SpriteRenderer>().color = statusColor;
            Child.GetComponent<SpriteRenderer>().color = statusColor;

        }
    }

    void Shoot(){

        FindObjectOfType<PlayerSoundManager>().PlayShootSound();

        // สร้างกระสุน
        projectilesList[bulletIndex].SetActive(true);
        projectilesList[bulletIndex].transform.position = firePoint.position;

        // หาทิศทางจาก firePoint ไปที่ตำแหน่งเมาส์
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;  // ล็อคไม่ให้มีค่า z
        Vector3 direction = (mousePosition - firePoint.position).normalized;

        // ส่งทิศทางให้กระสุน
        projectilesList[bulletIndex].GetComponent<Projectile>().SetDirection(direction);
        projectilesList[bulletIndex].GetComponent<Projectile>().SetDamage(weaponDamage);

        bulletIndex = (bulletIndex + 1 == projectilesList.Length) ? 0 : bulletIndex + 1;

    }

    public bool CanPickUp(){
        return pickupItem == GroundItemType.None;
    }

    public void PickUp(GroundItemType pickUp){

        pickupItem = pickUp;
        shownPickUp.gameObject.SetActive(true);

        // show pickup Item
        shownPickUp.sprite = ItemImgs[(int)pickupItem];
        
    }

    public void UsePowerUP(){
        if(pickupItem != GroundItemType.None){

            switch(pickupItem){
                // for item
                case GroundItemType.Fuel:{
                    health = Mathf.Min(
                        fullHealth, health + (fullHealth / 2)
                    );
                    break;
                }
                case GroundItemType.Speed:{
                    carSpeedMultiplier *= 1.5f;
                    speedCountDown = 5f;
                    PowerUpIndicator[0].SetActive(true);
                    ParallaxBG.scrollSpeed *= carSpeedMultiplier;

                    break;
                }
                case GroundItemType.Double:{
                    scoreMultiplier *= 2;
                    scoreCountDown = 5f;
                    PowerUpIndicator[1].SetActive(true);
                    break;
                }
                case GroundItemType.Weapon:{
                    weaponDamage = 2;
                    weaponCountDown = 5f; 
                    PowerUpIndicator[2].SetActive(true);
                    break;
                }
            }

            shownPickUp.gameObject.SetActive(false);
            pickupItem = GroundItemType.None;
                
        }
    }

    public void TakeDamageFromObstacle(){
        DamageTakenVisualCountDown = 0.35f;
    }
    
}
