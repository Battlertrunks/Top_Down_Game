using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Weapon_StateMachine : MonoBehaviour {

    static class States {
        public class State {

        }
    }



    [SerializeField] Transform muzzlePos;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzleFlash;

    [SerializeField] float bulletSpeed = 15f;
    [SerializeField] float roundsPerSec = 10;
    float rateOfFire = 0;

    void Awake() {
        
    }

    void Update() {
        if (rateOfFire > 0) rateOfFire -= Time.deltaTime;

        if (Input.GetButton("Fire1")) {
            ShootBullets();
        }
    }

    void ShootBullets() {
        if (rateOfFire > 0) return;

        GameObject bulletTransform = Instantiate(bullet, muzzlePos.position, muzzlePos.rotation);
        Transform flash = Instantiate(muzzleFlash, muzzlePos.position, muzzlePos.rotation);
        Rigidbody rb = bulletTransform.GetComponent<Rigidbody>();
        rb.AddForce(muzzlePos.forward * bulletSpeed, ForceMode.Impulse);

        rateOfFire = 1 / roundsPerSec;
    }
}
