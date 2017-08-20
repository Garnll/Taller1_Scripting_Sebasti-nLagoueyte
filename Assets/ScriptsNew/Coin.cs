using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    GameController gameController;
    AudioSource audioSo;

    public ParticleSystem particles;
    private ParticleSystem copiaParticles;

    void Start()
    {
        gameController = GameController.Instancia;
        audioSo = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("DestroyThis", 2);
            audioSo.Play();
            copiaParticles = Instantiate(particles, transform.position, particles.transform.rotation);
            copiaParticles.Play();
            gameController.SetScore(100);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    void DestroyThis()
    {
        Destroy(copiaParticles.gameObject);
        Destroy(gameObject);
    }
}
