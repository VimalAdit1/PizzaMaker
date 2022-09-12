using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuBoard : Item
{
    public string order;
    GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public override void Interact()
    {
        gameManager.AddOrder(order);
    }

}
