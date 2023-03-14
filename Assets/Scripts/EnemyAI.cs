using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Entity
{
    public PathScript next;
    public float speed;
    public float attackDamage;
    public float attackSpeed;
    private float attackCooldown;
    public float attackRadius;
    public bool trapImmune;
    float _speed;
    public bool move = true;
    public Animator anim;
    public LayerMask filter;
    public int cost;
    public int minWave;
    bool inBattle = false;

    private void Awake()
    {
        next = GameManager.instance.startLocation;
    }
    void Update()
    {
        if (health > 0)
        {
            //Check if tower nearby
            TowerCheck();
            if (move)
            {
                float distance = speed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, next.gameObject.transform.position, distance);
                if (Vector2.Distance(transform.position, next.transform.position) < 0.1)
                {
                    if (next.next != null)
                    {
                        next = next.next;
                    }
                    else
                    {
                        Destroy(this);
                    }
                }
            }
            else
            {
                //Attacking
                if (attackCooldown <= Time.time)
                {
                    attackCooldown = Time.time + attackSpeed;
                    //Attack
                    attack();
                }
            }
        }
        else
        {
            this.gameObject.transform.Rotate(Vector3.forward * 100 * Time.deltaTime);
        }
    }

    public void TowerCheck()
    {
        bool temp = true;
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, attackRadius, filter);
        foreach(Collider2D hit in collisions)
        {
            if(hit.tag == "Tower")
            {
                temp = false;
                if(inBattle == false)
                {
                    attackCooldown = Time.time + attackSpeed;
                }
            }
        }
        move = temp;
        if(anim != null)
        {
            anim.SetBool("Walking", move);
            inBattle = !move;
        }
    }

    public void attack()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, attackRadius, filter);
        if (collisions == null) { return; }
        if (collisions.Length < 1) { return; }
        foreach (Collider2D hit in collisions)
        {
            if (hit.tag == "Tower")
            {
                Entity tower = hit.GetComponent<Entity>();
                tower.RecieveDamage(attackDamage);
            }
        }
    }

    protected override void Die()
    {
        Destroy(this.gameObject, 0.5f);
    }
    private void OnDestroy()
    {
        GameManager.instance.incrementGold(Mathf.FloorToInt((attackDamage + _speed)));
    }
}
