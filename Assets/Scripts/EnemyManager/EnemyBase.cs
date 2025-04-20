using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 3f;

    public float enemyScore;

    [SerializeField] private GameManager gameManager;

    [SerializeField] private int health = 3;
    private Transform player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }

        if (health == 0)
        { 
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player>().health -= 1;
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Bullet"))
        {
            Debug.Log("Hit by Bullet!");
            health -= 1;
        }
    }
}
