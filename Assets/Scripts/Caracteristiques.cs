using UnityEngine;
using System.Collections;

public class Caracteristiques : MonoBehaviour {

    public Material etudiantInfecte;
    public bool infecte;

	// Use this for initialization
	void Start () {
        infecte = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter(Collision collision)
    {
        GameObject source = collision.gameObject;
        
        if (source.tag.Equals("Personnage"))
            if (source.GetComponent<Caracteristiques>().isInfecte())
            {
                GetComponent<Caracteristiques>().infecter();
            }
    }

    public void infecter()
    {
        GetComponent<Renderer>().material = etudiantInfecte;
        GetComponent<Caracteristiques>().setInfecte(true);
    }

    public void setInfecte(bool infecte) { this.infecte = infecte; }
    public bool isInfecte() { return infecte; }
}
