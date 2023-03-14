using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : Entity
{
    public float attackDamage;
    public float attackSpeed;
    float cooldown;
    public float attackRadius;
    public LayerMask filter;
    public GameObject healthBar;

    protected virtual void Awake()
    {
        cooldown = Time.time + attackSpeed;
    }

    protected virtual void Update()
    {
        if (cooldown < Time.time)
        {
            checkForEnemies();
            cooldown = Time.time + attackSpeed;
        }
        healthBar.transform.localScale = new Vector3(Mathf.Clamp(health/maxHealth, 0, 1), healthBar.transform.localScale.y, healthBar.transform.localScale.z);
    }

    protected virtual void checkForEnemies()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, attackRadius, filter);
        if (collisions == null) { return; }
        if (collisions.Length < 1) { return; }
        foreach (Collider2D hit in collisions)
        {
            if (hit.tag == "Enemy")
            {
                Entity enemy = hit.GetComponent<Entity>();
                enemy.RecieveDamage(attackDamage);
            }
        }
    }
}
