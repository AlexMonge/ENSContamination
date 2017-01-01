using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAssistance : IIA {

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

    public IAssistance(Caractere caractere) { this.caractere = caractere; }

    #endregion

    #region Méthodes

    public void Start(GameObject gameObject)
    {
        this.gameObject = gameObject;
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
                
    }

    public void Update()
    {
        // Va chercher un soigneur proche de lui (calcul à chaque frame)
        destination = chercherSoigneur();

        // L'entité va se diriger vers le soigneur
        if (destination != null)
        {
            agent.SetDestination(destination.position);
            movingTo = destination.position - gameObject.transform.position;

            if (movingTo.magnitude < 2)
            {
                destination = chercherSoigneur();
                agent.SetDestination(destination.position);
            }
        }
        else
        {
            destination = chercherSoigneur();

            if (destination != null)
                agent.SetDestination(destination.position);
        }
    }

    /// <summary>
    /// Permet de trouver le soigneur le plus proche
    /// </summary>
    /// <returns>Le Transform du soigneur le plus proche</returns>
    private Transform chercherSoigneur()
    {
        GameObject victime = plusProche("Boss");

        if (victime != null)
            return victime.transform;
        else
            return null;
    }

    /// <summary>
    /// Méthode permettant de trouver l'entité la plus proche
    /// </summary>
    /// <param name="tag">Tag de l'entité</param>
    /// <returns></returns>
    private GameObject plusProche(string tag)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject proche = null;
        float distance = Mathf.Infinity;
        Vector3 position = gameObject.transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
                if (tag.Equals("Boss"))
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
        return false;
    }

    public bool isAssistance()
    {
        return true;
    }

    public bool isPeur()
    {
        return false;
    }

    #endregion
}
