using UnityEngine;
using System.Collections;

public class Caracteristiques : MonoBehaviour {

    public Material etudiantInfecte;
    public Material etudiant;
    public bool infecte;
    public bool protect;

    // Use this for initialization
    void Start () 
    {
        int perinf = Random.Range(0, 50);

        if (perinf == 0)
            infecte = true;
        else
            infecte = false;

        protect = false;
	}
	
	// Update is called once per frame
	void Update () 
    {        
        OnTriggerEnter(GetComponent<Collider>()); 
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject source = collision.gameObject;

        if (source.tag.Equals("Personnage") && protect == false)
            if (source.GetComponent<Caracteristiques>().isInfecte())
                GetComponent<Caracteristiques>().infecter();
    }

    void OnTriggerEnter(Collider bc)
    {
        GameObject source = bc.gameObject;

        if (source.tag.Equals("Boss"))
        {
            GetComponent<Caracteristiques>().desinfecter();
            protect = true;
        }        
    }

    void OnTriggerExit(Collider bc)
    {
        GameObject source = bc.gameObject;

        if (source.tag.Equals("Boss"))
        {
            GetComponent<Caracteristiques>().desinfecter();
            protect = false;
        }
    }

    public void infecter()
    {
        GetComponent<Renderer>().material = etudiantInfecte;
        GetComponent<Caracteristiques>().setInfecte(true);
    }

    public void desinfecter()
    {
        GetComponent<Renderer>().material = etudiant;
        GetComponent<Caracteristiques>().setInfecte(false);
    }

    public void setInfecte(bool infecte) { this.infecte = infecte; }
    public bool isInfecte() { return infecte; }
}
