using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class Caracteristiques : MonoBehaviour {

    public Material etudiantInfecte;
    public Material etudiant;
    public Material etudiantImmunise;
    public NavMeshAgent nav;

    /// <summary>
    /// Si vrai, la personne est malade
    /// </summary>
    public bool infecte = false;

    /// <summary>
    /// Si vrai, la personne ne peut pas être malade (temporaire)
    /// </summary>
    public bool resistant = false;

    /// <summary>
    /// Si vrai, la personne ne peut pas être malade (définitif)
    /// </summary>
    public bool immunise = false;

    /// <summary>
    /// Si vrai, la personne peut être soignée
    /// </summary>
	public bool soignable = true;

    /// <summary>
    /// Si vrai, la personne est suivie par un autre personnage inconnu
    /// </summary>
    public bool suivi = false;

    #region Méthodes

    /// <summary>
    /// Méthode d'initialisation du script
    /// </summary>
    void Start () 
    {
        
	}
	
	/// <summary>
    /// Méthode appelée une fois par frame
    /// </summary>
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

    /// <summary>
    /// Méthode appelée lors d'une "collision à distance"
    /// </summary>
    /// <param name="other">La collision</param>
    void OnTriggerEnter(Collider other)
    {
        GameObject source = other.gameObject;

        if (tag.Equals("Personnage"))
        {
            if ((source.tag.Equals("Personnage") && source.GetComponent<Caracteristiques>().isInfecte()) || source.tag.Equals("Satan"))
                GetComponent<Caracteristiques>().infecter();

            if (source.tag.Equals("Boss") && soignable && infecte)
                GetComponent<Caracteristiques>().desinfecter();
        }
    }

    /// <summary>
    /// Méthode appelée tant que la "collision à distance" a toujours lieu
    /// </summary>
    /// <param name="other">La collision</param>
    void OnTriggerStay(Collider other)
    {
        GameObject source = other.gameObject;

        if (source.tag.Equals("Boss") && soignable && infecte)
            GetComponent<Caracteristiques>().desinfecter();
    }

    /// <summary>
    /// Méthode appelée lorsque la "collision à distance" prend fin
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        resistant = false;
    }

    /// <summary>
    /// Permet d'infecter des étudiants, et de redéfinir leur IA aléatoirement
    /// </summary>
    public void infecter()
    {
        if (!infecte && !immunise && !resistant)
        {
            if (GetComponent<Renderer>() != null) GetComponent<Renderer>().material = etudiantInfecte;
            infecte = true;

            GameObject.Find("ENSC").GetComponent<UIController>().upContamines();

            int rd = Random.Range(0, 101);

            // Changement d'attitude
            if (rd < 10) // 10%
            {
                GetComponent<IA>().agressif();
                GameObject.Find("ENSC").GetComponent<UIController>().upAgressif();
            }
            else if (rd < 40) // 30%
            {
                GetComponent<IA>().assistance();
                GameObject.Find("ENSC").GetComponent<UIController>().upAssistance();
            }
            else if (rd < 50) // 10%
            {
                GetComponent<IA>().peur();
                GameObject.Find("ENSC").GetComponent<UIController>().upPeur();
            }
        }
    }

    /// <summary>
    /// Permet de soigner les étudiants malades
    /// </summary>
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

            /* On redonne une IA passive aux étudiants soignés */
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

    #endregion

    #region Accesseurs

    /// <summary>
    /// Permet de redéfinir l'état d'infection de l'entité
    /// </summary>
    /// <param name="infecte">Infecter ?</param>
    public void setInfecte(bool infecte) { this.infecte = infecte; }

    /// <summary>
    /// Permet de savoir si l'entité est infectée
    /// </summary>
    /// <returns>Vrai si l'entité est infectée, faux sinon</returns>
    public bool isInfecte() { return infecte; }

    /// <summary>
    /// Permet de savoir si l'entité est immunisée
    /// </summary>
    /// <returns>Vrai si l'entité est immunisée, faux sinon</returns>
    public bool isImmunise() { return immunise; }

    /// <summary>
    /// Permet de savoir si l'entité est résistante
    /// </summary>
    /// <returns>Vrai si l'entité est résistante, faux sinon</returns>
    public bool isResistant() { return resistant; }

    #endregion
}
