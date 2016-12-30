using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public Camera[] cameras;

    /// <summary>
    /// L'identifiant de la caméra active dans le tableau
    /// </summary>
    private int active = 0;

    void Start()
    {
        if (cameras.Length > 0)
        {
            cameras[0].enabled = true;

            for (int i = 1; i < cameras.Length; ++i)
            {
                cameras[i].enabled = false;
            }
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
}