using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceSoldier_Projectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    Vector3 vel;

    float age = 0f;
    [SerializeField] float lifeSpan = 4f;

    private void Awake() {
        transform.parent = null;
        vel = transform.forward * projectileSpeed;
    }

    void Update()
    {
        transform.position += vel * Time.deltaTime;

        age += Time.deltaTime;
        if (age > lifeSpan) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Player_Movement player = other.GetComponent<Player_Movement>();
        //if (player) print("hit");
    }
}
