using UnityEngine;
using System.Collections;

public class IA : MonoBehaviour {

    public IIA ia;

    void Start()
    {
        switch (this.tag)
        {
            case "Personnage":
                ia = new IPassif();
                break;

            case "Boss":
                ia = new ISoigneur();
                break;

            case "Satan":
                ia = new IAgressif();
                break;

            default:
                ia = new IPassif();
                break;
        }

        ia.Start(gameObject);
    }

    void Update()
    {
        ia.Update(); 
    }

    public void passif()
    {
        ia = new IPassif();
        ia.Start(gameObject);
    }

    public void soigneur()
    {
        ia = new ISoigneur();
        ia.Start(gameObject);
    }

    public void agressif()
    {
        ia = new IAgressif();
        ia.Start(gameObject);
    }

    public bool isAgressif() { return ia.isAgressif(); }
    public bool isPassif() { return ia.isPassif(); }
    public bool isSoigneur() { return ia.isSoigneur(); }
}
