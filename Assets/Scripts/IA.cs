using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour {

    private Transform destination;
    private NavMeshAgent agent;
    private Vector3 movingTo;

    void Start()
    {
        destination = initDestination();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);
    }

    void Update()
    {
        movingTo = destination.position - transform.position;

        if (movingTo.magnitude < 1)
        {
            destination = initDestination();
            agent.SetDestination(destination.position);
        }
    }

    private Transform initDestination()
    {
        GameObject dest = GameObject.Find("Toilettes");
        Transform[] possibilites = new Transform[dest.transform.childCount];
        int i = 0;

        foreach (Transform d in dest.transform)
            possibilites[i++] = d;

        return possibilites[Random.Range(0, possibilites.Length)];
    }
}
