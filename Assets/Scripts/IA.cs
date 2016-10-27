using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour {

    private Transform destination;
    private NavMeshAgent agent;
    private Vector3 movingTo;
    private bool surpatio = false;

    void Start()
    {
        destination = initDestination();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);
    }

    void Update()
    {
        if (destination.name.Contains("Patio"))
        {
            surpatio = true;
        }
        else
        {
            surpatio = false;
        }
        StartCoroutine(attente());         
    }

    IEnumerator attente()
    {
        movingTo = destination.position - transform.position;

        while (movingTo.magnitude < 2)
        {
            destination = initDestination();
            if (surpatio == true)
            {
                yield return new WaitForSeconds(8);
            }
            else
            {
                yield return null;
            }
            agent.SetDestination(destination.position);
        }
       
    }

    private Transform initDestination()
    {
        int zone = Random.Range(1, 3);
        if (zone == 1)
        {            
            GameObject dest = GameObject.Find("Toilettes");
            Transform[] possibilites = new Transform[dest.transform.childCount];
            int i = 0;

            foreach (Transform d in dest.transform)
                possibilites[i++] = d;

            return possibilites[Random.Range(0, possibilites.Length)];
        }
        else
        {
            GameObject dest = GameObject.Find("Patio");
            Transform[] possibilites = new Transform[dest.transform.childCount];
            int i = 0;

            foreach (Transform d in dest.transform)
                possibilites[i++] = d;

            return possibilites[Random.Range(0, possibilites.Length)];
        }
        
    }
}
