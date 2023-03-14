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
    public Color invalidLocation;
    public LayerMask mask;
    bool selected = false;
    AudioSource sfxSource;
    public AudioClip sfxPlace;
    public AudioClip sfxNeedMoney;
    private void Start()
    {
        originalPosition = transform.position;
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        sfxSource = this.gameObject.GetComponent<AudioSource>();
    }
    public bool purchase(Transform position)
    {
        if(GameManager.instance.gold < cost)
        {
            sfxSource.PlayOneShot(sfxNeedMoney);
            return false;
        }
        else
        {
            Instantiate(tower, position.position, Quaternion.identity);
            GameManager.instance.gold -= cost;
            sfxSource.PlayOneShot(sfxPlace);
        }
        return true;
    }

    private void Update()
    {
        FollowMouse();
    }

    private void FollowMouse()
    {
        if (Input.GetButtonDown("Fire1") && Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < size)
        {
            spriteRenderer.color = selectedColor;
            selected = true;
        }
        if (selected)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(Mathf.FloorToInt(transform.position.x) + 0.5f, Mathf.FloorToInt(transform.position.y) + 0.5f, 0);
            Collider2D hit = Physics2D.OverlapCircle(transform.position, .45f, mask);
            if (hit != null)
            {
                spriteRenderer.color = invalidLocation;
                Debug.DrawLine(transform.position, hit.transform.position, Color.red);
            }
            else
            {
                spriteRenderer.color = selectedColor;
            }
        }
        if (Input.GetButtonUp("Fire1") && Vector2.Distance(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition)) < size)
        {
            //Place snapped to grid
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(Mathf.FloorToInt(transform.position.x) + 0.5f, Mathf.FloorToInt(transform.position.y) + 0.5f, 0);

            Collider2D hit = Physics2D.OverlapCircle(transform.position, 0.45f, mask);
            if (hit == null)
            {
                purchase(transform);
            }
            else
            {
                sfxSource.PlayOneShot(sfxNeedMoney);
            }
            transform.position = originalPosition;
            spriteRenderer.color = baseColor;
            selected = false;
        }
    }

}
