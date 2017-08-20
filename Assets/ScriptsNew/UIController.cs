using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static Text vidasText;
    public static Text scoreText;

	void Start ()
    {
        if (vidasText == null)
        {
            vidasText = GameObject.Find("VidasText").GetComponent<Text>();
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        }
	}
	
	void OnDestroy ()
    {
        vidasText = null;
        scoreText = null;
    }
}
