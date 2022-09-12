using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : Base
{
    public GameObject normal;
    public GameObject deep;
    GameManager gameManager;
    public Pickup pickup;

    // Start is called before the first frame update
    void Start()
    {
        hideAll();
        gameManager = FindObjectOfType<GameManager>();
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (!this.containsItem)
        {
            if (other.CompareTag(GameConstants.player))
            {
                if (pickup.itemName.Equals(GameConstants.normal))
                {
                    normal.SetActive(true);
                }
                else if (pickup.itemName.Equals(GameConstants.deep))
                {
                    deep.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (!this.containsItem)
        {
            if (other.CompareTag(GameConstants.player))
            {
                if (pickup.itemName.Equals(" "))
                {
                    hideAll();
                }
                if (pickup.itemName.Equals(GameConstants.normal))
                {
                    normal.SetActive(true);
                }
                else if (pickup.itemName.Equals(GameConstants.deep))
                {
                    deep.SetActive(true);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(GameConstants.player))
        {
            hideAll();
        }
    }
    public override  void ObjectPlaced(string objectName)
    {
        hideAll();
        gameManager.progress(GameConstants.crustPlaced);
        this.containsItem = true;

    }
    void hideAll()
    {
        normal.SetActive(false);
        deep.SetActive(false);
    }

    public GameObject RemovePizza()
    {
        Pizza pizza = this.GetComponentInChildren<Pizza>();
        GameObject pizzaObject = pizza.gameObject;
        pizzaObject.transform.parent = null;
        this.containsItem = false;
        return pizzaObject;
    }
}
