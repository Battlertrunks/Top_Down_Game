using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursorAim : MonoBehaviour
{
    Camera cam;

    public Transform debugObj;
    [HideInInspector] public Transform lookingDirection;

    // Start is called before the first frame update
    void Start() {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update() {
        AimWithMouse();
    }

    void AimWithMouse() {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if (plane.Raycast(ray, out float distance)) {
            Vector3 hitPos = ray.GetPoint(distance);

            if (debugObj) debugObj.position = hitPos;

            Vector3 vectorToHitPosition = hitPos - transform.position;

            float angle = Mathf.Atan2(vectorToHitPosition.x, vectorToHitPosition.z);
            angle /= Mathf.PI;
            angle *= 180;

            transform.eulerAngles = new Vector3(0, angle, 0);
            lookingDirection = transform;
        }
    }
}
