using UnityEngine;
using System.Collections;

public class IPassif : IIA {

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
    /// Variable gérant la variation de l'arrivée (cercle aléatoire)
    /// </summary>
    private Vector3 variationArrivee;

    /// <summary>
    /// L'entité
    /// </summary>
    private GameObject gameObject;

    /// <summary>
    /// Permet de gérer l'attende de l'entité
    /// </summary>
    private bool isSleeping;

    /// <summary>
    /// Temps à partir duquel l'entité peut reprendre son déplacement
    /// </summary>
    private float stopSleepAt;

    /// <summary>
    /// Caractère de l'entité
    /// </summary>
    private Caractere caractere;

    #region Constructeurs

    public IPassif(Caractere caractere) { this.caractere = caractere; }

    #endregion

    #region Méthodes

    public void Start(GameObject gameObject)
    {
        this.gameObject = gameObject;
        variationArrivee = new Vector3();
        destination = initDestination();
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        agent.SetDestination(destination.position + variationArrivee);
    }

    /// <summary>
    /// Déplacement de l'entité
    /// </summary>
    public void Update()
    {
        if (destination != null)
        {
            if (!isSleeping)
            {
                movingTo = destination.position - gameObject.transform.position;

                // Lorsque l'entité est arrivée, elle dort pendant 3 - 5 secondes, puis cherche une nouvelle destination
                if (movingTo.magnitude < 4)
                {
                    sleepSeconds(Random.Range(3, 6));
                    destination = initDestination();
                    agent.SetDestination(destination.position + variationArrivee);
                }
            }
            else
            {
                if (Time.realtimeSinceStartup - stopSleepAt > 0)
                {
                    Rigidbody body = gameObject.GetComponent<Rigidbody>();
                    body.isKinematic = false;

                    isSleeping = false;
                }
            }
        }
        else
            destination = initDestination();
    }

    /// <summary>
    /// Permet d'initialiser la destination en fonction des préférences de la personne
    /// </summary>
    /// <returns>Le transform de la destination</returns>
    private Transform initDestination()
    {
        int zone = Random.Range(0, 101);

        if (zone >= 0 && zone < 6)
            return chercherLieux("Toilettes", true, 0);
        else if (zone >= 6 && zone < 10)
            return chercherLieux("Administration", false, 1);
        else
        {
            zone = Random.Range(0, 101);

            if (caractere.inRandomRange(zone, ECaractere.INFORMATIQUE))
                return chercherLieux("Info", false, 4);

            if (caractere.inRandomRange(zone, ECaractere.FOYER))
                return chercherLieux("Foyer", false, 2);

            if (caractere.inRandomRange(zone, ECaractere.SALLECOURS))
            {
                zone = Random.Range(0, 3);

                if (zone == 0)
                    return chercherLieux("O108", false, 2);
                else if (zone == 1)
                    return chercherLieux("S101", false, 3);
                else
                    return chercherLieux("N103", false, 2);
            }

            if (caractere.inRandomRange(zone, ECaractere.PAUSE))
                return chercherLieux("Patio", false, 4);
        }

        return chercherLieux("Patio", false, 4);
    }

    /// <summary>
    /// Permet à la personne de trouver un lieu
    /// </summary>
    /// <param name="lieu">Nom du lieu (Empty dans la vue)</param>
    /// <param name="arriveeFixe">Si faux, l'arrivée se fera dans un cercle aléatoire autour du point d'arrivée de base</param>
    /// <param name="radius">Taille du cercle autour du point d'arrivée</param>
    /// <returns>Le Transform correspondant au lieu trouvé</returns>
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

    /// <summary>
    /// Permet à la personne de "dormir" pendant x secondes
    /// </summary>
    /// <param name="sec">Le nombre de secondes</param>
    private void sleepSeconds(int sec)
    {
        Rigidbody body = gameObject.GetComponent<Rigidbody>();
        body.isKinematic = true;
        body.velocity = Vector3.zero;
        stopSleepAt = Time.realtimeSinceStartup + sec;
        isSleeping = true;
    }

    #endregion

    #region Accesseurs

    public bool isAgressif()
    {
        return false;
    }

    public bool isPassif()
    {
        return true;
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
