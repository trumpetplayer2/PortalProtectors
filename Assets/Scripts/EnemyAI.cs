using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : Entity
{
    public PathScript next;
    public float speed;
    public float attackDamage;
    public bool trapImmune;
    float _speed;
    void Update()
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

    private void OnDestroy()
    {
        GameManager.instance.incrementGold(Mathf.FloorToInt((attackDamage + _speed) / 2));
    }
}
