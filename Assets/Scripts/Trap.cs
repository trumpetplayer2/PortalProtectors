using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : Tower
{
    public float speedMult = 1;
    public float effectTime = 0;
    protected override void checkForEnemies()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, attackRadius, filter);
        if (collisions == null) { return; }
        if (collisions.Length < 1) { return; }
        foreach (Collider2D hit in collisions)
        {
            if (hit.tag == "Enemy")
            {
                EnemyAI enemy = hit.GetComponent<EnemyAI>();
                if (!enemy.trapImmune)
                {
                    enemy.RecieveDamage(attackDamage);
                    this.RecieveDamage(attackDamage);
                    if(effectTime > 0)
                    {
                        enemy.lowerSpeed(speedMult, effectTime);
                        this.RecieveDamage(2 * speedMult);
                    }
                }
            }
        }
    }

    protected override void Hurt()
    {
        spriteRenderer.color = damageFlash;
        if (health > 0)
        {
            Invoke("resetColor", 0.1f);
        }
    }
}
