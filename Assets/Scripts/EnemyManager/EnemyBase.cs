using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 3f;

    [SerializeField] protected int enemyScore;
    protected GameManager gameManager;

    protected int baseHealth = 1;
    [SerializeField] public int health;

    protected bool inRange;

    protected Transform player;

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        health = baseHealth;
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

    protected void Spawn(){
        health = baseHealth;
        inRange = false;

    }
}
