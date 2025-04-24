using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public float moveSpeed = 3f;

    [SerializeField] protected int enemyScore;
    protected GameManager gameManager;

    protected int baseHealth = 1;
    [SerializeField] public float health;

    protected bool inRange;

    protected Transform player;

    private Vector3 CollidedDir = Vector3.zero;
    protected float fadeOutCountDown = 0f;
    protected float fadeOutTimeMult = 2;
    protected EnemySoundController sound;


    [SerializeField] GameObject HitMark;
    private float HitCountDown = -1;

    protected void Awake()
    {
        sound = GetComponent<EnemySoundController>();
    }

    protected void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        health = baseHealth;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        sound.enabled = true;

    }

    protected void Update()
    {

        if(HitCountDown > -1){
            HitCountDown += Time.deltaTime;
            if(HitCountDown >= 0.25){
                HitMark.SetActive(false);
                HitCountDown = -1;
            }
        }

        if (health <= 0){   
            
            Color tmpColor = gameObject.GetComponent<SpriteRenderer>().color;
            transform.Translate(4 * Time.deltaTime * -CollidedDir);

            if(tmpColor.a > 0f){
                tmpColor.a -= Time.deltaTime * fadeOutTimeMult;
                gameObject.GetComponent<SpriteRenderer>().color = tmpColor;
            }
            else{ 
                sound.PlayHitSound();
                gameManager.GetScore(enemyScore);
                gameObject.SetActive(false);
            }

            return;
        }
        
    }

    protected void Spawn(){
        health = baseHealth * GameManager.gameProgressModifier;
        inRange = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
    }

    public void Hit(float Damage){
        health -= Damage;
        HitCountDown = 0f;
        HitMark.SetActive(true);
    }

}
