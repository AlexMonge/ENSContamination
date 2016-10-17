using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour {

    private Transform chemin;
    private Transform[] pathPoints;
    private int speed, currentPathPoint;
    private Vector3 point, movingTo, velocity;
    private bool isSleeping;
    private float stopSleepAt;
    private Caracteristiques caracteristiques;

    void Start()
    {
        speed = 10;
        point = movingTo = velocity = Vector3.zero;
        isSleeping = false;
        stopSleepAt = 0f;
        caracteristiques = gameObject.GetComponent<Caracteristiques>();

        initChemins();
    }

    void Update()
    {
        if (!isSleeping)
            deplacement(caracteristiques.isInfecte() && nbResteInfecte() > 0);
        else
            if (Time.realtimeSinceStartup - stopSleepAt > 0)
            {
                Rigidbody body = GetComponent<Rigidbody>();
                body.isKinematic = false;

                isSleeping = false;
            }
    }

    private void deplacement(bool poursuivrePersonnages)
    {
        if (currentPathPoint < pathPoints.Length)
        {
            point = pathPoints[currentPathPoint].position;
            movingTo = point - transform.position;
            velocity = GetComponent<Rigidbody>().velocity;

            if (movingTo.magnitude < 1)
                ++currentPathPoint;
            else
                velocity = movingTo.normalized * speed;
        }
        else
        {
            currentPathPoint = 0;

            if (!poursuivrePersonnages)
                trouverAutreChemin();
            else
                poursuivre();
        }

        GetComponent<Rigidbody>().velocity = velocity;
        transform.LookAt(point);
    }

    private void initChemins()
    {
        string zoneProche = chercherGameObjectPlusProche("Zone").name;
        int numZoneProche;
        try { numZoneProche = int.Parse(zoneProche.Split(' ')[1]); }
        catch { numZoneProche = 1; }

        string cheminProche = appartientAChemin(chercherGameObjectPlusProche("Zone")).name;

        chemin = GameObject.Find(cheminProche).transform;
        pathPoints = new Transform[chemin.childCount - numZoneProche + 2];

        pathPoints[0] = transform;

        int i = 1;
        foreach (Transform zone in chemin)
        {
            int numZone;
            try { numZone = int.Parse(zone.name.Split(' ')[1]); }
            catch { numZone = 1; }

            if (numZone >= numZoneProche)
                pathPoints[i++] = zone;
        }

        changeLastZone(pathPoints[pathPoints.Length - 2], pathPoints[pathPoints.Length - 1]);
    }

    private void trouverAutreChemin()
    {
        sleepSeconds(Random.Range(1, 5));
        string nomChemin = "Chemin " + Random.Range(0, GameObject.FindGameObjectsWithTag("Chemin").Length);
        chemin = GameObject.Find(nomChemin).transform;
        pathPoints = new Transform[chemin.childCount + 1];

        pathPoints[0] = transform;

        int i = 1;
        foreach (Transform zone in chemin)
            pathPoints[i++] = zone;

        changeLastZone(pathPoints[pathPoints.Length - 2], pathPoints[pathPoints.Length - 1]);
    }

    private void poursuivre()
    {
        pathPoints = new Transform[2];
        pathPoints[0] = transform;
        pathPoints[1] = chercherGameObjectPlusProche("Personnage").transform;
    }

    private GameObject chercherGameObjectPlusProche(string tag)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag(tag);
        GameObject proche = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;

            if (curDistance < distance)
                if (tag.Equals("Zone") || (tag.Equals("Personnage") && !go.GetComponent<Caracteristiques>().isInfecte()))
                {
                    proche = go;
                    distance = curDistance;
                }
        }

        return proche;
    }

    private GameObject appartientAChemin(GameObject zone)
    {
        GameObject[] chemins = GameObject.FindGameObjectsWithTag("Chemin");

        foreach (GameObject chemin in chemins)
            foreach (Transform child in chemin.transform)
                if (child.Equals(zone.transform))
                    return chemin;

        return null;
    }

    private void sleepSeconds(int sec)
    {
        Rigidbody body = GetComponent<Rigidbody>();
        body.isKinematic = true;
        body.velocity = Vector3.zero;
        stopSleepAt = Time.realtimeSinceStartup + sec;
        isSleeping = true;
    }

    private void changeLastZone(Transform zoneAvant, Transform zone)
    {
        Vector3 randomSphere = Random.insideUnitSphere * 7;

        zone.position = new Vector3(zoneAvant.position.x + randomSphere.x, 1.70005f, zoneAvant.position.z + randomSphere.z);

        pathPoints[pathPoints.Length - 1] = zone;
    }

    public void maxValueCurrentPath() { currentPathPoint = int.MaxValue; }
    public void doubleVitesse() { speed = 13; }
    private int nbResteInfecte()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Personnage");
        int resteInfecte = gos.Length;

        foreach (GameObject go in gos)
            if (go.GetComponent<Caracteristiques>().isInfecte())
                resteInfecte--;

        return resteInfecte;
    }
}
