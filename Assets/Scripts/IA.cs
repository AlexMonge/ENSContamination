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
            attendre = false;
        else
            attendre = true;

        StartCoroutine(attente());         
    }

    IEnumerator attente()
    {
        movingTo = destination.position - transform.position;

        while (movingTo.magnitude < 2)
        {
            destination = initDestination();

            if (attendre == true)
                yield return new WaitForSeconds(8);
            else
                yield return null;

            agent.SetDestination(destination.position);
        }
       
    }

    private Transform initDestination()
    {
        int zone = Random.Range(1, 101);
        
        if (zone >= 1 && zone < 6)
            return chercherLieux("Toilettes");

        if (zone >= 6 && zone < 51)
            return chercherLieux("Patio");

        if (zone >= 51 && zone < 56)
            return chercherLieux("O108");

        if (zone >= 56 && zone < 61)
            return chercherLieux("S101");

        if (zone >= 61 && zone < 71)
            return chercherLieux("Foyer");

        if (zone >= 71 && zone < 101)
            return chercherLieux("Info");

        return null;
    }

    private Transform chercherLieux(string lieu)
    {
        GameObject dest = GameObject.Find(lieu);
        Transform[] possibilites = new Transform[dest.transform.childCount];
        int i = 0;

        if (possibilites.Length == 0)
            return dest.transform;
        else
            foreach (Transform d in dest.transform)
                possibilites[i++] = d;

        return possibilites[Random.Range(0, possibilites.Length)];
    }

    public Vector3 GetMovingTo() { return movingTo; }
    public void SetMovingTo(Vector3 value) { movingTo = value; }
   
}
