using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 15f;
    private Vector3 moveDirection;
    [SerializeField] private float Damage;

    public void SetDirection(Vector3 dir)
    {
        moveDirection = dir;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }

    void Update()
    {
        if(GameManager.isGameOver) return;

        transform.Translate(moveDirection * speed * Time.deltaTime, Space.World);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(gameObject.CompareTag("Bullet")){

            if(other.CompareTag("Enemy")){
                if(other.gameObject.GetComponent<EnemyBase>().health <= 0){
                    return;
                }
                other.gameObject.GetComponent<EnemyBase>().Hit(Damage);
                OnBecameInvisible();

            }
            else if(other.CompareTag("Boss")){
                print("hit boss");
                other.gameObject.GetComponent<Boss>().Hit(Damage);
                OnBecameInvisible();
            }

        }
        else if(gameObject.CompareTag("EnemyBullet") && other.CompareTag("Player")){
            other.gameObject.GetComponent<Player>().health -= Damage;
            OnBecameInvisible();
        }
    }

    void OnBecameInvisible(){
        // print("out of screen");
        gameObject.SetActive(false);
    }

    public void SetDamage(float dam){
        Damage = dam;
    }
}
