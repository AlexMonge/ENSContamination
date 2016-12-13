using UnityEngine;
using System.Collections;

public class IASoigneur : MonoBehaviour {

    public Transform bureau;
    private Transform destination;
    private NavMeshAgent agent;
    private Vector3 movingTo;
    private Animator animator;
    private GameObject suit;

    // Use this for initialization
    void Start()
    {
        agent = gameObject.GetComponent<NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();

        if (animator != null) animator.SetInteger("Marche", 0);
        suit = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (animator != null) animator.SetInteger("Marche", 1);
        destination = chercherVictime();

        if (destination != null)
        {
            agent.SetDestination(destination.position);
            movingTo = destination.position - transform.position;

            if (movingTo.magnitude < 2)
            {
                if (animator != null) animator.SetInteger("Marche", 0);
                destination = chercherVictime();
                agent.SetDestination(destination.position);
            }
        }
        else
        {
            if (animator != null) animator.SetInteger("Marche", 0);
            destination = chercherVictime();

            if (destination != null)
                agent.SetDestination(destination.position);
            else
                agent.SetDestination(bureau.position);
        }
    }

    private Transform chercherVictime()
    {
        GameObject victime = plusProche("Personnage", false);

        if (victime != null)
        {
            suit = victime;
            victime.GetComponent<Caracteristiques>().suivi = true;
            return victime.transform;
        }
        else
            return null;
    }

    private GameObject plusProche(string tag, bool victimeSaine)
    {
        if (suit != null)
            suit.GetComponent<Caracteristiques>().suivi = false;

        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject proche = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
                if (tag.Equals("Personnage") && (!go.GetComponent<Caracteristiques>().isInfecte() == victimeSaine) && go.GetComponent<Caracteristiques>().suivi == false)
                {
                    proche = go;
                    distance = curDistance;
                }
        }

        return proche;
    }
}
