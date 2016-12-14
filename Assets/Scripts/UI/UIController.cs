using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    private int passif;
    private int agressif;
    private int contamines;
    private int sains;

    public Text passifText;
    public Text agressifText;
    public Text contaminesText;
    public Text sainsText;

	// Use this for initialization
	void Start () {
        passif = 0;
        agressif = 0;
        contamines = 0;
        sains = 0;
	}
	
	// Update is called once per frame
	void Update () {
        passifText.text = "Personnes passives : " + passif;
        agressifText.text = "Personnes agressives : " + agressif;
        contaminesText.text = "Contaminés : " + contamines;
        sainsText.text = "Sains : " + sains;
	}

    public void upPassif() { ++passif; --agressif; }
    public void downPassif() { --passif; ++agressif; }

    public void upAgressif() { ++agressif; --passif; }
    public void downAgressif() { --agressif; ++passif; }

    public void upContamines() { ++contamines; --sains; }
    public void downContamines() { --contamines; ++sains; }

    public void upSains() { ++sains; --contamines; }
    public void downSains() { --sains; ++contamines; }

    public void ajouterPersonne() { ++passif; ++sains; }
}
