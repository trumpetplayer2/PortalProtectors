using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float maxHealth = 1;
    private float _health;
    public float health
    {
        get { return _health; }
        set
        {
            _health = value;
            Hurt();
            if(_health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

    public virtual void Start()
    {
        _health = maxHealth;
    }
    protected virtual void Hurt()
    {

    }

    public virtual void RecieveDamage(float amount)
    {
        health -= amount;
    }
}
