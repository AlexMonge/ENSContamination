using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour {

    public InputField nbEtu;

    #region Méthodes

    /// <summary>
    /// permet de lancer la simulation
    /// </summary>
    public void lancer()
    {
        try
        {
            Constantes.nbEtudiants = int.Parse(nbEtu.text);
        }
        catch
        {
            Constantes.nbEtudiants = 160;
        }

        SceneManager.LoadScene("Simulation");
    }

    /// <summary>
    /// Permet de quitter l'application
    /// </summary>
    public void quitter()
    {
        Application.Quit();
    }

    #endregion
}
