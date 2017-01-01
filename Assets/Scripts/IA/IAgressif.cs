using UnityEngine;
using System.Collections;

public class IAgressif : IIA {

    /// <summary>
    /// Point de destination de l'entité
    /// </summary>
    private Transform destination;

    /// <summary>
    /// Agent gérant le déplacement
    /// </summary>
    private UnityEngine.AI.NavMeshAgent agent;

    /// <summary>
    /// Différence entre la position de l'entité et sa destination
    /// </summary>
    private Vector3 movingTo;

    /// <summary>
    /// L'entité
    /// </summary>
    private GameObject gameObject;

    /// <summary>
    /// Caractère de l'entité
    /// </summary>
    private Caractere caractere;

    #region Constructeurs

    public IAgressif(Caractere caractere) { this.caractere = caractere; }

    #endregion

    #region Méthodes

    public void Start(GameObject gameObject)
    {
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        this.gameObject = gameObject;
    }

    /// <summary>
    /// Déplacement de l'entité
    /// </summary>
    public void Update()
    {
        // Va chercher à contaminer une victime proche de lui (calcul à chaque frame)
        destination = chercherVictime();

        // Va se diriger vers sa victime
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

            // Si ne peut plus contaminer, retourne à son point de départ
            if (destination != null)
                agent.SetDestination(GameObject.Find("DepartMicrobe").transform.position);
        }

        // Mutation du microbe
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
    /// Permet de faire muter le microbe en fonction du nombre d'infectés
    /// </summary>
    private void mutation()
    {
        UIController uic = GameObject.Find("ENSC").GetComponent<UIController>();

        if (uic.getNbPersonne() > 0)
        {
            double prctgConta = uic.getPourcentageContamines();

            if (prctgConta < 25d) // < 25% : vitesse normale
            {
                agent.speed = 3.666f;
            }
            else if (prctgConta < 50d) // < 50% : vitesse plus basse
            {
                agent.speed = 2f;
            }
            else // >= 50% : retourne à son départ -> inactif temporairement
            {
                agent.speed = 5f;
                agent.SetDestination(GameObject.Find("DepartMicrobe").transform.position);
            }
        }
    }

    #endregion

    #region Accesseurs

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

    #endregion
}
