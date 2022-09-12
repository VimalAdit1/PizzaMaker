
using UnityEngine;

public class GameConstants : MonoBehaviour
{
    public static string player = "Player";
    public static string pizza = "Pizza";
    public static string pizzaBoard = "PizzaBoard";
    public static string normal = "Normal";
    public static string deep = "Deep";
    public static string sauce = "Sauce";
    public static string cheese = "MozzerellaCheese";
    public static string cheeseTracker = "Cheese";
    public static string cheddar = "CheddarCheese";
    public static string parmesan = "ParmesanCheese";
    public static string pepperoni = "Pepperoni";
    public static string olive = "Olive";
    public static string mushroom = "Mushroom";
    public enum Crust { Normal, Deep };
    public enum Toppings { Pepperoni, Olive, Mushroom };
    public enum Cheese { MozzerellaCheese, CheddarCheese, ParmesanCheese };
    public enum Extra { Pepperoni, Olive, Mushroom ,MozzerellaCheese, CheddarCheese, ParmesanCheese };
    public static int crustPlaced = 1;
    public static int sauceAdded = 2;
    public static int cheeseAdded = 3;
    public static int toppingsAdded = 4;
    public static int baked = 5;
    public static int delivered = 6;
    public static int pepperoniIndex = 0;
    public static int mushroomIndex = 1;
    public static int oliveIndex = 2;
    public static int mozzerellaIndex = 0;
    public static int cheddarIndex = 1;
    public static int parmesanIndex = 2;


    public static bool isTopping(string item)
    {
        return item.Equals(pepperoni) || item.Equals(olive) || item.Equals(mushroom);
    }
    public static int getToppingPosition(string item)
    {
        
        if(item.Equals(pepperoni))
        {
            return pepperoniIndex;
        }
        else if(item.Equals(olive))
        {
            return oliveIndex;
        }
        else if(item.Equals(mushroom))
        {
            return mushroomIndex;
        }
        else
        {
            return -1;
        }
    }
    public static int getCheesePosition(string item)
    {

        if (item.Equals(cheese))
        {
            return mozzerellaIndex;
        }
        else if (item.Equals(cheddar))
        {
            return cheddarIndex;
        }
        else if (item.Equals(parmesan))
        {
            return parmesanIndex;
        }
        else
        {
            return -1;
        }
    }
    public static bool isCheese(string item)
    {
        return item.Equals(cheese) || item.Equals(cheddar)||item.Equals(parmesan);
    }
    public static bool isMenu(string item)
    {
        return item.Contains("Menu");
    }
}
