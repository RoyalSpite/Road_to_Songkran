using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 3f;

    [SerializeField] protected int enemyScore;
    protected GameManager gameManager;

    [SerializeField] public int health = 1;
    protected Transform player;

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    protected void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }

        if (health <= 0)
        {   
            // print("Enemy Destroyed");
            gameManager.GetScore(enemyScore);
            Destroy(gameObject);
        }

    }

    public void SetPosition(Vector3 pos){
        transform.position = pos;
    }

    void OnBecameVisible(){
        
    }
}
