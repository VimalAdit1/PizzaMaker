using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleaningRobot : MonoBehaviour
{
    GameObject[] goalLocations;
    UnityEngine.AI.NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        goalLocations = GameObject.FindGameObjectsWithTag("Destination1");
        agent = this.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);
    }


    // Update is called once per frame
    void Update()
    {
        if (agent.remainingDistance < 1)
        {
            StartCoroutine(ReturnBack());
        }

    }

    IEnumerator ReturnBack()
    {

        yield return new WaitForSeconds(3f);
        agent.SetDestination(goalLocations[Random.Range(0, goalLocations.Length)].transform.position);

    }
}
