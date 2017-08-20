using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour {
    GameController gc;

    public void Jugar()
    {
        gc = GameController.Instancia;
        gc.StartGame();
    }
}
