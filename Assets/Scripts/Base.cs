using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public bool containsItem;
    public List<string> allowedItems;
    

    virtual public void ObjectPlaced(string objectName)
    {
        Debug.Log("Object Placed "+objectName);
    }
    virtual public void ObjectPlaced(GameObject objectName)
    {
        Debug.Log("Object Placed "+objectName.name);
    }


}
