using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    /// <summary>
    /// Nombre d'étudiants
    /// </summary>
    private int nbPersonne;

    /// <summary>
    /// Nombre de personnes passives
    /// </summary>
    private int passif;

    /// <summary>
    /// Nombre de personnes agressives
    /// </summary>
    private int agressif;

    /// <summary>
    /// Nombre de personnes contaminées
    /// </summary>
    private int contamines;

    /// <summary>
    /// Nombre de personnes saines
    /// </summary>
    private int sains;

    /// <summary>
    /// Nombre de personnes ayant besoin d'assistance
    /// </summary>
    private int assistance;

    /// <summary>
    /// Nombre de personnes ayant peur
    /// </summary>
    private int peur;

    /// <summary>
    /// Nombre de personnes immunisées à la maladie
    /// </summary>
    private int immunises;

    /* Textes de l'interface */
    public Text passifText;
    public Text agressifText;
    public Text contaminesText;
    public Text assistanceText;
    public Text peurText;
    public Text immunisesText;


    #region Méthodes

    // Use this for initialization
    void Start () {
        nbPersonne = 0;
        passif = 0;
        agressif = 0;
        contamines = 0;
        sains = 0;
        assistance = 0;
        peur = 0;
        immunises = 0;
	}
	
	// Update is called once per frame
	void Update () {
        passifText.text = "Personnes passives : " + passif;
        agressifText.text = "Personnes agressives : " + agressif;
        contaminesText.text = "Contaminés : " + (int) getPourcentageContamines() + "%";
        assistanceText.text = "Besoin d'assistance : " + assistance;
        peurText.text = "Personnes effrayées : " + peur;
        immunisesText.text = "Immunisés : " + immunises;
    }

    /// <summary>
    /// Augmente le nombre d'entités passives
    /// </summary>
    public void upPassif() { ++passif; --agressif; }

    /// <summary>
    /// Diminue le nombre d'entités passives
    /// </summary>
    public void downPassif() { --passif; ++agressif; }

    /// <summary>
    /// Augmente le nombre d'entités agressives
    /// </summary>
    public void upAgressif() { ++agressif; --passif; }

    /// <summary>
    /// Diminue le nombre d'entités agressives
    /// </summary>
    public void downAgressif() { --agressif; ++passif; }

    /// <summary>
    /// Augmente le nombre d'entités contaminées
    /// </summary>
    public void upContamines() { ++contamines; --sains; }

    /// <summary>
    /// Diminue le nombre d'entités contaminées
    /// </summary>
    public void downContamines() { --contamines; ++sains; }

    /// <summary>
    /// Augmente le nombre d'entités saines
    /// </summary>
    public void upSains() { ++sains; --contamines; }

    /// <summary>
    /// Diminue le nombre d'entités saines
    /// </summary>
    public void downSains() { --sains; ++contamines; }

    /// <summary>
    /// Augmente le nombre d'entités ayant besoin d'assistance
    /// </summary>
    public void upAssistance() { ++assistance; --passif; }

    /// <summary>
    /// Diminue le nombre d'entités ayant besoin d'assistance
    /// </summary>
    public void downAssistance() { --assistance; ++passif; }

    /// <summary>
    /// Augmente le nombre d'entités ayant peur
    /// </summary>
    public void upPeur() { ++peur; --passif; }

    /// <summary>
    /// Diminue le nombre d'entités ayant peur
    /// </summary>
    public void downPeur() { --peur; ++passif; }

    /// <summary>
    /// Augmente le nombre d'entités immunisés
    /// </summary>
    public void upImmunises() { ++immunises; }

    /// <summary>
    /// Ajoute une nouvelle personne dans les statistiques (passive et saine par défaut)
    /// </summary>
    public void ajouterPersonne() { ++nbPersonne; ++passif; ++sains; }

    #endregion

    #region Accesseurs

    /// <summary>
    /// Retourne le nombre d'étudiants de la simulation
    /// </summary>
    /// <returns>Nombre de personnes de la simulation</returns>
    public int getNbPersonne() { return nbPersonne; }

    /// <summary>
    /// Retourne le pourcentage d'étudiants contaminés
    /// </summary>
    /// <returns>Le pourcentage d'étudiants contaminés</returns>
    public double getPourcentageContamines() { return ((double)contamines / (double)nbPersonne) * 100; }

    #endregion
}
