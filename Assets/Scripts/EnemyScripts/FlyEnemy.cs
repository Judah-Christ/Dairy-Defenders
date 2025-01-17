using NavMeshPlus.Extensions;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics.Internal;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField] Transform target;
    private Rigidbody2D rb;
    //private GameManager GM;
    private float speed;
    private GameManager GM;
    private Vector3 moveDirection;
    private EnemyAttack enemyAttack;

    [SerializeField]
    private float _maxSpeed;
    private Animator anim;
    private int knockBackX;
    private bool isAttacking;

    void Start()
    {
        target = FindAnyObjectByType<ObjectiveManager>().transform;
        rb = GetComponent<Rigidbody2D>();
        GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        //speed =0.03f;
        speed =0.009f;
        anim = gameObject.GetComponent<Animator>();
        enemyAttack = gameObject.GetComponentInChildren<EnemyAttack>();
    }

    void Update()
    {
        if (target != null)
        {
            CheckDist();
            rb.position = Vector3.MoveTowards(rb.position, target.position, speed);
            AnimationUpdate();


        }

        if (rb.velocity.magnitude > _maxSpeed)
        {
            rb.velocity = Vector2.zero;
        }

        if (target == null)
        {
            CheckDist();
            return;
        }


    }
    private void CheckDist()
    {
        if (GM.activeObject.Count == 1 && GM.activeObject[0] != null)
        {
            target = GM.activeObject[0];
            return;
        }
        int j = 0;
        float maxDistance = 10000000f;
        for (int i = 0; i < GM.activeObject.Count; i++)
        {
            Transform t = GM.activeObject[i];
            if (t != null)
            {
                float dist = Vector2.Distance(transform.position, t.position);
                if (dist < maxDistance)
                {
                    j = i;
                    maxDistance = dist;
                    target = t;
                }
            }
        }
    }


    private void AnimationUpdate()
    {
        anim.SetFloat("MoveX" , moveDirection.x);
        anim.SetFloat("MoveY", moveDirection.y);
    }


    public void StopMovement()
    {
        anim.SetTrigger("Death");
        speed = 0f;
        target = gameObject.transform;
    }

    public void CollisionDirection(Vector2 direction)
    {
        anim.SetInteger("KnockBackX", ((int)direction.x));
    }

    public void TriggerKnockback()
    {
        anim.SetTrigger("KnockbackAnim");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Objective"))
        {
            if (!isAttacking)
            {
                enemyAttack.StartAttacking(target);
                isAttacking = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Objective"))
        {
            if (isAttacking)
            {
                enemyAttack.StopAttacking(target);
                isAttacking = false;
            }
        }
    }
}
