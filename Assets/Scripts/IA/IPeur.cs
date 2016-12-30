using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IPeur : IIA {

    private Transform destination;
    private GameObject gameObject;
    private UnityEngine.AI.NavMeshAgent agent;
    private Vector3 movingTo;
    private Caractere caractere;
    private Vector3 variationArrivee;

    public IPeur(Caractere caractere) { this.caractere = caractere; }

    public void Start(GameObject gameObject)
    {
        this.gameObject = gameObject;
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    public void Update()
    {
        if (destination == null)
        {
            destination = chercherRefuge("EST", false, 1);
            agent.SetDestination(destination.position + variationArrivee);
        }

        movingTo = destination.position - gameObject.transform.position;
    }

    /// <summary>
    /// Permet à la personne de trouver un refuge dans le lieu voulu
    /// </summary>
    /// <param name="lieu">Nom du lieu (Empty dans la vue)</param>
    /// <param name="arriveeFixe">Si faux, l'arrivée se fera dans un cercle aléatoire autour du point d'arrivée de base</param>
    /// <param name="radius">Taille du cercle autour du point d'arrivée</param>
    /// <returns>Le Transform correspondant au refuge trouvé</returns>
    private Transform chercherRefuge(string lieu, bool arriveeFixe, int radius)
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

    public bool isAgressif()
    {
        return false;
    }

    public bool isPassif()
    {
        return false;
    }

    public bool isSoigneur()
    {
        return false;
    }

    public bool isAssistance()
    {
        return false;
    }

    public bool isPeur()
    {
        return true;
    }
}
