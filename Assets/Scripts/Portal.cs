using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Portal : Entity
{
    public GameObject hurtParticle;
    public Image healthBar;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            EnemyAI enemy = collision.GetComponent<EnemyAI>();
            enemy.RecieveDamage(enemy.maxHealth);
            this.RecieveDamage(Mathf.Ceil(enemy.attackDamage/2));
        }
    }

    private void Update()
    {
        healthBar.fillAmount = getHealthNormalized();
    }

    protected override void Hurt()
    {
        Instantiate(hurtParticle, transform.position, Quaternion.identity);
        if(health <= 0)
        {
            //Summon a Particle system

            //Death
            GameManager.instance.Death();
        }
    }


    public float getHealthNormalized()
    {
        return health / maxHealth;
    }
}
