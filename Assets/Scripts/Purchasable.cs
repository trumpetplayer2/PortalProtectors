using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Purchasable : MonoBehaviour
{
    public int cost;
    public float size = 0.5f;
    public GameObject tower;
    private Vector3 originalPosition;
    SpriteRenderer spriteRenderer;
    public Color baseColor;
    public Color selectedColor;
    private void Start()
    {
        originalPosition = transform.position;
        spriteRenderer = this.GetComponent<SpriteRenderer>();
    }
    public bool purchase(Transform position)
    {
        if(GameManager.instance.gold < cost)
        {
            return false;
        }
        else
        {
            Instantiate(tower, position.position, Quaternion.identity);
            GameManager.instance.gold -= cost;
        }
        return true;
    }

    private void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        if (Input.GetButton("Fire1") && Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < size)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            spriteRenderer.color = selectedColor;
        }
        if (Input.GetButtonUp("Fire1") && Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < size)
        {
            //Check if the location is valid

            //Place snapped to grid
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(Mathf.FloorToInt(transform.position.x) + 0.5f, Mathf.FloorToInt(transform.position.y) + 0.5f, 0);

            purchase(transform);

            transform.position = originalPosition;
            spriteRenderer.color = baseColor;
        }
    }

}
