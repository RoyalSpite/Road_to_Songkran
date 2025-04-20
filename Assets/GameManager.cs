using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Player player;
    [SerializeField] private Transform[] roadPosition;

    [SerializeField] private GameObject fuelGaugeNeedle;
    private readonly float guageAngle = 40f;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI ScoreText;
    [SerializeField] private TextMeshProUGUI DistanceText;

    [Header("MovementIndex")]
    [SerializeField] private int laneIndex = 0;

    private int Score = 0;
    private float Distance = 0f;

    public static float gameProgressModifier = 1f;

    void Start(){
        player.transform.position = roadPosition[laneIndex].position;
    }

    // Update is called once per frame
    void Update(){

        // FuelText.SetText("HP : " + player.health);
        
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
        player.health -= Time.deltaTime * 0.5f;
        fuelGaugeNeedle.transform.eulerAngles = new Vector3(
            0, 0,
            (guageAngle * 2 * (player.health / player.fullHealth)) - guageAngle
        );

        // update score
        ScoreText.SetText("Score : "+Score);

        // update distance
        Distance += Time.deltaTime * player.carSpeedMultiplier;
        DistanceText.SetText(Math.Round(Distance, 1) +" Km");

    }

    public void GetScore(int enemyScore){
        Score += enemyScore * player.scoreMultiplier;
    }
}
