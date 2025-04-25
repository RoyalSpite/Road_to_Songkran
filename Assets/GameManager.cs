using UnityEngine;
using TMPro;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Player player;
    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private Transform[] roadPosition;
    
    [SerializeField] private GameObject fuelGaugeNeedle;
    private readonly float guageAngle = 40f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI DistanceText;

    [Header("Ending")]
    [SerializeField] private GameObject goodEnd;
    [SerializeField] private GameObject badEnd;

    [Header("MovementIndex")]
    [SerializeField] private int laneIndex = 0;

    [Header("Progress")]
    public int Score = 0;
    public float Distance = 0f;

    public float time = 0f;

    public static float gameProgressModifier = 1f;

    public float[] DistanceProgress;

    public static bool isGameOver = false;

    [SerializeField] GameObject Transition;
    [SerializeField] float TransitionTimer = 0f;

    bool isGood = true;

    void Start(){
        player.transform.position = roadPosition[laneIndex].position;
        gameProgressModifier = 1f;
        isGameOver = false;
    }

    // Update is called once per frame
    void Update(){

        // FuelText.SetText("HP : " + player.health);
        if(TransitionTimer > 0){
            TransitionTimer -= Time.deltaTime * 5;
            Color tmpColor = Transition.GetComponent<Image>().color;
            tmpColor.a += Time.deltaTime * 5;

            Transition.GetComponent<Image>().color = tmpColor; 

            if(TransitionTimer <= 0){
                EndGame.score = Score;
                EndGame.distance = Distance;

                if(isGood){
                    SceneManager.LoadScene("GoodEnd");
                }
                else SceneManager.LoadScene("BadEnd");
            }
            //goodEnd.SetActive(true); // ฉากจบ
            return;
        }
        
        // player movement
        if(player.target == Vector3.zero){

            player.target = player.transform.position;

            if(Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)){
                // up and down

                if(Input.GetKeyDown(KeyCode.W)){
                    laneIndex = Mathf.Max(laneIndex - 1, 0);
                }
                else if(Input.GetKeyDown(KeyCode.S)){
                    laneIndex = Mathf.Min(laneIndex + 1, roadPosition.Length - 1);
                }

                player.target.y = roadPosition[laneIndex].position.y;
                
            }
            else if(Input.GetKeyDown(KeyCode.E)){
                player.UsePowerUP();
            }

        }

        // update fuel
        // draining fuel every second
        player.health -= Time.deltaTime * 0.75f;
        fuelGaugeNeedle.transform.eulerAngles = new Vector3(
            0, 0,
            (guageAngle * 2 * (player.health / player.fullHealth)) - guageAngle
        );

        // update score
        ScoreText.SetText("Score : "+Score);

        // update distance
        Distance += Time.deltaTime * player.carSpeedMultiplier / 6f ;
        DistanceText.SetText(Math.Round(Distance, 2) +" Km");
    
        // enemy progress
        if(!BossSpawner.bossSpawned){
            if(Distance >= 40){
                // BOSS IMCOMING
                enemySpawner.maxEnemy01 = 0;
                enemySpawner.maxEnemy02 = 0;
            }
            else if(Distance >= 30){
                enemySpawner.maxEnemy01 = enemySpawner.Enemies01Pool.Length;
                enemySpawner.maxEnemy02 = enemySpawner.Enemies02Pool.Length;
            }
            else if(Distance >= 25){
                enemySpawner.maxEnemy01 = enemySpawner.Enemies01Pool.Length / 2;
                enemySpawner.maxEnemy02 = enemySpawner.Enemies02Pool.Length / 2;
            }
            else if(Distance >= 20){
                enemySpawner.maxEnemy01 = enemySpawner.Enemies01Pool.Length / 3;
                enemySpawner.maxEnemy02 = enemySpawner.Enemies02Pool.Length / 3;
            }
            else if(Distance >= 10){
                enemySpawner.maxEnemy01 = enemySpawner.Enemies01Pool.Length / 4;
                enemySpawner.maxEnemy02 = enemySpawner.Enemies02Pool.Length / 4;
            }
            else if(Distance >= 3){
                enemySpawner.maxEnemy01 = enemySpawner.Enemies01Pool.Length / 5;
            }
        }

        if(Distance >= 25){
            gameProgressModifier = 2f;
        }

    }

    public void GetScore(int enemyScore){
        Score += enemyScore * player.scoreMultiplier;
    }

    public void CancelEnemySpawn(){
        enemySpawner.maxEnemy01 = 0;
        enemySpawner.maxEnemy02 = 0;
    }

    public void GameOver(){
        gameProgressModifier = 1f;
        isGameOver = true;
        TransitionTimer = 1f;
    }

    public void BadEnd(){
        isGood = false;
        GameOver();
    }

    public void GoodEnd(){
        isGood = true;
        GameOver();
        
    }
}
