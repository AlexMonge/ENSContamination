using UnityEngine;
using System.Collections;

public class IAgressif : IIA {

    private Transform destination;
    private UnityEngine.AI.NavMeshAgent agent;
    private Vector3 movingTo;
    private GameObject gameObject;
    private Caractere caractere;

    public IAgressif(Caractere caractere) { this.caractere = caractere; }

    public void Start(GameObject gameObject)
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        this.gameObject = gameObject;
    }

    public void Update()
    {
        destination = chercherVictime();

        if (destination != null)
        {
            agent.SetDestination(destination.position);
            movingTo = destination.position - gameObject.transform.position;

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
                agent.SetDestination(GameObject.Find("DepartMicrobe").transform.position);
        }

        if (gameObject.tag.Equals("Satan"))
            mutation();
    }

    /// <summary>
    /// Permet de chercher une victime
    /// </summary>
    /// <returns></returns>
    public Transform chercherVictime()
    {
        GameObject victime = plusProche("Personnage", true);

        return (victime == null) ? null : victime.transform;
    }

    /// <summary>
    /// Permet de trouver l'entité la plus proche
    /// </summary>
    /// <param name="tag">Tag de l'entité</param>
    /// <param name="victimeSaine">Si vrai, la victime cherchée ne sera pas malade</param>
    /// <returns>La victime la plus proche</returns>
    public GameObject plusProche(string tag, bool victimeSaine)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject proche = null;
        float distance = Mathf.Infinity;
        Vector3 position = gameObject.transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (!soigneurProche(go.transform.position) && !soigneurProche(gameObject.transform.position))
            {
                if (curDistance < distance)
                    if (tag.Equals("Personnage") && (!go.GetComponent<Caracteristiques>().isInfecte() == victimeSaine) && !go.GetComponent<Caracteristiques>().isImmunise() && !go.GetComponent<Caracteristiques>().isResistant())
                    {
                        proche = go;
                        distance = curDistance;
                    }
            }
        }

        return proche;
    }

    /// <summary>
    /// Permet de savoir si un soigneur est proche d'une position ou non
    /// </summary>
    /// <param name="position">La position à évaluer</param>
    /// <returns>Vrai si un soigneur est proche de la position, faux sinon</returns>
    private bool soigneurProche(Vector3 position)
    {
        GameObject[] soigneurs = GameObject.FindGameObjectsWithTag("Boss");

        foreach (GameObject soigneur in soigneurs)
        {
            Vector3 diff = position - soigneur.transform.position;

            if (diff.sqrMagnitude < 40)
                return true;
        }

        return false;
    }

    /// <summary>
    /// Permet de faire muter le microbe
    /// </summary>
    private void mutation()
    {
        UIController uic = GameObject.Find("ENSC").GetComponent<UIController>();

        if (uic.getNbPersonne() > 0)
        {
            double prctgConta = uic.getPourcentageContamines();

            if (prctgConta < 25f)
            {
                agent.speed = 3.666f;
            }
            else if (prctgConta < 50f)
            {
                agent.speed = 2f;
            }
            else
            {
                agent.speed = 5f;
                agent.SetDestination(GameObject.Find("DepartMicrobe").transform.position);
            }
        }
    }


    public bool isAgressif()
    {
        return true;
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
        return false;
    }
}
