using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

    public string simulation;
    public UnityEngine.UI.InputField nbEtu;

	public void lancer()
    {
        Constantes.nbEtudiants = int.Parse(nbEtu.text);

        SceneManager.LoadScene(simulation);
    }

    public void quitter()
    {
        Application.Quit();
    }
}
