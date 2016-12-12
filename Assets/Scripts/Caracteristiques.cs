using UnityEngine;
using System.Collections;

public class Caracteristiques : MonoBehaviour {

    public Material etudiantInfecte;
    public Material etudiant;
    public NavMeshAgent nav;
    public bool infecte;
    public bool protect;
    public bool fear = false;
	public bool soignable = true;
    public bool suivi = false;

    // Use this for initialization
    void Start () 
    {
        int perinf = Random.Range(0, 50);

        if (perinf == 0)
            infecte = true;
		else if (soignable)
            infecte = false;

        protect = false;
	}
	
	// Update is called once per frame
	void Update () 
    {        
		if (tag.Equals ("Satan")) {
			soignable = false;
			infecte = true;
		}
    }

    void OnCollisionEnter(Collision collision)
    {
        GameObject source = collision.gameObject;

        if ((source.tag.Equals("Personnage") || source.tag.Equals("Satan")) && protect == false)
            if (source.GetComponent<Caracteristiques>().isInfecte())
                GetComponent<Caracteristiques>().infecter();
    }

    void OnTriggerStay(Collider bc)
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
		if (GetComponent<Renderer>() != null) GetComponent<Renderer>().material = etudiantInfecte;
        GetComponent<Caracteristiques>().setInfecte(true);
    }

    public void desinfecter()
    {
		if (GetComponent<Renderer>() != null) GetComponent<Renderer>().material = etudiant;
        GetComponent<Caracteristiques>().setInfecte(false);
    }

    public void setInfecte(bool infecte) { this.infecte = infecte; }
    public bool isInfecte() { return infecte; }
}
