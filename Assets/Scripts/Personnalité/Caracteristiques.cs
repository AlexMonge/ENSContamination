using UnityEngine;
using System.Collections;

public class Caracteristiques : MonoBehaviour {

    public Material etudiantInfecte;
    public Material etudiant;
    public Material etudiantImmunise;
    public UnityEngine.AI.NavMeshAgent nav;

    public bool infecte = false;
    public bool immunise = false;
    public bool peur = false;
	public bool soignable = true;
    public bool suivi = false;
    public bool resistant = false;

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
            immunise = true;
            infecte = false;
            resistant = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        GameObject source = other.gameObject;

        if (((source.tag.Equals("Personnage") && source.GetComponent<Caracteristiques>().isInfecte()) || source.tag.Equals("Satan")))
            GetComponent<Caracteristiques>().infecter();

        if (source.tag.Equals("Boss") && soignable && infecte)
            GetComponent<Caracteristiques>().desinfecter();
    }

    void OnTriggerStay(Collider other)
    {
        GameObject source = other.gameObject;

        if (source.tag.Equals("Boss") && soignable && infecte)
            GetComponent<Caracteristiques>().desinfecter();
    }

    void OnTriggerExit(Collider other)
    {
        resistant = false;
    }

    public void infecter()
    {
        if (!infecte && !immunise && !resistant)
        {
            if (GetComponent<Renderer>() != null) GetComponent<Renderer>().material = etudiantInfecte;
            infecte = true;

            GameObject.Find("ENSC").GetComponent<UIController>().upContamines();

            int rd = Random.Range(0, 101);

            if (rd < 10)
            {
                GetComponent<IA>().agressif();
                GameObject.Find("ENSC").GetComponent<UIController>().upAgressif();
            }
            else if (rd < 40)
            {
                GetComponent<IA>().assistance();
                GameObject.Find("ENSC").GetComponent<UIController>().upAssistance();
            }
            else if (rd < 50)
            {
                GetComponent<IA>().peur();
                GameObject.Find("ENSC").GetComponent<UIController>().upPeur();
            }
        }
    }

    public void desinfecter()
    {
        if (infecte)
        {
            if (GetComponent<Renderer>() != null) GetComponent<Renderer>().material = etudiant;
            infecte = false;
            resistant = true;
            GameObject.Find("ENSC").GetComponent<UIController>().downContamines();

            if (Random.Range(0, 201) == 0)
            {
                if (!immunise)
                {
                    immunise = true;
                    GameObject.Find("ENSC").GetComponent<UIController>().upImmunises();
                    if (GetComponent<Renderer>() != null) gameObject.GetComponent<Renderer>().material = etudiantImmunise;
                }
            }

            if (GetComponent<IA>().isAgressif())
            {
                GetComponent<IA>().passif();
                GameObject.Find("ENSC").GetComponent<UIController>().downAgressif();
            }

            if (GetComponent<IA>().isAssistance())
            {
                GetComponent<IA>().passif();
                GameObject.Find("ENSC").GetComponent<UIController>().downAssistance();
            }

            if (GetComponent<IA>().isPeur())
            {
                GetComponent<IA>().passif();
                GameObject.Find("ENSC").GetComponent<UIController>().downPeur();
            }
        }
    }

    public void setInfecte(bool infecte) { this.infecte = infecte; }
    public bool isInfecte() { return infecte; }
    public bool isImmunise() { return immunise; }
    public bool isResistant() { return resistant; }
}
