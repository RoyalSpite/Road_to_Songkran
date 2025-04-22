using UnityEngine;
using TMPro;
using System;

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

    [Header("MovementIndex")]
    [SerializeField] private int laneIndex = 0;

    [Header("Progress")]
    public int Score = 0;
    public float Distance = 0f;

    public float time = 0f;

    public static float gameProgressModifier = 1f;

    public static bool isGameOver = false;

    void Start(){
        player.transform.position = roadPosition[laneIndex].position;
    }

    // Update is called once per frame
    void Update(){

        // FuelText.SetText("HP : " + player.health);
        if(isGameOver){
            gameProgressModifier = 0;
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
        Distance += Time.deltaTime / 6f;
        DistanceText.SetText(Math.Round(Distance, 2) +" Km");

        if(Distance >= 90){
            // BOSS IMCOMING
            enemySpawner.maxEnemy01 = 0;
            enemySpawner.maxEnemy02 = 0;
        }
        else if(Distance >= 70){
            enemySpawner.maxEnemy01 = enemySpawner.Enemies01Pool.Length;
            enemySpawner.maxEnemy02 = enemySpawner.Enemies02Pool.Length;
        }
        else if(Distance >= 54){
            enemySpawner.maxEnemy01 = enemySpawner.Enemies01Pool.Length / 2;
            enemySpawner.maxEnemy02 = enemySpawner.Enemies02Pool.Length / 2;
        }
        else if(Distance >= 36){
            enemySpawner.maxEnemy01 = enemySpawner.Enemies01Pool.Length / 3;
            enemySpawner.maxEnemy02 = enemySpawner.Enemies02Pool.Length / 3;
        }
        else if(Distance >= 15){
            enemySpawner.maxEnemy01 = enemySpawner.Enemies01Pool.Length / 4;
            enemySpawner.maxEnemy02 = enemySpawner.Enemies02Pool.Length / 4;
        }
        
    }

    public void GetScore(int enemyScore){
        Score += enemyScore * player.scoreMultiplier;
    }
}
