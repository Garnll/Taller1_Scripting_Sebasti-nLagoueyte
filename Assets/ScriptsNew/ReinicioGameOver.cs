using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReinicioGameOver : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Invoke("Reiniciar", 5);
	}
	
    void Reiniciar ()
    {
        GameController gc = GameController.Instancia;

        gc.RestartEverything();
    }
}
