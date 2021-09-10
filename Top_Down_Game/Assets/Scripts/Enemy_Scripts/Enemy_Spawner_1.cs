using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner_1 : MonoBehaviour {

    [SerializeField] Transform enemyToSpawn;

    [SerializeField] Transform[] spawnPoints;

    public int numberOfEnemiesOnTheField { get; private set; }
    [SerializeField] int maxEnemiesOnField = 15;

    private void Awake() {
        numberOfEnemiesOnTheField = 0;
    }

    void Update() {
        SpawningEnemies();
    }

    void SpawningEnemies() {
        if (numberOfEnemiesOnTheField <= maxEnemiesOnField) {
            for (int i = 0; i < 4; i++) {
                if (numberOfEnemiesOnTheField >= maxEnemiesOnField) return;
                Instantiate(enemyToSpawn, spawnPoints[i].position, Quaternion.identity);
                numberOfEnemiesOnTheField++;
            }
        }
    }

    public void EnemyKilled(int enemyLost = 1) {
        numberOfEnemiesOnTheField -= enemyLost;
    }
}
