using UnityEngine;
using System.Collections;

public class MushGen : MonoBehaviour {

    public GameObject[] gameObjects;
    public int nb;
    private int generes;

	// Use this for initialization
	void Start () {
        if (nb <= 0)
            nb = 10;

        generes = nb;
	}
	
	// Update is called once per frame
	void Update () {
        if (generes > 0)
        {
            GameObject go = Instantiate<GameObject>(gameObjects[Random.Range(0, gameObjects.Length)]);
            go.transform.localPosition = new Vector3(Random.Range(-50, 50)-10, 1, Random.Range(-50, 50)-50);
            --generes;
        }
	}
}
