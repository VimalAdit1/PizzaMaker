using System;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryTray : Base
{
    GameManager gameManager;
    public NavMovement nav;
    public Text outputText;
    public GameObject outputScreen;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        outputScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)&&gameManager.currentOrder.currentStep==GameConstants.delivered)
        {
            outputScreen.SetActive(false);
            gameManager.GenerateNewOrder();
            nav.Deliver();
            this.containsItem = false;
        }
    }

    public override void ObjectPlaced(GameObject objectName)
    {
        Order order = gameManager.currentOrder;
        objectName.transform.parent = this.transform;
        objectName.transform.localPosition = Vector3.zero;
        Pizza pizza = objectName.GetComponentInChildren<Pizza>();
        pizza.UpdateTime();
        string result = validateOrder(order, pizza);
        outputText.text = result;
        gameManager.updateOutputScreen(result);
        if (order.currentStep.Equals(GameConstants.baked))
        {
            gameManager.progress(GameConstants.delivered);
            Destroy(pizza.gameObject);

            //outputScreen.SetActive(true);
        }
        this.containsItem = true;
    }

    private string validateOrder(Order order, Pizza pizza)
    {
        string result="";
        float score = 100f;
        try
        {
            if (!order.crustType.Equals(pizza.crustType))
            {
                result += "Asked " + order.crustType + ", but " + pizza.crustType + " is used\n";
                score -= 10f;
            }
            if (pizza.tracker[GameConstants.sauce] > 2.5f)
            {
                result += "Too much sauce\n";
                score -= (1.5f - pizza.tracker[GameConstants.sauce]) * 10f;
            }
            if (pizza.tracker[GameConstants.cheeseTracker] > 2.5f)
            {
                result += "Too much cheese\n";
                score -= (1.5f-pizza.tracker[GameConstants.cheeseTracker])*10f;
            }
            if (!pizza.tracker.ContainsKey(order.cheeseType))
            {
                result += "Asked " + order.cheeseType + ", but " + pizza.GetCheeseUsed() + " is used\n";
                score -= 10f;
            }
            string[] toppings = order.toppings.Split(',');
            foreach (string topping in toppings)
            {
                if (pizza.tracker.ContainsKey(topping))
                {
                    if (pizza.tracker[topping] > 2 && !order.extras.Contains(topping))
                    {
                        result += "Too much " + topping + ", added\n";
                        score -= 2f;
                    }
                    else if (pizza.tracker[topping] > 4)
                    {
                        result += "Too much " + topping + ", added\n";
                        score -= 2f;
                    }
                }
                else
                {
                    if (!topping.Equals(""))
                    {
                        result += "Forgot to add " + topping + "\n";
                        score -= 5f;
                    }
                }
            }
            toppings = pizza.GetToppingsUsed().Split(',');
            foreach (string topping in toppings)
            {
                if (!order.toppings.Contains(topping))
                {
                    result += topping + " Added to pizza by mistake\n";
                }
                score -= 2f;
            }
            if(pizza.timeRemaining<-3f)
            {
                result += "Pizza is burnt\n";
                score -= pizza.timeRemaining * 5f;
            }


        }
        catch(Exception e)
        {
            result += "\n\n" + e.Message;
        }
        finally
        {
            if(result.Equals(""))
            {
                result += "This is perfect";
            }
            result+= "\n Score: "+Math.Round(score,2).ToString() + " % ";
            result+= "\n Time taken for Preperation: "+Math.Abs(Math.Round(order.endTime-order.startTime,2)).ToString() + " s ";
        }
        return result;
    }
}
