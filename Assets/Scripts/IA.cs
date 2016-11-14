using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour {

    private Transform destination;
    private NavMeshAgent agent;
    private Vector3 movingTo;
    private bool attendre = false;

	private Caractere caractere;

    void Start()
    {
		caractere = new Caractere ();
		destination = initDestination();
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.SetDestination(destination.position);
    }

    void Update()
    {
		if (destination.name.Contains ("Toilettes"))
			attendre = false;
		else
			attendre = true;

		StartCoroutine (attente ());         
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
			return chercherLieux ("Toilettes");
		else 
		{
			zone = Random.Range (0, 101);

			if (caractere.inRandomRange (zone, ECaractere.INFORMATIQUE))
				return chercherLieux ("Info");

			if (caractere.inRandomRange (zone, ECaractere.BIBLI))
				return chercherLieux ("Foyer");

			if (caractere.inRandomRange (zone, ECaractere.SALLECOURS)) {
				zone = Random.Range (0, 2);

				if (zone == 0)
					return chercherLieux ("O108");
				else
					return chercherLieux ("S101");
			}

			if (caractere.inRandomRange (zone, ECaractere.PAUSE))
				return chercherLieux ("Patio");
		}
        
		return chercherLieux ("Patio");
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
}
