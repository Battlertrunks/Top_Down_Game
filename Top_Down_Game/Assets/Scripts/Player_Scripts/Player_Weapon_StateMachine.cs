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

    void Awake() {
        
    }

    void Update() {
        if (Input.GetButtonDown("Fire1")) {
            ShootBullets();
        }
    }

    void ShootBullets() {
        GameObject bulletTransform = Instantiate(bullet, muzzlePos.position, muzzlePos.rotation);
        Transform flash = Instantiate(muzzleFlash, muzzlePos.position, muzzlePos.rotation);
        Rigidbody rb = bulletTransform.GetComponent<Rigidbody>();
        rb.AddForce(muzzlePos.forward * bulletSpeed, ForceMode.Impulse);
    }
}
