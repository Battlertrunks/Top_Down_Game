using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleFlash_Life_and_Effects : MonoBehaviour {

    float age = 0f;
    [SerializeField] float lifeTime = 1f;

    void Update() {
        age += Time.deltaTime;
        if (lifeTime < age)
            Destroy(gameObject);
    }
}
