using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float maxHealth = 1;
    public Color damageFlash;
    Color defaultColor;
    protected SpriteRenderer spriteRenderer;
    private float _health;
    public AudioClip sfxHurt;
    public AudioClip sfxDie;
    AudioSource sfxSource;
    public float health
    {
        get { return _health; }
        set
        {
            if (value != _health)
            {
                _health = value;
                Hurt();
                if (_health <= 0)
                {
                    Die();
                }
            }
        }
    }

    public virtual void Start()
    {
        _health = maxHealth;
        sfxSource = this.GetComponent<AudioSource>();
        spriteRenderer = this.GetComponent<SpriteRenderer>();
        defaultColor = spriteRenderer.color;
    }
    protected virtual void Hurt()
    {
        if(health > 0)
        {
            sfxSource.PlayOneShot(sfxHurt);
        }
        else
        {
            sfxSource.PlayOneShot(sfxDie);
        }
        //Flash color
        spriteRenderer.color = damageFlash;
        if (health > 0)
        {
            Invoke("resetColor", 0.1f);
        }
    }

    private void resetColor()
    {
        spriteRenderer.color = defaultColor;
    }

    protected virtual void Die()
    {
        Destroy(this.gameObject);
    }

    public virtual void RecieveDamage(float amount)
    {
        health -= amount;
    }
}
