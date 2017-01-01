using UnityEngine;
using System.Collections;

public interface IIA {

    /// <summary>
    /// Méthode d'initialisation du script
    /// </summary>
    /// <param name="gameObject">L'entité recevant l'IA</param>
    void Start(GameObject gameObject);

    /// <summary>
    /// Méthode appelée à chaque frame : gère le déplacement
    /// </summary>
    void Update();

    /// <summary>
    /// Permet de savoir si l'entité est agressive
    /// </summary>
    /// <returns>Vrai si l'entité est agressive, faux sinon</returns>
    bool isAgressif();

    /// <summary>
    /// Permet de savoir si l'entité est passive
    /// </summary>
    /// <returns>Vrai si l'entité est passive, faux sinon</returns>
    bool isPassif();

    /// <summary>
    /// Permet de savoir si l'entité est un soigneur
    /// </summary>
    /// <returns>Vrai si l'entité est un soigneur, faux sinon</returns>
    bool isSoigneur();

    /// <summary>
    /// Permet de savoir si l'entité a besoin d'assistance
    /// </summary>
    /// <returns>Vrai si l'entité a besoin d'assistance, faux sinon</returns>
    bool isAssistance();

    /// <summary>
    /// Permet de savoir si l'entité a peur
    /// </summary>
    /// <returns>Vrai si l'entité a peur, faux sinon</returns>
    bool isPeur();
}
