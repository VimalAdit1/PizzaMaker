
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public Order currentOrder;
    public TextMeshPro dialogPanel;
    public TextMeshPro orderPanel;
    public TextMeshPro outputScreen;
    public string order;
    public string[] instructions;
    public GameConstants.Crust crust;
    public GameConstants.Toppings[] toppings;
    public GameConstants.Cheese cheese;
    public GameConstants.Extra[] extra;
    // Start is called before the first frame update
    void Start()
    {
        order = ConstructOrder();
        //GenerateNewOrder();
        Debug.Log(order);
        this.AddOrder(order);
    }
    public void AddOrder(string order)
    {
        this.currentOrder = new Order(order);
        Debug.Log(currentOrder.GetInstructions());
        this.instructions = this.currentOrder.GetInstructions().Split('\n');
        this.dialogPanel.text = this.instructions[0].ToString();
        this.currentOrder.startTime = Time.time;
        this.orderPanel.text = order;
        updateOutputScreen(this.currentOrder.GetInstructions());
    }
    public string ConstructOrder()
    {
        string order = "";
        order += crust;
        order += "-";
        foreach(GameConstants.Toppings topping in toppings)
        {
            order += topping + ",";
        }
        order += "-";
        order += cheese;
        order += "-";
        foreach (GameConstants.Extra item in extra)
        {
            order += item + ",";
        }
        return order;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            GenerateNewOrder();
        }
    }
    public void progress(int step)
    {
        if(step==GameConstants.crustPlaced)
        {
            currentOrder.startTime = Time.time;
        }
        if (step == GameConstants.delivered)
        {
            currentOrder.endTime = Time.time;
        }
        if (currentOrder.currentStep < step)
        {
            currentOrder.currentStep = step;
            dialogPanel.text = instructions[currentOrder.currentStep].ToString();
        }
    }

    public void GenerateNewOrder()
    {
        string crusttype = "";
        string toppingsRequired = "";
        string cheeseRequired = "";
        string extraRequired = "Pizza,With extra";
        crusttype += (GameConstants.Crust)Random.Range(0, 2);
        int noOfToppings = Random.Range(1, 3);
        for(int i=0;i<noOfToppings;i++)
        {
            string topping =""+ (GameConstants.Toppings)Random.Range(0, 3);
            if(!toppingsRequired.Contains(topping))
            {
                toppingsRequired += topping + ",";
            }
        }
        cheeseRequired += (GameConstants.Cheese)Random.Range(0, 3);
        int isExtra = Random.Range(1, 100);
        if (isExtra > 80)
        {
            int noOfExtra = Random.Range(1, 4);
            for (int i = 0; i < noOfExtra; i++)
            {
                string extraItem = "" + (GameConstants.Extra)Random.Range(0, 3);
                if (!extraRequired.Contains(extraItem))
                {
                    extraRequired += extraItem + ",";
                }
            }
        }
        else
        {
            extraRequired = "Pizza";
        }
        string order = crusttype + "-" + toppingsRequired + "-" + cheeseRequired + "-" + extraRequired;
        Debug.Log(order);
        this.AddOrder(order);
    }

    public void updateOutputScreen(string text)
    {
        this.outputScreen.text = text;
    }
}
