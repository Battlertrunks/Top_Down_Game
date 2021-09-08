using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Rifle_Bullet : MonoBehaviour {

    Transform bullet;
    float age = 0f;
    [SerializeField] float lifeSpan = 5f;

    [SerializeField] float bullet_Velocity = 10f;

    [SerializeField] Transform impactEffect;

    [SerializeField] float damageAmount = 10f;

    Vector3 velocity = new Vector3();

    ParticleSystem trail;

    void Awake() {
        transform.parent = null;
        velocity = transform.forward * bullet_Velocity;
        trail = GetComponentInChildren<ParticleSystem>();
    }

    void Update() {
        //transform.position += velocity * Time.deltaTime;

        age += Time.deltaTime;
        if (lifeSpan < age) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Transform particleImpact = Instantiate(impactEffect, transform.position, transform.rotation);
        trail.transform.parent = null;
        Destroy(gameObject);

        Enemies_Health health = other.GetComponent<Enemies_Health>();
        if (health) {
            health.DamageTaken(damageAmount);
        }
    }
}
