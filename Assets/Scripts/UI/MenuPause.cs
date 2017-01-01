using System.Linq;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
 
 
public class MenuPause : MonoBehaviour 
{
    /// <summary>
    /// Permet de savoir si le jeu est en pause ou non.
    /// </summary>
    private bool isPaused = false; 
     
    #region Methodes
     
    void Start () 
    {
     
    }
     
     
    void Update () 
    {
        // Si le joueur appuis sur Echap alors la valeur de isPaused devient le contraire.
        if(Input.GetKeyDown(KeyCode.Escape))
            isPaused = !isPaused;
 
 
        if(isPaused)
            Time.timeScale = 0f; // Le temps s'arrete
         
        else
            Time.timeScale = 1.0f; // Le temps reprend
 
 
    }
 
    void OnGUI ()
    {
        if(isPaused)
        {
            Color saveColor = GUI.color;

            GUI.color = new Color(0.0f, 0.0f, 0.0f, 1f);
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
            GUI.color = saveColor;

            // Si le bouton est pressé, alors isPaused devient faux donc le jeu reprend.
            if (GUI.Button(new Rect(Screen.width / 2 - 20, Screen.height / 3 + 20, 130, 50), "Continuer"))
            {
                isPaused = false;
            }
 			// Si le bouton est pressé alors on revient au menu completement le jeu ou charge la scene "MainMenu"
            if (GUI.Button(new Rect(Screen.width / 2 - 20, Screen.height / 3 + 80, 130, 50), "Menu principal"))
            {
                SceneManager.LoadScene("MainMenu");
            }
            // Si le bouton est pressé alors on ferme completement le jeu 
            if (GUI.Button(new Rect(Screen.width / 2 - 20, Screen.height / 3 + 140, 130, 50), "Quitter"))
            {
                Application.Quit(); 
            }
        }
    }
     
    #endregion
}