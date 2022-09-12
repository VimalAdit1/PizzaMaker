using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pizza : Base
{
    public string crustType;
    public Transform sauceMesh;
    public Transform[] cheeseMesh;
    public Transform[] toppingPositions;
    public GameObject topping;
    GameManager gameManager;
    public bool isSauceAdded;
    public bool isCheeseAdded;
    public float MaxSauceHeight;
    public float timeRemaining;
    public float totalTime;
    public GameObject[] toppings;
    public Material cookedCrust;
    public Material cookedCheese;
    public Dictionary<string, float> tracker;
    // Start is called before the first frame update
    void Start()
    {
        sauceMesh.localScale = Vector3.zero;
        foreach (Transform cheese in cheeseMesh)
        {
            cheese.localScale = Vector3.zero;
        }
        gameManager = FindObjectOfType<GameManager>();
        isSauceAdded = false;
        isCheeseAdded = false;
        totalTime = timeRemaining;
        tracker = new Dictionary<string, float>();
    }

    internal void UpdateTime()
    {
        if (tracker.ContainsKey("TimeLeft"))
        {
            tracker["TimeLeft"] = timeRemaining;
        }
        else
        {
            tracker.Add("TimeLeft", timeRemaining);
        }
    }

    public override void ObjectPlaced(string objectName)
    {
        Debug.Log("Object" + objectName);
        if(objectName.Equals(GameConstants.sauce)|| GameConstants.isCheese(objectName))
        {
            Add(objectName);
        }
        else if(isCheeseAdded)
        {
            AddTopping(objectName);
        }
        UpdateTracker(objectName);
    }

    private void AddTopping(string objectName)
    {
        for (int i = 0;i<5;i++)
        {
            GameObject go = Instantiate(toppings[GameConstants.getToppingPosition(objectName)]);
            go.GetComponent<Transform>().parent = this.transform;
            go.GetComponent<Rigidbody>().isKinematic = true;
            go.transform.localPosition = UnityEngine.Random.insideUnitSphere*0.13f+toppingPositions[i % toppingPositions.Length].localPosition;
            go.transform.localPosition = new Vector3(go.transform.localPosition.x, 0.013f, go.transform.localPosition.z);
            go.transform.localRotation = Quaternion.Euler(go.GetComponent<Item>().offsetRotation);
        }
        gameManager.progress(GameConstants.toppingsAdded);
    }

    private void Add(string objectName)
    {
        if (objectName.Equals(GameConstants.sauce))
        {
            Add(sauceMesh,objectName);
        }
        else if (GameConstants.isCheese(objectName) &&isSauceAdded)
        {
            Add(cheeseMesh[GameConstants.getCheesePosition(objectName)],objectName);
        }
    }

    private void UpdateTracker(string objectName)
    {
        if(GameConstants.isTopping(objectName))
        {
            float count = tracker.ContainsKey(objectName) ? tracker[objectName] : 0;
            count++;
            Debug.Log(objectName + " " + count);
            if(tracker.ContainsKey(objectName))
            {
                tracker[objectName] = count;
            }
            else
            {
                tracker.Add(objectName, count);
            }
        }
        else
        {
            float height;
            if(GameConstants.isCheese(objectName))
            {
                height = cheeseMesh[GameConstants.getCheesePosition(objectName)].localScale.y;
                if (tracker.ContainsKey(objectName))
                {
                    tracker[objectName] = 0;
                }
                else
                {
                    tracker.Add(objectName, 0);
                }
                objectName = GameConstants.cheeseTracker;
            }
            else
            {
                height = sauceMesh.localScale.y;
            }
            if (tracker.ContainsKey(objectName))
            {
                tracker[objectName] = height;
            }
            else
            {
                tracker.Add(objectName, height);
            }
        }
    }

    private void Add(Transform mesh, string objectName)
    {
        float max;
        if(objectName.Equals(GameConstants.sauce))
        {
            max = MaxSauceHeight;
        }
        else
        {
            max = MaxSauceHeight + 1;
        }
        Vector3 added;
        if (mesh.localScale.x < 1f)
        {
            added = Vector3.one * Time.deltaTime*15;
        }
        else if (mesh.localScale.y < max)
        {
            if(objectName.Equals(GameConstants.sauce))
            {
                isSauceAdded = true;
                gameManager.progress(GameConstants.sauceAdded);
            }
            else
            {
                isCheeseAdded = true;
                gameManager.progress(GameConstants.cheeseAdded);
            }    
            added = Vector3.zero;
            added.y = Time.deltaTime * 50;
        }
        else
        {
            added = Vector3.zero;
        }
            mesh.localScale = new Vector3(Mathf.Clamp(mesh.localScale.x+added.x,0,1), mesh.localScale.y + added.y, mesh.localScale.z + added.z);
       
    }

    public string GetCheeseUsed()
    {
        string cheeseUsed = "";
        foreach (string key in this.tracker.Keys)
        {
            if (GameConstants.isCheese(key))
            {
                cheeseUsed += key + ",";
            }
        }
        return cheeseUsed;
    }
    public string GetToppingsUsed()
    {
        string toppings = "";
        foreach (string key in this.tracker.Keys)
        {
            if (GameConstants.isTopping(key))
            {
                toppings += key + ",";
            }
        }
        return toppings;
    }

    public void RemovedfromOven()
    {
        this.GetComponent<MeshRenderer>().material = cookedCrust;
        foreach(Transform cheese in cheeseMesh)
        {
            cheese.gameObject.GetComponent<MeshRenderer>().material = cookedCheese;
        }
    }
}
