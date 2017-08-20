using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

    GameController gameController;

    public ParticleSystem particles;
    private ParticleSystem copiaP;

    public delegate void OcurrenciasDeMuerte();
    public static event OcurrenciasDeMuerte enReinicio;

	void Start ()
    {
        gameController = GameController.Instancia;
	}
	

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            copiaP = Instantiate(particles, other.transform.position, particles.transform.rotation);
            copiaP.Play();
            gameController.Setvidas(-1);
            if (!gameController.End())
            {
                enReinicio();
            }
            //Application.LoadLevel(Application.loadedLevel);
            //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
