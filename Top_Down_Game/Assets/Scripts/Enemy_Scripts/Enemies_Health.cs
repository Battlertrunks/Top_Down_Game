using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies_Health : MonoBehaviour {

    [SerializeField] float enemyMaxHealth = 50f;
    public float enemyhealth  { get; private set; }

    void Start() {
        enemyhealth = enemyMaxHealth;
    }

    void Update() {
        
    }

    public void DamageTaken(float damage) {
        enemyhealth -= damage;

        if (enemyhealth <= 0) {
            Destroy(gameObject, 3);
        }
    }
}
