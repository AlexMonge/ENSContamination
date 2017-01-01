using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    /// <summary>
    /// Tableau des caméras de la scène
    /// </summary>
    public Camera[] cameras;

    /// <summary>
    /// L'identifiant de la caméra active dans le tableau
    /// </summary>
    private int active = 0;

    #region Méthodes

    void Start()
    {
        // On active uniquement la 1ere caméra
        if (cameras.Length > 0)
        {
            cameras[0].enabled = true;

            for (int i = 1; i < cameras.Length; ++i)
                cameras[i].enabled = false;
        }
    }


    void Update()
    {
        // Si on presse Flèche_Haut, on passe à la caméra suivante
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            ++active;

            if (active > cameras.Length - 1)
            {
                active = 0;
                cameras[cameras.Length - 1].enabled = false;
            }
            else
                cameras[active - 1].enabled = false;

            cameras[active].enabled = true;
        }

        // Si on presse Flèche_Bas, on passe à la caméra précédente
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            --active;

            if (active < 0)
            {
                active = cameras.Length - 1;
                cameras[0].enabled = false;
            }
            else
                cameras[active + 1].enabled = false;

            cameras[active].enabled = true;
        }
    }

    #endregion
}