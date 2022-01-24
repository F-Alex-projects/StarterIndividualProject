using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;

    public float jumpForce;

    public Text scoreText;

    private int scoreValue = 0;

    private bool facingRight = true;

    private bool isOnGround;

    public bool gameOver;
    public Transform groundcheck;

    public float checkRadius;

    public LayerMask allGround;

    public Text gameOverText;

    public AudioClip musicClipOne;
    public AudioClip musicClipTwo;
    public AudioClip bgm;
    public AudioClip collectedClip;
    public AudioSource musicSource;

    public GameObject gain;

    Animator anim;

    private float startTime;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rd2d = GetComponent<Rigidbody2D>();
        setScoreText();
        gameOverText.text = "";
        musicSource.clip = bgm;
        musicSource.Play();
        musicSource.loop = true;
        gameOver = false;
        startTime = Time.time;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float t = Time.time - startTime;
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));

        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }

        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (t >= 12 && gameOver == false)
            setLoseText();
        if  (gameOver == true)
            GameOver();
        if (Input.GetKey(KeyCode.R))
        {
            if (gameOver == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            Particle();
            scoreValue += 1;
            setScoreText();
            setWinText();
            PlaySound(collectedClip);
            Destroy(collision.collider.gameObject);

        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
        }
    }
    
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }
    
    void setScoreText()
    {
        scoreText.text = "Score: " + scoreValue.ToString();
    }
    void setLoseText()
    {
            musicSource.Stop();
            gameOverText.text = "You Lose! Try again by pressing R";
            musicSource.clip = musicClipOne;
            musicSource.Play();
            musicSource.loop = false;
            gameOver = true;
    }

    void setWinText()
    {
            if (scoreValue >= 5)
        {
            musicSource.Stop();
            gameOverText.text = "You Win! Game by Alejandro Franquez.";
            musicSource.clip = musicClipTwo;
            musicSource.Play();
            musicSource.loop = false;
            gameOver = true;
        }

    }
    void PlaySound(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }
    void GameOver()
    {
           speed = 0;
           jumpForce = 0;     
    }
    void Particle()
    {
        GameObject gainObject = Instantiate(gain, rd2d.position + Vector2.up * 0.5f, Quaternion.identity);
    }
}