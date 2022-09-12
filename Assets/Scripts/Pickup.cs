using UnityEngine;
using UnityEngine.UI;
public class Pickup : MonoBehaviour
{
    // Start is called before the first frame update
    public float range = 2000f;
    public Transform hand;
    public Text itemText;
    public string itemName;
    public bool isItemInHand;
    Item itemInHand;
    Camera camera;
    void Start()
    {
        camera = FindObjectOfType<Camera>();
        itemName = " ";
        itemText.text = itemName;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (!isItemInHand)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
                {
                    Item item = hit.transform.GetComponent<Item>();
                    if (item != null)
                        if ( item.isPicked == false)
                        {
                            itemName = item.itemName;
                            itemText.text = itemName;
                            item.Interact();
                            item.pickupScript = this;
                            item.gameObject.transform.parent = this.gameObject.transform;
                            item.Pickup();
                            isItemInHand = true;
                            itemInHand = item;
                        }
                    else if(GameConstants.isMenu(item.itemName))
                        {
                            item.Interact();
                        }
                }
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.E))
            {

                if (GameConstants.isTopping(itemInHand.itemName))
                {
                    if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
                    {
                        Base itemBase = hit.transform.GetComponentInChildren<Base>();
                        if (itemBase != null && itemBase.containsItem == false)
                        {
                            if (itemBase.allowedItems == null || itemBase.allowedItems.Contains(itemInHand.itemName))
                            {
                                itemBase.ObjectPlaced(itemInHand.itemName);
                            }
                        }
                    }
                }
                else if(itemInHand.itemName.Equals(GameConstants.pizzaBoard))
                {
                    if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
                    {
                        Base itemBase = hit.transform.GetComponentInChildren<Base>();
                        if (itemBase != null)
                        {
                            
                            if (itemBase.allowedItems == null || itemBase.allowedItems.Contains(itemInHand.itemName))
                            {
                                Board pizzaBoard = itemInHand.gameObject.GetComponentInChildren<Board>();
                                if (!itemBase.containsItem)
                                {
                                    if (pizzaBoard.containsItem)
                                    {
                                        GameObject pizza = pizzaBoard.RemovePizza();
                                        itemBase.ObjectPlaced(pizza);
                                    }
                                }
                                else
                                {
                                    Oven oven = itemBase.gameObject.GetComponentInChildren<Oven>();
                                    Debug.Log(oven);
                                    if (oven != null)
                                    {
                                        GameObject pizza = oven.RemovePizza();
                                        pizza.gameObject.transform.parent = pizzaBoard.gameObject.transform;
                                        pizzaBoard.containsItem = true;
                                        pizza.GetComponent<Item>().itemBase = pizzaBoard;
                                        pizza.GetComponent<Item>().Place();
                                    }
                                }
                            }
                        }
                    }
                }
                else if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
                {
                    Base itemBase = hit.transform.GetComponentInChildren<Base>();
                        if (itemBase != null && itemBase.containsItem == false)
                        {
                        if (itemBase.allowedItems == null || itemBase.allowedItems.Contains(itemInHand.itemName))
                        {
                            itemInHand.gameObject.transform.parent = itemBase.gameObject.transform;
                            itemBase.containsItem = true;
                            itemInHand.itemBase = itemBase;
                            itemInHand.Place();
                            isItemInHand = false;
                            itemName = "";
                            itemText.text = itemName;
                            itemBase.ObjectPlaced(itemInHand.itemName);
                        }
                        }
                }
            }
            if(Input.mouseScrollDelta!=Vector2.zero && (itemInHand.itemName.Equals(GameConstants.sauce) || GameConstants.isCheese(itemInHand.itemName)))
            {
                if (Physics.Raycast(camera.transform.position, camera.transform.forward, out hit, range))
                {
                    Base itemBase = hit.transform.GetComponentInChildren<Base>();
                    if (itemBase != null && itemBase.containsItem == false)
                    {
                        if (itemBase.allowedItems == null || itemBase.allowedItems.Contains(itemInHand.itemName))
                        {
                            itemBase.ObjectPlaced(itemInHand.itemName);
                        }
                    }
                }
            }
        }
    }

    public void ObjectDropped()
    {
        isItemInHand = false;
        itemName = " ";
        itemText.text = itemName;
    }

}
