using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalTrigger : MonoBehaviour {

    GameController gameController;
    AudioSource audioSo; 

    [SerializeField]
    AudioClip[] clips;

    [SerializeField]
    private ParticleSystem particles;

    private void Start()
    {
        gameController = GameController.Instancia;
        audioSo = GetComponent<AudioSource>();
    }

	private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Sonar();
            Invoke("SiguienteNivel", 1);
        }
    }

    private void Sonar()
    {
        if (!audioSo.isPlaying)
        {
            Particular();
            audioSo.clip = clips[Random.Range(0, clips.Length)];
            audioSo.Stop();
            audioSo.Play();
        }
    }

    private void Particular()
    {
        particles.Play();
    }

    private void SiguienteNivel()
    {
        gameController.SeguirNivel();
    }
}
