using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] public GameObject[] Enemies01Pool;
    [SerializeField] public GameObject[] Enemies02Pool;

    [SerializeField] public int maxEnemy01 = 3;
    [SerializeField] public int maxEnemy02 = 3;

    [SerializeField] private Transform UpperLane;
    [SerializeField] private Transform LowerLane;

    // spawing time
    [SerializeField] private int[] Enemy01SpawnTime;
    [SerializeField] private int[] Enemy02SpawnTime;

    private float countDown = 0;

    [SerializeField] int TimeToSpawn01;
    [SerializeField] int TimeToSpawn02;


    private float SpawnThreshold = 1.25f;

    void Start(){
        
        // initial spawnTime for each type of enemies

        Enemy01SpawnTime = new int[Enemies01Pool.Length];
        Enemy02SpawnTime = new int[Enemies02Pool.Length];

        maxEnemy01 = 0;
        maxEnemy02 = Enemies02Pool.Length / 5;

        for(int i = 0 ; i < Enemy01SpawnTime.Length ; i ++){
            Enemy01SpawnTime[i] = i;
        }


    }

    // Update is called once per frame
    void Update(){

        if(GameManager.isGameOver) return;

        countDown += Time.deltaTime;

        TimeToSpawn01 = Mathf.RoundToInt(countDown * 2 * 10);
        for(int i = 0 ; i < maxEnemy01 ; i++){
            if(TimeToSpawn01 == Enemy01SpawnTime[i]){
                if(!Enemies01Pool[i].activeSelf){
                    Enemies01Pool[i].SetActive(true);
                    
                    Enemies01Pool[i].GetComponent<Enemy01>().Spawn(
                        (TimeToSpawn01 < 10)? UpperLane.position : LowerLane.position
                    );
                }
            }
        }

        TimeToSpawn02 = Mathf.RoundToInt(countDown * 10);
        for(int i = 0 ; i < maxEnemy02 ; i++){
            if(TimeToSpawn02 == Enemy02SpawnTime[i]){
                if(!Enemies02Pool[i].activeSelf){
                    Enemies02Pool[i].SetActive(true);
                    Enemies02Pool[i].GetComponent<Enemy02>().Spawn(
                        new Vector3(
                            9.5f,
                            Random.Range(2.45f, 4f)
                        )
                    );
                }
            }           
        }

        if(countDown >= SpawnThreshold){
            // create new spawntime
            SetSpawnTime();
            countDown = 0;
        }
    }

    void SetSpawnTime(){

        for (int t = 0; t < Enemy01SpawnTime.Length; t++ ){
            int tmp = Enemy01SpawnTime[t];
            int r = Random.Range(t, Enemy01SpawnTime.Length);
            Enemy01SpawnTime[t] = Enemy01SpawnTime[r];
            Enemy01SpawnTime[r] = tmp;
        }
        

        if(maxEnemy02 > 0){
            for(int i=0; i< Enemy02SpawnTime.Length ; i++){
                Enemy02SpawnTime[i] = Mathf.RoundToInt(Random.Range(0f, 1) * 10) ;
            }
        }

    }
}
