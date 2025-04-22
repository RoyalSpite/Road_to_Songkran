using UnityEngine;

public class Enemy01 : EnemyBase{

    [SerializeField] private Transform fireOrigin;

    [SerializeField] GameObject[] ProjectilesPool;
    private int bulletIndex = 0;
    private readonly float baseCoolDown = 1.75f;
    private float coolDown = 0;

    private Vector3 FirePosition;

    // Start is called before the first frame update
    new void Start(){
        base.Start();
    }

    // Update is called once per frame
    new void Update(){

        base.Update();

        if (player != null){

            
            if(!inRange){

                Vector3 direction = (FirePosition - transform.position).normalized;
                transform.Translate(direction * moveSpeed * Time.deltaTime);

                if(Mathf.Abs(FirePosition.y - transform.position.y) <= 0.0025f){
                    transform.position = FirePosition;
                    inRange = true;
                }

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

    public void Spawn(Vector3 TargetFirePosition){
        
        Spawn();

        FirePosition = TargetFirePosition;
        FirePosition.x = Random.Range(-8f, 8f);

        int YMagnitude = (FirePosition.y < 0)? -1 : 1;

        transform.position = new Vector3(
            FirePosition.x,
            YMagnitude * 5
        );

    }
}
