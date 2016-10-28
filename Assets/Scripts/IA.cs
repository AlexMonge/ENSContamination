using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour {

    private Transform destination;
    private NavMeshAgent agent;
    private Vector3 movingTo;
    private bool attendre = false;

    void Start()
    {
        destination = initDestination();
        agent = gameObject.GetComponent<NavMeshAgent>();
        agent.SetDestination(destination.position);
    }

    void Update()
    {
        if (destination.name.Contains("Toilettes"))
        {
            attendre = false;
        }
        else
        {
            attendre = true;
        }
        StartCoroutine(attente());         
    }

    IEnumerator attente()
    {
        movingTo = destination.position - transform.position;

        while (movingTo.magnitude < 2)
        {
            destination = initDestination();
            if (attendre == true)
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
        int zone = Random.Range(1, 101);

        if (zone >=1 && zone <6)
        {            
            GameObject dest = GameObject.Find("Toilettes");
            Transform[] possibilites = new Transform[dest.transform.childCount];
            int i = 0;

            foreach (Transform d in dest.transform)
                possibilites[i++] = d;

            return possibilites[Random.Range(0, possibilites.Length)];
        }

        if (zone >= 6 && zone < 51)
        {
            GameObject dest = GameObject.Find("Patio");
            Transform[] possibilites = new Transform[dest.transform.childCount];
            int i = 0;

            foreach (Transform d in dest.transform)
                possibilites[i++] = d;

            return possibilites[Random.Range(0, possibilites.Length)];
        }

        if (zone >= 51 && zone < 56)
        {
            GameObject dest = GameObject.Find("O108");
            Transform[] possibilites = new Transform[dest.transform.childCount];
            int i = 0;

            foreach (Transform d in dest.transform)
                possibilites[i++] = d;

            return possibilites[Random.Range(0, possibilites.Length)];
        }

        if (zone >= 56 && zone < 61)
        {
            GameObject dest = GameObject.Find("S101");
            Transform[] possibilites = new Transform[dest.transform.childCount];
            int i = 0;

            foreach (Transform d in dest.transform)
                possibilites[i++] = d;

            return possibilites[Random.Range(0, possibilites.Length)];
        }

        if (zone >= 61 && zone < 71)
        {
            GameObject dest = GameObject.Find("Foyer");
            
            return dest.transform;
        }

        if (zone >= 71 && zone < 81)
        {
            GameObject dest = GameObject.Find("S110");

            return dest.transform;
        }

        if (zone >= 81 && zone < 91)
        {
            GameObject dest = GameObject.Find("S111");

            return dest.transform;
        }

        if (zone >= 91 && zone < 101)
        {
            GameObject dest = GameObject.Find("S112");

            return dest.transform;
        }

        else
        {
            return null;
        }

    }
}
