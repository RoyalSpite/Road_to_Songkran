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

    private Vector3 CollidedDir;
    protected float fadeOutCountDown = 0f;
    protected float fadeOutTimeMult = 2;

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        health = baseHealth;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

    }

    protected void Update()
    {

        if (health <= 0){   
            
            Color tmpColor = gameObject.GetComponent<SpriteRenderer>().color;
            transform.Translate(4 * Time.deltaTime * -CollidedDir);

            if(tmpColor.a > 0f){
                tmpColor.a -= Time.deltaTime * fadeOutTimeMult;
                gameObject.GetComponent<SpriteRenderer>().color = tmpColor;
            }
            else{ 
                gameManager.GetScore(enemyScore);
                gameObject.SetActive(false);
            }

            return;
        }

    }

    protected void Spawn(){
        health = baseHealth;
        inRange = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void SetCollidedPos(Vector3 Collide){
        CollidedDir = Collide;
    }
}
