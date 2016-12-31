using UnityEngine;
using System.Collections;

public class Generation : MonoBehaviour {

    public GameObject[] gameObjects;

    /// <summary>
    /// Nombre d'entités qu'il reste à générer
    /// </summary>
    private int generes;

    // Use this for initialization
    void Start()
    {
        int nb = Constantes.nbEtudiants;

        if (nb < 0 || nb > 160)
            nb = 160;

        generes = nb;
    }

    // Update is called once per frame
    void Update()
    {
        if (generes > 0)
        {
            GameObject go = Instantiate<GameObject>(gameObjects[Random.Range(0, gameObjects.Length)]);
            go.transform.localPosition = new Vector3(Random.Range(-50, 50) - 10, 1, Random.Range(-50, 50) - 50);
            --generes;
            GetComponent<UIController>().ajouterPersonne();
        }
    }
}