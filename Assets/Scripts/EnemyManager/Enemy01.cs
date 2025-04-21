using UnityEngine;

public class Enemy01 : EnemyBase{

    [SerializeField] private Transform fireOrigin;

    [SerializeField] GameObject[] ProjectilesPool;
    private int bulletIndex = 0;
    private readonly float baseCoolDown = 1.75f;
    private float coolDown = 0;
    private bool inRange = false;

    // Start is called before the first frame update
    new void Start(){
        base.Start();
    }

    // Update is called once per frame
    new void Update(){

        if (player != null){

            inRange = Vector2.Distance(player.position, transform.position) <= 6;

            if(!inRange){

                Vector3 direction = (player.position - transform.position).normalized;
                transform.Translate(direction * moveSpeed * Time.deltaTime);

            }
            else{

                if(coolDown >= baseCoolDown){
                    // Fire
                    coolDown = 0;
                    Fire();
                }

            }
        }

        // animation
        gameObject.GetComponent<Animator>().SetBool("isShooting",inRange);
        gameObject.GetComponent<Animator>().SetBool("isPlayerUpper",
            player.transform.position.y > transform.position.y
        );


        if (health <= 0){   
            // print("Enemy Destroyed");
            gameManager.GetScore(enemyScore);
            //Destroy(gameObject);
            gameObject.SetActive(false);
        }

        coolDown += Time.deltaTime;
    }

    void Fire(){
        ProjectilesPool[bulletIndex].SetActive(true);
        ProjectilesPool[bulletIndex].transform.position = fireOrigin.position;

        // หาทิศทางจาก firePoint ไปที่ตำแหน่งเมาส์
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;  // ล็อคไม่ให้มีค่า z
        Vector3 direction = (player.transform.position - fireOrigin.position).normalized;

        // ส่งทิศทางให้กระสุน
        ProjectilesPool[bulletIndex].GetComponent<Projectile>().SetDirection(direction);

        bulletIndex = (bulletIndex + 1 == ProjectilesPool.Length) ? 0 : bulletIndex + 1;

    }
}
