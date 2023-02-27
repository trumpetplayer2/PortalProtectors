using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTrail : MonoBehaviour
{
    public AudioClip killEnemy;
    public int moveSpeed = 170;
    public string EnemyTag = "Enemy";
    public string[] ignoreTags = new string[0];
    public float lifeTime = 1f;
    public int damage = 1;
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * moveSpeed);
        Destroy(gameObject, lifeTime);
    }

    public void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        //Spawn hit particle
        if(ignoreTags.Length > 0)
        {
            foreach(string tag in ignoreTags)
            {
                if (collision.gameObject.tag.Equals(tag))
                {
                    return;
                }
            }
        }
        //Deal Damage

        //Despawn
        Destroy(this.gameObject);
    }
}
