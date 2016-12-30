using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAssistance : IIA {

    private Transform destination;
    private UnityEngine.AI.NavMeshAgent agent;
    private Vector3 movingTo;
    private GameObject gameObject;
    private Caractere caractere;

    public IAssistance(Caractere caractere) { this.caractere = caractere; }

    public void Start(GameObject gameObject)
    {
        this.gameObject = gameObject;
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
                
    }

    public void Update()
    {
        destination = chercherSoigneur();

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
}
