using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public AudioSource hitAudioSource;
    public AudioSource dieAudioSource;

    public float lookRadius = 10f;
    [Range(0, 360)]
    public float viewAngle = 100f;

    private UnityEngine.AI.NavMeshAgent agent;
    private Animator animator;

    private Transform target;
    //private AttackHandler handler;
    public string enemyTag = "Enemy";

    CharacterCombat combatManager;
    void Start()
    {
        
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = GetComponent<Animator>();
        combatManager = GetComponent<CharacterCombat>();
        target = Player.instance.transform;
        //handler = GetComponent<AttackHandler>();
        //InvokeRepeating("UpdateTarget", 0f, 0.5f);
        combatManager.OnAttack += OnAttack;
    }

    protected virtual void OnAttack()
    {
        print("attack");
        animator.SetTrigger("attack");
    }

    //void UpdateTarget()
    //{
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
    //    foreach (GameObject enemy in enemies)
    //    {
    //        if (enemy.GetComponent<EnemyController>().Target != null)
    //        {
    //            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
    //            if (distanceToEnemy < lookRadius)
    //            {
    //                //shortestDistance = distanceToEnemy;
    //                target = enemy.transform;
    //                return;
    //            }
    //        }
    //    }

    //}

    void Update()
    {
        // Get the distance to the player
        float distance = Vector3.Distance(target.position, transform.position);

        // If inside the radius
        if (distance <= lookRadius)
        {


            Vector3 dirToTarget = (target.position - transform.position);
            float angle = Vector3.Angle(dirToTarget, transform.forward);
            if (angle < viewAngle * 0.5f)
            {
                //Debug.Log ("in angle");
                RaycastHit hit;
                if (Physics.Raycast(transform.position + transform.up, dirToTarget.normalized, out hit, (int)lookRadius))
                {
                    if (hit.collider.gameObject == target.gameObject)
                    {
                        //Debug.Log ("in sight");
                        // Move towards the player
                        
                        agent.SetDestination(target.position);
                        //agent.stoppingDistance = 2f;
                        
                        if (distance <= agent.stoppingDistance)
                        {
                            // Attack
                            combatManager.Attack(Player.instance.PlayerStats);
                            //handler.Seek(player);
                            FaceTarget();
                            animator.SetBool("walking", false);
                        }
                        else
                        {
                            //mAnimator.SetBool("attack", false);
                            animator.SetBool("walking", true);
                        }
                    }

                }
            }
            
        }
    }


    void FollowTarget()
    {
        
        if (target != null)
        {
            //Debug.Log("follow");
            float distance = Vector3.Distance(target.position, transform.position);
            agent.stoppingDistance = 4f;
            if (distance <= agent.stoppingDistance)
            {
                //              handler.Seek(target);
                //              mAnimator.SetBool ("attack", true);
            }
            else
            {
                //Debug.Log("follow destination");
                agent.SetDestination(target.position);
                animator.SetBool("attack", false);
            }
        }
    }

    // Point towards the player
    void FaceTarget()
    {
        Vector3 direction = (Player.instance.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }



    public Transform Target
    {
        get { return target; }
    }

    public void HitEvent()
    {
        hitAudioSource.Play();
    }

    public void DieEvent()
    {
        dieAudioSource.Play();
    }
}
