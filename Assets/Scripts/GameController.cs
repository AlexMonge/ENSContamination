using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject[] gos = GameObject.FindGameObjectsWithTag("Personnage");
            GameObject personnage = gos[Random.Range(0, gos.Length)];

            personnage.GetComponent<Caracteristiques>().infecter();
        }
	}
}
