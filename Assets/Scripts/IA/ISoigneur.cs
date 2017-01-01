using UnityEngine;
using System.Collections;

public class ISoigneur : IIA {

    /// <summary>
    /// Point de départ de l'entité
    /// </summary>
    private Transform bureau;

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
    /// Permet de gérer l'animation de l'entité
    /// </summary>
    private Animator animator;

    /// <summary>
    /// Définit qui suit l'entité
    /// </summary>
    private GameObject suit;

    /// <summary>
    /// L'entité
    /// </summary>
    private GameObject gameObject;

    /// <summary>
    /// Caractère de l'entité
    /// </summary>
    private Caractere caractere;

    #region Constructeurs

    public ISoigneur(Caractere caractere) { this.caractere = caractere; }

    #endregion

    #region Méthodes

    public void Start(GameObject gameObject)
    {
        this.gameObject = gameObject;
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();

        if (animator != null) animator.SetInteger("Marche", 0);
        suit = null;

        // On initialise le bureau de départ de chaque soigneur
        switch (gameObject.name)
        {
            case "Bernard Claverie":
                bureau = GameObject.Find("Directeur").transform;
                break;

            case "Isabelle Sese":
                bureau = GameObject.Find("Administration 2").transform;
                break;

            case "Jean-Paul":
                bureau = GameObject.Find("Logistique").transform;
                break;

            case "Nadège Rodriguez":
                bureau = GameObject.Find("Administration 1").transform;
                break;
        }
    }

    /// <summary>
    /// Déplacement du soigneur
    /// </summary>
    public void Update()
    {
        if (animator != null) animator.SetInteger("Marche", 1);
        destination = chercherVictime();

        // Si la destination est nulle :
        // - s'il y a des victimes, on cherche une victime
        // - s'il n'y a pas de victime, le soigneur retourne à son bureau
        if (destination != null)
        {
            agent.SetDestination(destination.position);
            movingTo = destination.position - gameObject.transform.position;

            // Lorsque l'entité soigne une victime, elle cherche une nouvelle victime proche d'elle à soigner
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

    /// <summary>
    /// Permet de trouver la victime la plus proche
    /// </summary>
    /// <returns>Le Transform de la victime</returns>
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

    /// <summary>
    /// Permet de trouver l'entité la plus proche
    /// </summary>
    /// <param name="tag">Tag de l'entité</param>
    /// <param name="victimeSaine">Si vrai, la victime cherchée ne sera pas malade</param>
    /// <returns>La victime la plus proche</returns>
    private GameObject plusProche(string tag, bool victimeSaine)
    {
        if (suit != null)
            suit.GetComponent<Caracteristiques>().suivi = false;

        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject proche = null;
        float distance = Mathf.Infinity;
        Vector3 position = gameObject.transform.position;

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

    #endregion

    #region Accesseurs

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
        return true;
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
