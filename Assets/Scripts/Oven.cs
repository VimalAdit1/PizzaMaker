
using UnityEngine;
using UnityEngine.UI;

public class Oven : Base
{
    float time;
    float totalTime;
    bool isTimerOn;
    public Image timerImage;
    GameManager gameManager;
    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        isTimerOn = false;
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimerOn)
        {
            time = time - 1 * Time.deltaTime;
            timerImage.fillAmount = time/totalTime;
            if(time<0)
            {
                gameManager.progress(GameConstants.baked);
            }
        }
        else
        {
            timerImage.fillAmount = 0;
        }
    }

    public override void ObjectPlaced(GameObject objectName)
    {
        objectName.transform.parent = this.transform;
        objectName.transform.localPosition = Vector3.zero;
        Pizza pizza = objectName.GetComponentInChildren<Pizza>();
        if(pizza!=null)
        {
            time = pizza.timeRemaining;
            totalTime = pizza.totalTime;
            isTimerOn = true;
        }
        this.containsItem = true;
        animator.SetTrigger("Door");
    }

    public GameObject RemovePizza()
    {
        Pizza pizza = this.GetComponentInChildren<Pizza>();
        pizza.timeRemaining = time;
        pizza.RemovedfromOven();
        GameObject pizzaObject = pizza.gameObject;
        pizzaObject.transform.parent = null;
        this.containsItem = false;
        isTimerOn = false;
        animator.SetTrigger("Door");
        return pizzaObject;
    }
}
