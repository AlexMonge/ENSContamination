using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour {

    public IIA ia;
    private Caractere caractere;

    void Start()
    {
        caractere = new Caractere();

        switch (this.tag)
        {
            case "Personnage":
                ia = new IPassif(caractere);
                break;

            case "Boss":
                ia = new ISoigneur(caractere);
                break;

            case "Satan":
                ia = new IAgressif(caractere);
                break;

            default:
                ia = new IPassif(caractere);
                break;
        }

        ia.Start(gameObject);
    }

    void Update()
    {
        ia.Update(); 
    }

    /// <summary>
    /// Redéfinir l'IA pour un comportement passif
    /// </summary>
    public void passif()
    {
        ia = new IPassif(caractere);
        ia.Start(gameObject);
    }

    /// <summary>
    /// Redéfinir l'IA pour un comportement de médecin (va vers les malades)
    /// </summary>
    public void soigneur()
    {
        ia = new ISoigneur(caractere);
        ia.Start(gameObject);
    }

    /// <summary>
    /// Redéfinir l'IA pour un comportement agressif
    /// </summary>
    public void agressif()
    {
        ia = new IAgressif(caractere);
        ia.Start(gameObject);
    }

    /// <summary>
    /// Redéfinir l'IA pour un comportement de recherche d'assistance
    /// </summary>
    public void assistance()
    {
        ia = new IAssistance(caractere);
        ia.Start(gameObject);
    }

    /// <summary>
    /// Redéfinir l'IA pour un comportement de peur
    /// </summary>
    public void peur()
    {
        ia = new IPeur(caractere);
        ia.Start(gameObject);
    }

    public bool isAgressif() { return ia.isAgressif(); }
    public bool isPassif() { return ia.isPassif(); }
    public bool isSoigneur() { return ia.isSoigneur(); }
    public bool isAssistance() { return ia.isAssistance(); }
    public bool isPeur() { return ia.isPeur(); }
}
