
public class Order 
{
    public string crustType;
    public string toppings;
    public string cheeseType;
    public string extras;
    public int currentStep;
    public float startTime;
    public float endTime;

    public Order(string dish)
    {
        AddOrder(dish);
    }
    public void AddOrder(string dish)
    {
        CalculateOrder(dish);
        currentStep = 0;
    }

    public void CalculateOrder(string dish)
    {
        string[] tokens = dish.Split('-');
        this.crustType = tokens[0];
        this.toppings = tokens[1];
        this.cheeseType = tokens[2];
        this.extras = tokens[3];
    }
    public string GetInstructions()
    {
        string instructions="";
        instructions += "1.Place " + crustType + " crust on the board\n";
        instructions += "2.Add Pizza Sauce\n";
        instructions += "3.Add "+cheeseType+"\n";
        instructions += "4.Add "+toppings+"\n";
        instructions += "5.Place pizza in oven \n";
        instructions += "6.Deliver in delivery zone \n";

        return instructions;
    }

    public string GetFeedback(float score)
    {

        return "Good";
    }
}
