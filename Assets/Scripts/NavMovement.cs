using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavMovement : MonoBehaviour
{
    GameObject[] goalLocations;
    GameObject OriginLocation;
    UnityEngine.AI.NavMeshAgent agent;
    public GameObject navMeshCamera;
    public bool isDelivered;
    public bool isAgentActive;
    float startTime;

    // Start is called before the first frame update
    void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("Destination1");
        OriginLocation = GameObject.FindGameObjectWithTag("Origin");
        navMeshCamera.SetActive(false);
        isDelivered = false;
        isAgentActive = false;
    }

    public void Deliver()
    {        
        
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
        isAgentActive = true;
        navMeshCamera.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
         if(agent.remainingDistance < 1&&!isDelivered&&isAgentActive)
         {  
                StartCoroutine(ReturnBack());
         }
        if (agent.remainingDistance < 1 && isDelivered&&isAgentActive)
        {
            isDelivered = !isDelivered;
            isAgentActive = false;
            navMeshCamera.SetActive(false);
        }

    }

    IEnumerator ReturnBack()
    {
        
            yield return new WaitForSeconds(3f);
            agent.SetDestination(OriginLocation.transform.position);
            isDelivered = true;

    }
}
