using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    public Transform targetToFollow;

    [HideInInspector] public float transitionSmoothness = .001f;

    void LateUpdate()
    {
        if (targetToFollow) {
            float p = 1 - Mathf.Pow(transitionSmoothness, Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetToFollow.position, p);
        }
    }
}
