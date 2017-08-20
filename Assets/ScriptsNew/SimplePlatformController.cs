using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePlatformController : MonoBehaviour {

    private GameController gameController;

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;
    [HideInInspector]
    public bool jumpFromWall = false;

    public int vidasMax = 3;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public Transform groundCheck;
    public Transform rightCheck;
    public ParticleSystem[] particles;
    public AudioClip[] audioClips;

    private bool grounded = false;
    private bool walled = false;
    private bool notOnGround = false;
    private Animator anim;
    private Rigidbody2D rb2d;
    private AudioSource audioS;
    private ParticleSystem[] copyOfP;

    // Use this for initialization
    void Awake ()
    {
        gameController = GameController.Instancia;

        anim = GetComponent<Animator>();
        rb2d = GetComponent< Rigidbody2D > ();
        audioS = GetComponent<AudioSource>();
        jump = false;
        jumpFromWall = false;
        notOnGround = false;
    }
	
    void Start()
    {
        copyOfP = new ParticleSystem [particles.Length];
        gameController.SetOrigin(transform.position);
        Invoke("LaterSetLives", 1);
        audioS.clip = audioClips[0];
        DeathTrigger.enReinicio += Restart;
    }

    void LaterSetLives()
    {
        gameController.Setvidas(vidasMax);
    }

    void OnDestroy()
    {
        DeathTrigger.enReinicio -= Restart;
    }

	// Update is called once per frame
	void Update ()
    {
        grounded = Physics2D.BoxCast(groundCheck.position, new Vector2(1,0.5f), 0f, groundCheck.position, 0f, 1 << LayerMask.NameToLayer("Ground"));
        if (!grounded)
        {
            walled = Physics2D.BoxCast(rightCheck.position, new Vector2(0.1f, 1.2f), 0f, rightCheck.position, 0f, 1 << LayerMask.NameToLayer("Wall"));
        }

        if (grounded)
        {
            notOnGround = false;
        }

        if (walled && !grounded)
        {
            notOnGround = true;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (grounded)
            {
                jump = true;
            }
            else if (walled)
            {
                jumpFromWall = true;
            }
        }
	}

    void FixedUpdate ()
    {
        float h = Input.GetAxis("Horizontal");

        if (walled)
        {
            if (jumpFromWall)
            {
                JumpFromWall();
            }
        }

        if (notOnGround)
        {
            h = 0;
        }

        anim.SetFloat("Speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
        {
            rb2d.AddForce(Vector2.right * h * moveForce);
        }
        if(Mathf.Abs(rb2d.velocity.x) > maxSpeed)
        {
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);
        }

        if (h > 0 && !facingRight)
        {
            Flip();
        }
        else if (h < 0 && facingRight)
        {
            Flip();
        }

        if (jump)
        {
            Jump();
        }
    }

    void Flip ()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void Jump()
    {
        copyOfP[0] = Instantiate(particles[0], new Vector2(transform.position.x, transform.position.y - 1), particles[0].transform.rotation);
        copyOfP[0].Play();
        audioS.clip = audioClips[0];
        audioS.Stop();
        audioS.Play();
        anim.SetTrigger("Jump");
        rb2d.AddForce(new Vector2(0f, jumpForce));
        jump = false;
    }

    void JumpFromWall()
    {
        copyOfP[1] = Instantiate(particles[1], new Vector2(transform.position.x, transform.position.y - 1), particles[1].transform.rotation);
        copyOfP[1].transform.localScale = new Vector3(transform.localScale.x * particles[1].transform.localScale.x, particles[1].transform.localScale.y, particles[1].transform.localScale.z);
        copyOfP[1].Play();
        audioS.clip = audioClips[2];
        audioS.Stop();
        audioS.Play();
        anim.SetTrigger("Jump");
        rb2d.AddForce(new Vector2(-(transform.position.x + (300 * transform.localScale.x)), jumpForce));
        jumpFromWall = false;
        Flip();
        StartCoroutine(WaitToContinue());
    }

    IEnumerator WaitToContinue()
    {
        yield return new WaitForSeconds(0.5f);
        notOnGround = false;
    }

    public void Restart()
    {
        if (!gameController.End())
        {
            audioS.clip = audioClips[1];
            audioS.Stop();
            audioS.Play();
            transform.position = gameController.GetOrigin();
            rb2d.velocity = Vector2.zero;
        }
    }


    void OnDrawGizmos()
    {
        Gizmos.DrawCube(groundCheck.position, new Vector2(1, 0.5f));
        Gizmos.DrawCube(rightCheck.position, new Vector2(0.1f, 1.2f));
    }
}
