using UnityEngine;
using System.Collections;

public class Caracteristiques : MonoBehaviour {

    public Material etudiantInfecte;
    public Material etudiant;
    public NavMeshAgent nav;

    public bool infecte = false;
    public bool resistant = false;
    public bool peur = false;
	public bool soignable = true;
    public bool suivi = false;

    // Use this for initialization
    void Start () 
    {
        
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (tag.Equals("Satan"))
        {
            soignable = false;
            infecte = true;
        }

        if (tag.Equals("Boss"))
        {
            soignable = true;
            resistant = true;
            infecte = false;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject source = other.gameObject;

         if (((source.tag.Equals("Personnage") && source.GetComponent<Caracteristiques>().isInfecte()) || source.tag.Equals("Satan")) && !resistant)
            GetComponent<Caracteristiques>().infecter();

        if (source.tag.Equals("Boss") && soignable)
            GetComponent<Caracteristiques>().desinfecter();
    }

    void OnTriggerExit(Collider other)
    {
        resistant = false;
    }

    public void infecter()
    {
		if (GetComponent<Renderer>() != null) GetComponent<Renderer>().material = etudiantInfecte;
        infecte = true;

        if (Random.Range(0, 20) == 0)
            GetComponent<IA>().agressif();
    }

    public void desinfecter()
    {
		if (GetComponent<Renderer>() != null) GetComponent<Renderer>().material = etudiant;
        infecte = false;
        resistant = true;

        if (GetComponent<IA>().isAgressif())
            GetComponent<IA>().passif();
    }

    public void setInfecte(bool infecte) { this.infecte = infecte; }
    public bool isInfecte() { return infecte; }
}
