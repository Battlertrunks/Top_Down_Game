using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpaceSoldier : MonoBehaviour
{
    NavMeshAgent _navMeshAgent;
    [SerializeField] float attackRange = 3f; // How far enemy can attack
    [SerializeField] Transform muzzlePos;

    Animator _animator;

    [SerializeField] SpaceSoldier_Projectile projectile_Bullet;

    float laserAmountShot;
    [SerializeField] float roundsPerSecond = 30;

    Enemies_Health healthAmount;

    CapsuleCollider basicCapsule;


    private void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        healthAmount = GetComponent<Enemies_Health>();

        basicCapsule = GetComponent<CapsuleCollider>();
    }

    void Update() {
        if (healthAmount.enemyhealth <= 0) {
            DeathAnimation();
            return;
        }

        if (laserAmountShot > 0) laserAmountShot -= Time.deltaTime;

        Player_Movement playerLocation = FindObjectOfType<Player_Movement>();
        

        if (Vector3.Distance(transform.position, playerLocation.transform.position) < attackRange) {
            _navMeshAgent.isStopped = true;

            LookAtTarget(playerLocation.transform);
            AttackAnimation();
            ShootingAttack();
        } else {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(playerLocation.transform.position);
        }

    }

    void LookAtTarget(Transform target) {

        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRot = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRot, Time.deltaTime * 5);
    }

    void AttackAnimation() {
        _animator.SetTrigger("AttackIdle");
    }

    void AttackComplete() {

    }

    void ShootingAttack() {
        if (laserAmountShot > 0) return;
        SpaceSoldier_Projectile lasers = Instantiate(projectile_Bullet, muzzlePos.position, muzzlePos.rotation);

        laserAmountShot = 1 / roundsPerSecond;
    }

    void DeathAnimation() {
        _navMeshAgent.isStopped = true;
        _animator.SetBool("NoHealth", true);
        basicCapsule.enabled = false;
    }
}
