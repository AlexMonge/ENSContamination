﻿using UnityEngine;
using System.Collections;

public class ISoigneur : IIA {

    private Transform bureau;
    private Transform destination;
    private UnityEngine.AI.NavMeshAgent agent;
    private Vector3 movingTo;
    private Animator animator;
    private GameObject suit;
    private GameObject gameObject;
    private Caractere caractere;

    public ISoigneur(Caractere caractere) { this.caractere = caractere; }

    public void Start(GameObject gameObject)
    {
        this.gameObject = gameObject;
        agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
        animator = gameObject.GetComponent<Animator>();

        if (animator != null) animator.SetInteger("Marche", 0);
        suit = null;

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

    public void Update()
    {
        if (animator != null) animator.SetInteger("Marche", 1);
        destination = chercherVictime();

        if (destination != null)
        {
            agent.SetDestination(destination.position);
            movingTo = destination.position - gameObject.transform.position;

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
}
