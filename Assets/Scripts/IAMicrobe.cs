using UnityEngine;
using System.Collections;

public class IAMicrobe : MonoBehaviour {

	public Transform destination;
	private NavMeshAgent agent;
	private Vector3 movingTo;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {

        destination = chercherVictime();

        if (destination != null)
        {
            agent.SetDestination(destination.position);
            movingTo = destination.position - transform.position;

            if (movingTo.magnitude < 2)
            {
                destination = chercherVictime();
                agent.SetDestination(destination.position);
            }
        }
        else
        {
            destination = chercherVictime();

            if (destination != null)
                agent.SetDestination(destination.position);
        }
	}

	private Transform chercherVictime()
	{
        GameObject victime = plusProche("Personnage", true);

        return (victime == null) ? null : victime.transform;
	}

    private GameObject plusProche(string tag, bool victimeSaine)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject proche = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (!soigneurProche(go.transform.position) && !soigneurProche(this.transform.position))
            {
                if (curDistance < distance)
                    if (tag.Equals("Personnage") && (!go.GetComponent<Caracteristiques>().isInfecte() == victimeSaine))
                    {
                        proche = go;
                        distance = curDistance;
                    }
            }
        }

        return proche;
    }

    private bool soigneurProche(Vector3 position)
    {
        GameObject[] soigneurs = GameObject.FindGameObjectsWithTag("Boss");

        foreach (GameObject soigneur in soigneurs)
        {
            Vector3 diff = position - soigneur.transform.position;

            if (diff.sqrMagnitude < 50)
                return true;
        }

        return false;
    }
}
