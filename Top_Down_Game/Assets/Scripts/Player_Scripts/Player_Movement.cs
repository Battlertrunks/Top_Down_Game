using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{

    static class States {
        public class State {

            protected Player_Movement playerPawn;

            virtual public State Update() {
                return null;
            }

            virtual public void OnStart(Player_Movement player) {
                this.playerPawn = player;
            }

            virtual public void OnEnd() {

            }
        }
        // Child classes:

        public class Idle : State {
            public override State Update() {
                // Behavior:
                playerPawn.Idle_Pawn();
                // Translations:
                if (Input.GetKey("w") || Input.GetKey("a") || Input.GetKey("s") || Input.GetKey("d"))
                    return new States.Walk(); // goes to walking state/moving around.

                return null;
            }

        }
        public class Walk : State {
            public override State Update() {
                playerPawn.Walking_Pawn();
                if (!Input.GetKey("w") && !Input.GetKey("a") && !Input.GetKey("s") && !Input.GetKey("d"))
                    return new States.Idle();

                if (Input.GetKey("left ctrl")) {
                    playerPawn.isSlow = true;
                    return new States.SlowAim();
                }

                return null;
            }
        }

        public class SlowAim : State {
            public override State Update() {
                // Behavior:
                playerPawn.Slow_Pawn();

                // Translation:
                if (!Input.GetKey("left ctrl")) {
                    playerPawn.isSlow = false;
                    return new States.Walk();
                } 
                return null;
            }
        }
        
    }

    private States.State currentState;

    private Vector3 moveVec;
    private CharacterController playerPawnModel;

    [SerializeField] float walkingSpeed = 5f;
    Animator _animator;

    Transform cam;
    Vector3 camForward;
    Vector3 move;
    Vector3 moveInput;

    float forwardAmount;
    float turnAmount;

    PlayerCursorAim lookingAt;
    bool isSlow = false;


    // Start is called before the first frame update
    void Start() {
        playerPawnModel = gameObject.GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();

        cam = Camera.main.transform;
    }

    // Update is called once per frame
    void Update() {
        if (currentState == null) SwitchingStates(new States.Idle());
        if (currentState != null) SwitchingStates(currentState.Update());
    }

    private void SwitchingStates(States.State newState) {
        if (newState == null) return;
        if (currentState != null) currentState.OnEnd();

        currentState = newState;
        currentState.OnStart(this);
    }

    void MovementAction() {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        move = v * Vector3.forward + h * Vector3.right;
        if (move.magnitude > 1) {
            move.Normalize();
        }


        moveVec = new Vector3(h, 0f, v);
        moveVec *= Time.deltaTime * walkingSpeed;
        if (moveVec.sqrMagnitude > 1) moveVec.Normalize();

        //UpdateAnimation();

        // _animator.SetFloat("VelocityX", h, 0.1f, Time.deltaTime);
        // _animator.SetFloat("VelocityZ", v, 0.1f, Time.deltaTime);
        ConvertMoveInput();
        UpdateAnimator();
    }

    void ConvertMoveInput() {
        Vector3 localMove = transform.InverseTransformDirection(move);

        if (isSlow == true) {
            localMove.x /= 2;
            localMove.z /= 2;
        }
        print(localMove);
        turnAmount = localMove.x;

        forwardAmount = localMove.z;
    }

    void UpdateAnimator() {
        _animator.SetFloat("VelocityX", turnAmount, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityZ", forwardAmount, 0.1f, Time.deltaTime);
    }

    //void UpdateAnimation() {
        //float forwardsBackwardsMagnitude = 0;
        //float rightLeftMagnitude = 0;
        //if (moveVec.magnitude > 0) {
        //    PlayerCursorAim lookingAt = GetComponent<PlayerCursorAim>();
        //    Vector3 normalizedLookingAt = lookingAt.lookingDirection.position - transform.position;
        //    normalizedLookingAt.Normalize();

        //    forwardsBackwardsMagnitude = Mathf.Clamp(Vector3.Dot(moveVec, normalizedLookingAt), -1, 1);

        //    Vector3 perpendicularLookAt = new Vector3(normalizedLookingAt.z, 0, -normalizedLookingAt.x);
        //    rightLeftMagnitude = Mathf.Clamp(Vector3.Dot(moveVec, perpendicularLookAt), -1, 1);


        //}
    //}

    void Walking_Pawn() {
        MovementAction();
        transform.Translate(moveVec, Space.World);
    }

    void Slow_Pawn() {
        MovementAction();
        transform.Translate(moveVec / 2f, Space.World);
    }

    void Idle_Pawn() {
        _animator.SetFloat("VelocityX", 0, 0.1f, Time.deltaTime);
        _animator.SetFloat("VelocityZ", 0, 0.1f, Time.deltaTime);
    }
}
