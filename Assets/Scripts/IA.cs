using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour {

    public Transform destination;
    private NavMeshAgent agent;
    private Vector3 movingTo;
    private bool attendre = false;
    private Vector3 variationArrivee;

	private Caractere caractere;


    void Start()
    {
        variationArrivee = new Vector3();
		caractere = new Caractere ();
		destination = initDestination();
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.SetDestination(destination.position + variationArrivee);
    }

    void Update()
    {
		if (destination.name.Contains ("Toilettes"))
			attendre = false;
		else
			attendre = true;

        /*movingTo = destination.position - transform.position;


        while (movingTo.magnitude < 2)
        {
            destination = initDestination();

            agent.SetDestination(destination.position);
        }*/

		StartCoroutine (attente ());         
    }

    IEnumerator attente()
    {
        movingTo = destination.position - transform.position;
		

        while (movingTo.magnitude < 5)
        {
            destination = initDestination();

			if (attendre == true) {
				yield return new WaitForSeconds (2);
			}
			else
				yield return null;

            agent.SetDestination(destination.position + variationArrivee);
        }
       
    }

    private Transform initDestination()
    {
        int zone = Random.Range(1, 101);

		if (zone >= 1 && zone < 6)
            return chercherLieux("Toilettes", true, 0);
		else 
		{
			zone = Random.Range (0, 101);

			if (caractere.inRandomRange (zone, ECaractere.INFORMATIQUE))
				return chercherLieux ("Info", false, 4);

			if (caractere.inRandomRange (zone, ECaractere.BIBLI))
                return chercherLieux("Foyer", false, 2);

			if (caractere.inRandomRange (zone, ECaractere.SALLECOURS)) {
				zone = Random.Range (0, 2);

				if (zone == 0)
                    return chercherLieux("O108", false, 2);
				else
                    return chercherLieux("S101", false, 3);
			}

			if (caractere.inRandomRange (zone, ECaractere.PAUSE))
                return chercherLieux("Patio", false, 4);
		}

        return chercherLieux("Patio", false, 4);
    }

    private Transform chercherLieux(string lieu, bool arriveeFixe, int radius)
    {
        GameObject dest = GameObject.Find(lieu);
        Transform[] possibilites = new Transform[dest.transform.childCount];
        Transform position;
        int i = 0;

        if (possibilites.Length == 0)
            return dest.transform;
        else
            foreach (Transform d in dest.transform)
                possibilites[i++] = d;

        position = possibilites[Random.Range(0, possibilites.Length)];

        if (!arriveeFixe)
        {
            Vector3 variation = Random.insideUnitSphere * radius;
            variation.y = 0;

            variationArrivee = variation;
        }

        
        return position;
    }
}
