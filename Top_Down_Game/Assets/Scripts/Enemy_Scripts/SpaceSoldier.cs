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
    [SerializeField] float roundsPerSecond = 30f;
    [SerializeField] float rangeToChase = 10f;

    Enemies_Health healthAmount;

    CapsuleCollider basicCapsule;
    Player_Movement enemyPlayer;

    bool hasDied = true;


    private void Awake() {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        healthAmount = GetComponent<Enemies_Health>();

        basicCapsule = GetComponent<CapsuleCollider>();

        enemyPlayer = FindObjectOfType<Player_Movement>();
    }

    void Update() {
        if (healthAmount.enemyhealth <= 0) {
            if (hasDied) {
                DeathAnimation();
                hasDied = false;
            }
            return;
        }

        if (laserAmountShot > 0) laserAmountShot -= Time.deltaTime;

        if (Vector3.Distance(transform.position, enemyPlayer.transform.position) < rangeToChase)
            ChasingTarget();

    }

    void ChasingTarget() {
        if (Vector3.Distance(transform.position, enemyPlayer.transform.position) < attackRange) {
            _navMeshAgent.isStopped = true;

            LookAtTarget(enemyPlayer.transform);
            AttackAnimation();
            ShootingAttack();
        } else {
            _navMeshAgent.isStopped = false;
            _navMeshAgent.SetDestination(enemyPlayer.transform.position);
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

    void ShootingAttack() {
        if (laserAmountShot > 0) return;
        SpaceSoldier_Projectile lasers = Instantiate(projectile_Bullet, muzzlePos.position, muzzlePos.rotation);

        laserAmountShot = 1 / roundsPerSecond;
    }

    void DeathAnimation() {
        _navMeshAgent.isStopped = true;
        _animator.SetBool("NoHealth", true);
        basicCapsule.enabled = false;

        Enemy_Spawner_1 enemyDied = FindObjectOfType<Enemy_Spawner_1>();
        enemyDied.EnemyKilled();
    }
}
