using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    Image spriterenderer;
    public Sprite closed;
    public Sprite opened;
    public GameObject[] stock;
    bool isClosed = true;
    // Start is called before the first frame update
    void Start()
    {
        spriterenderer = this.GetComponent<Image>();
        spriterenderer.sprite = closed;
        if (stock.Length > 0)
        {
            foreach (GameObject item in stock)
            {
                item.SetActive(false);
            }
        }
    }

    public void toggleOpen()
    {
        if (isClosed)
        {
            spriterenderer.sprite = opened;
        }
        else
        {
            spriterenderer.sprite = closed;

        }
        if (stock.Length > 0)
        {
            foreach (GameObject item in stock)
            {
                item.SetActive(isClosed);
            }
        }
        isClosed = !isClosed;
    }
}
