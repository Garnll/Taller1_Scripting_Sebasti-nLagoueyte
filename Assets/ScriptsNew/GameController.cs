using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController  {

    private static GameController instancia = null;
    public static int nivel;

    private UIController ui;
    private int vidas = 0;
    private int score = 0;
    private Vector3 originalPosition;

    public static GameController Instancia
    {
        get
        {
            if (instancia == null)
            {
                instancia = new GameController();
            }
            return instancia;
        }
    }

    private GameController()
    {
        vidas = 0;
        score = 0;
    }

    public void Setvidas(int _vidas)
    {
        vidas += _vidas;
        if (vidas >= 3)
        {
            vidas = 3;
        }
        if (UIController.vidasText)
        {
            UIController.vidasText.text = "Vidas: " + vidas.ToString();
            UIController.scoreText.text = "Score: " + score.ToString();
        }

        if (vidas <= 0)
        {
            vidas = 0;
            score = 0;
            EndGame();
        }
    }

    public void SetScore(int _score)
    {
        score += _score;
        if (UIController.scoreText)
        {
            UIController.scoreText.text = "Score: " + score.ToString();
        }
    }

    public bool End()
    {
        if (vidas == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void SetOrigin(Vector3 _originalPosition)
    {
        originalPosition = _originalPosition;
    }

    public void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void FinishGame()
    {
        SceneManager.LoadScene("FinishGame");
    }

    public void RestartEverything()
    {
        vidas = 0;
        score = 0;
        SceneManager.LoadScene(0);

        Debug.Log("The Game Has Ended");
    }

    public Vector3 GetOrigin()
    {
        return originalPosition;
    }

    public void SeguirNivel()
    {
        if (SceneManager.sceneCountInBuildSettings > SceneManager.GetActiveScene().buildIndex + 1)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else if (SceneManager.sceneCountInBuildSettings == SceneManager.GetActiveScene().buildIndex + 1)
        {
            FinishGame();
        }
        else
        {
            EndGame();
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Nivel1");
    }
}
