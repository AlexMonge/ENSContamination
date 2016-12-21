using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private int nbPersonne;
    private int passif;
    private int agressif;
    private int contamines;
    private int sains;
    private int assistance;
    private int peur;
    private int immunises;

    public Text passifText;
    public Text agressifText;
    public Text contaminesText;
    public Text assistanceText;
    public Text peurText;
    public Text immunisesText;

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

    public void upPassif() { ++passif; --agressif; }
    public void downPassif() { --passif; ++agressif; }

    public void upAgressif() { ++agressif; --passif; }
    public void downAgressif() { --agressif; ++passif; }

    public void upContamines() { ++contamines; --sains; }
    public void downContamines() { --contamines; ++sains; }

    public void upSains() { ++sains; --contamines; }
    public void downSains() { --sains; ++contamines; }

    public void upAssistance() { ++assistance; --passif; }
    public void downAssistance() { --assistance; ++passif; }

    public void upPeur() { ++peur; --passif; }
    public void downPeur() { --peur; ++passif; }

    public void upImmunises() { ++immunises; }

    public void ajouterPersonne() { ++nbPersonne; ++passif; ++sains; }

    public int getNbPersonne() { return nbPersonne; }
    public double getPourcentageContamines() { return ((double) contamines / (double) nbPersonne) * 100; }
}
