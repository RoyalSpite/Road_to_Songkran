using UnityEngine;

public class Enemy01 : EnemyBase{

    [SerializeField] private Transform fireOrigin;

    [SerializeField] GameObject[] ProjectilesPool;
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

        if(GameManager.isGameOver) return;

        if (player != null){

            if(coolDown >= baseCoolDown){
                // Fire
                // Check if which projectiles not in use
                for(int i = 0;i < ProjectilesPool.Length; i++){
                    if(!ProjectilesPool[i].activeSelf){
                        coolDown = 0;
                        Fire(i);
                        break;
                    }
                }

            }

        }

        // animation
        gameObject.GetComponent<Animator>().SetBool("isShooting",true);
        gameObject.GetComponent<Animator>().SetBool("isPlayerUpper",
            player.transform.position.y > transform.position.y
        );

        coolDown += Time.deltaTime;

        transform.Translate(
            Vector3.left * ParallaxBG.scrollSpeed * Time.deltaTime
        );

    }

    void Fire(int projIndex){
        ProjectilesPool[projIndex].SetActive(true);
        ProjectilesPool[projIndex].transform.position = fireOrigin.position;

        // หาทิศทางจาก firePoint ไปที่ตำแหน่งเมาส์
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;  // ล็อคไม่ให้มีค่า z
        Vector3 direction = (player.transform.position - fireOrigin.position).normalized;

        // ส่งทิศทางให้กระสุน
        ProjectilesPool[projIndex].GetComponent<Projectile>().SetDirection(direction);

    }

    public void Spawn(Vector3 TargetFirePosition){
        
        Spawn();

        transform.position = TargetFirePosition;

    }

    void OnBecameInvisible(){
        gameObject.SetActive(false);
    }
}
