using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformFall : MonoBehaviour {

    public float fallDelay = 1f;
    private bool doIfloat;

    private Rigidbody2D rb2d;
    private Vector2 originalPosition;
    private GameObject sprite;

	// Use this for initialization
	void Awake ()
    {
        sprite = GetComponentInChildren<SpriteRenderer>().gameObject;
        rb2d = GetComponent<Rigidbody2D>();
        if (Random.Range(1,4 + GameController.nivel) > 3)
        {
            doIfloat = true;
            sprite.GetComponent<SpriteRenderer>().color = Color.red;
        }
        else
        {
            doIfloat = false;
            sprite.GetComponent<SpriteRenderer>().color = Color.green;
        }
	}

    void Start ()
    {
        originalPosition = transform.position;
        DeathTrigger.enReinicio += Reiniciar;
    }

    void OnDestroy ()
    {
        DeathTrigger.enReinicio -= Reiniciar;
    }

    void OnCollisionEnter2D (Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (doIfloat)
            {
                Invoke("Fall", fallDelay);
            }
        }
    }

    void Fall()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.isKinematic = false;
        Invoke("GoToOrigin", 5);
    }

    void Reappear()
    {
        this.gameObject.SetActive(true);
        CancelInvoke();
    }

    void GoToOrigin()
    {
        if (doIfloat)
        {
            rb2d = GetComponent<Rigidbody2D>();
            rb2d.isKinematic = true;
            transform.rotation = Quaternion.identity;
            transform.position = originalPosition;
            this.gameObject.SetActive(false);
        }
    }

    void Reiniciar()
    {
        if (doIfloat)
        {
            GoToOrigin();
            Reappear();
        }
    }
}
