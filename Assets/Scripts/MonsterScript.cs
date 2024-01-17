using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MonsterScript : MonoBehaviour
{
    Animator anim;
    [SerializeField] Transform Crystal;

    public NavMeshAgent enemyAgent;
    Transform player;                    //yapayzeka düþmanýmýzýn oyuncumuzun konumunu belirlemesi için gereken transform
    public LayerMask ground;             //yapayzeka düþmanýmýzýn ilerleyeceði zemin
    public LayerMask playerGround;
    public LayerMask crystalGround;

    //patrolling
    public Vector3 walkPoint;
    public float walkPointRange;
    public bool walkPointSet;

    //player detected
    public float sightRange, attackRange;
    public bool isInSightRange, isInAttackRange, isAttackingCrystal;

    //attack
    public float attackDelay;
    public bool isAttacking;



    void Start()
    {
        enemyAgent = GetComponent<NavMeshAgent>(); 
        player = GameObject.Find("Player").transform;
        Crystal = GameObject.Find("Crystal").transform;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isInSightRange = Physics.CheckSphere(this.transform.position, sightRange, playerGround);
        isInAttackRange = Physics.CheckSphere(this.transform.position, attackRange, playerGround);
        isAttackingCrystal = Physics.CheckSphere(this.transform.position, attackRange, crystalGround);

        if (isAttackingCrystal && !isInSightRange && !isAttacking)
        {
            ResetAttack();
            AttackPlayer();
        }
        else if (!isInSightRange && !isInAttackRange && !isAttackingCrystal)
        {
            Patrolling();
        }
        else if (isInSightRange && !isInAttackRange)
        {
            DetectPlayer();
        }
        else if (isInSightRange && isInAttackRange)
        {
            AttackPlayer();
        }
    }

    void Patrolling()
    {
        if (walkPointSet == false)
        {
            walkPoint = Crystal.position;
        
            if (Physics.Raycast(walkPoint, -transform.up, 2f, ground))
            {
                walkPointSet = true;
            }
        }
        if (walkPointSet == true)
        {
            enemyAgent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        if (distanceToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;
        }
    }

    void DetectPlayer()
    {
        enemyAgent.SetDestination(player.position);
        transform.LookAt(player,Vector3.up);
    }

    void AttackPlayer()
    {
        if (isAttackingCrystal)
        {
            enemyAgent.SetDestination(transform.position);
            transform.LookAt(Crystal, Vector3.up);
            if (isAttacking == false)
            {
                isAttacking = true;
                anim.SetBool("isReadyToAttack", isAttacking);
                Invoke("ResetAttack", attackDelay);
            }
        }
        else
        {
            enemyAgent.SetDestination(transform.position);
            transform.LookAt(player, Vector3.up);
            if (isAttacking == false)
            {
                isAttacking = true;
                anim.SetBool("isReadyToAttack", isAttacking);
                Invoke("ResetAttack", attackDelay);
            }
        }
    }
    void ResetAttack()
    {
        isAttacking = false;
        anim.SetBool("isReadyToAttack", isAttacking);
    }
    // gizmo çizimi yaparak checkSphere ve raycast gibi ýþýnlarý editör ekranýnda görünür yapabiliyoruz.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, sightRange);

    }
}
