using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner_1 : MonoBehaviour {

    [SerializeField] Transform enemyToSpawn;

    [SerializeField] Transform[] spawnPoints;

    public int numberOfEnemiesOnTheField { get; private set; }
    [SerializeField] int maxEnemiesOnField = 15;
    float enemyTimeSpawn = 0f;
    [SerializeField] float timeSet = 5f; 

    private void Awake() {
        numberOfEnemiesOnTheField = 0;
        enemyTimeSpawn = timeSet;
    }

    void Update() {
        SpawningEnemies();

        if (enemyTimeSpawn > 0) enemyTimeSpawn -= Time.deltaTime;
    }

    void SpawningEnemies() {
        if (numberOfEnemiesOnTheField < maxEnemiesOnField && enemyTimeSpawn < 0) {
            for (int i = 0; i < 4; i++) {
                if (numberOfEnemiesOnTheField >= maxEnemiesOnField) return;
                Instantiate(enemyToSpawn, spawnPoints[i].position, Quaternion.identity);
                numberOfEnemiesOnTheField++;
            }
        }

        if (enemyTimeSpawn <= 0) {
            enemyTimeSpawn = timeSet;
            print("reset");
        }
    }

    public void EnemyKilled(int enemyLost = 1) {
        numberOfEnemiesOnTheField -= enemyLost;
    }
}
