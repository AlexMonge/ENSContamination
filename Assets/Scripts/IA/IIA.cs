using UnityEngine;
using System.Collections;

public interface IIA {

    void Start(GameObject gameObject);
    void Update();
    bool isAgressif();
    bool isPassif();
    bool isSoigneur();
    bool isAssistance();
    bool isPeur();
}
