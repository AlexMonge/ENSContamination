using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour {

    /// <summary>
    /// Implémentation des classes implémentant l'interface IA
    /// Design Pattern Stategy
    /// </summary>
    private IIA ia;

    /// <summary>
    /// Caractère de l'entité
    /// </summary>
    private Caractere caractere;


    #region Méthodes

    void Start()
    {
        caractere = new Caractere();
        
        // On donne l'IA aux entités en fonction de leur tag
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

    #endregion

    #region Accesseurs

    /// <summary>
    /// Permet de savoir si l'entité est agressive
    /// </summary>
    /// <returns>Vrai si l'entité est agressive, faux sinon</returns>
    public bool isAgressif() { return ia.isAgressif(); }

    /// <summary>
    /// Permet de savoir si l'entité est passive
    /// </summary>
    /// <returns>Vrai si l'entité est passive, faux sinon</returns>
    public bool isPassif() { return ia.isPassif(); }

    /// <summary>
    /// Permet de savoir si l'entité est un soigneur
    /// </summary>
    /// <returns>Vrai si l'entité est un soigneur, faux sinon</returns>
    public bool isSoigneur() { return ia.isSoigneur(); }

    /// <summary>
    /// Permet de savoir si l'entité a besoin d'assistance
    /// </summary>
    /// <returns>Vrai si l'entité a besoin d'assistance, faux sinon</returns>
    public bool isAssistance() { return ia.isAssistance(); }

    /// <summary>
    /// Permet de savoir si l'entité a peur
    /// </summary>
    /// <returns>Vrai si l'entité a peur, faux sinon</returns>
    public bool isPeur() { return ia.isPeur(); }
    #endregion
}
