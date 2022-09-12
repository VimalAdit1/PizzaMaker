
using UnityEngine;

public  class Item : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isPicked = false;
    public Base itemBase;
    public string itemName;
    public Vector3 offset;
    public Vector3 offsetRotation;
    public Vector3 originalPosition;
    public Vector3 originalRoatation;
    public Transform originalParent;
    public Pickup pickupScript;
    Rigidbody rb;
    public bool isDroppable;

    public virtual void Interact()
    {
        Debug.Log("PickedUp "+itemName);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalParent = this.transform.parent;
        originalPosition = this.transform.position;
        originalRoatation = this.transform.rotation.ToEulerAngles();
        isDroppable = isPicked;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            if (isDroppable)
            {
                Drop();
            }
        }
    }
    public void Pickup()
    {
        if(itemBase!=null)
        {
            itemBase.containsItem = false;
            itemBase = null;
        }
        isPicked = true;
        isDroppable = true;
        this.transform.localPosition = offset;
        this.transform.localRotation = Quaternion.Euler(offsetRotation);
        rb.isKinematic = true;
    }
    public void Place()
    {
        isPicked = false;
        this.transform.localPosition = new Vector3(0,0.022f,-0.038f);
    }
    public void Drop()
    {
        if(isPicked==true)
        {
            isPicked = false;
            this.transform.parent = originalParent;
            this.transform.position = originalPosition;
            this.transform.rotation = Quaternion.Euler(originalRoatation);
            pickupScript.ObjectDropped();
        }
    }

}
