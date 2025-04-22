using UnityEngine;

public class Enemy02 : EnemyBase{

    [SerializeField] private Transform fireOrigin;

    private readonly float baseCoolDown = 1f;
    private float coolDown = 0;

    // Start is called before the first frame update
    new void Start(){
        base.Start();
    }

    // Update is called once per frame
    new void Update(){

        base.Update();

        if(GameManager.isGameOver) return;
        
        if (player != null){
            
            if(inRange){
                if(coolDown >= baseCoolDown){
                    // Fire
                    coolDown = 0;
                    Fire();
                }
            }
            else{
                Vector3 direction = (player.position - transform.position).normalized;
                transform.Translate(moveSpeed * Time.deltaTime * direction);
            }

            
        }

        // animation
        gameObject.GetComponent<Animator>().SetBool("isShooting",inRange);
        gameObject.GetComponent<Animator>().SetBool("isPlayerUpper",
            player.transform.position.y > transform.position.y
        );

        coolDown += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player")){
            inRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other){
        if(other.CompareTag("Player")){
            inRange = false;
        }
    }

    private void Fire(){
        player.GetComponent<Player>().health -= 2;
    }

    public void Spawn(Vector3 initPos){
        Spawn();
        gameObject.transform.position = initPos;
    }
}
