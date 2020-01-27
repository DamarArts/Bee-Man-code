using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Vector2 dest = Vector2.zero;
    public float Speed = 4.0f;
    private Rigidbody2D rb;
    public bool MovingUp, MovingDown, MovingRight, MovingLeft, PoweredUp, Moving;
    public Transform Portal1, Portal2, Portal3, Portal4;
    public int Flowers, Score, Lives;
    public Text FlowerCount, WinText, LoseText, ScoreText, LivesText;
    public EnemyMovement enemyScript;
    private SpriteRenderer m_SpriteRenderer;
    private Color m_NewColor;
    private float a, b;
    float m_Red, m_Blue, m_Green;
    private Vector2 originalPosition;

    private string sceneName;



    public AudioSource BeeSound;
    public AudioSource BoosterSound;
    public AudioSource resetSound;
    public AudioSource PickUpSound;
    public AudioSource WaspKillSound;


    void Start()
    {
        
        WinText.text = "";
        LoseText.text = "";
        a = 1.0f;
        b = 1.0f;
        Flowers = 0;
        Score = 0;
        Lives = 3;
        MovingUp = false;
        MovingDown = false;
        MovingRight = false;
        MovingLeft = false;
        rb = GetComponent<Rigidbody2D>();
        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        m_SpriteRenderer.color = Color.white;
        Moving = false;
        originalPosition = rb.transform.position;

        Scene currentScene = SceneManager.GetActiveScene();

        sceneName = currentScene.name;

    }

    void Update()
    {
        CheckInput();
        Move();
        if (Moving == true)
        {
            BeeSound.Play();
        }
        if (sceneName == "firstlevel")
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("mainmenu2");
            }
        }
        if (sceneName == "secondlevel")
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("mainmenu3");
            }
        }
        if ((Lives <= 0 ) && (sceneName == "firstlevel"))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("mainmenu2");
            }
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("firstlevel");
            }
        }
        if ((Flowers == 108) && (sceneName == "firstlevel"))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("mainmenu2");
            }
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("secondlevel");
            }
        }

        if ((Lives <= 0) && (sceneName == "secondlevel"))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("mainmenu3");
            }
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("menu3");
            }
        }
        if ((Flowers == 108) && (sceneName == "secondlevel"))
        {
            if (Input.GetKey(KeyCode.Escape))
            {
                SceneManager.LoadScene("mainmenu3");
            }
            if (Input.GetKey(KeyCode.Return))
            {
                SceneManager.LoadScene("menu3");
            }
        }
    }

    void CheckInput()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            MovingUp = true;
            MovingDown = false;
            MovingRight = false;
            MovingLeft = false;
            transform.localScale = new Vector3(a, b, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }


        if (Input.GetKey(KeyCode.RightArrow))
        {
            MovingRight = true;
            MovingUp = false;
            MovingDown = false;
            MovingLeft = false;
            transform.localScale = new Vector3(a, b, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);

        }



        if (Input.GetKey(KeyCode.DownArrow))
        {
            MovingDown = true;
            MovingUp = false;
            MovingRight = false;
            MovingLeft = false;
            transform.localScale = new Vector3(a, b, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 270);
        }



        if (Input.GetKey(KeyCode.LeftArrow))
        {
            MovingLeft = true;
            MovingUp = false;
            MovingDown = false;
            MovingRight = false;
            transform.localScale = new Vector3(-a, b, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Portal1")
        {
            rb.transform.position = new Vector3(9.5f, 3.5f, 0);
        }
        if (collision.collider.tag == "Portal2")
        {
            rb.transform.position = new Vector3(-6.5f, 3.5f, 0);
        }
        if (collision.collider.tag == "Portal3")
        {
            rb.transform.position = new Vector3(1.5f, 11.5f, 0);
        }
        if (collision.collider.tag == "Portal4")
        {
            rb.transform.position = new Vector3(1.5f, -4.5f, 0);
        }

        if ((collision.collider.tag == "Enemy") && PoweredUp == false)
        {

            rb.transform.position = originalPosition;
            resetSound.Play();
            Lives = Lives - 1;
            Score = Score - 3;
            SetAllText();
        }
        if ((collision.collider.tag == "Enemy") && PoweredUp == true)
        {
            resetSound.Play();
            Score = Score + 200;
            SetAllText();
        }
    }
    private void Move()
    {
        if (MovingUp == true)
            rb.transform.position = rb.transform.position + Vector3.up * Speed * Time.deltaTime;
        Moving = true;

        if (MovingRight == true)
            rb.transform.position = rb.transform.position + Vector3.right * Speed * Time.deltaTime;
        Moving = true;

        if (MovingDown == true)

            rb.transform.position = rb.transform.position + Vector3.down * Speed * Time.deltaTime;
        Moving = true;
        if (MovingLeft == true)

            rb.transform.position = rb.transform.position + Vector3.left * Speed * Time.deltaTime;
        Moving = true;


    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.gameObject.CompareTag("Flower")))
        {
            other.gameObject.SetActive(false);
            Flowers = Flowers + 1;
            Score = Score + 3;
            SetAllText();
            PickUpSound.Play();

        }
        if ((other.gameObject.CompareTag("BoosterFlower")))
        {
            other.gameObject.SetActive(false);
            transform.localScale = new Vector3(1.5f, 1.5f, 0);
            PoweredUp = true;
            a = 1.2f;
            b = 1.2f;
            m_SpriteRenderer.color = Color.red;
            BoosterSound.Play();
            Invoke("SetPowerUp", 5);
        }

    }
    private void SetPowerUp()
    {
        a = 1.0f;
        b = 1.0f;
        m_SpriteRenderer.color = Color.white;
        PoweredUp = false;
    }
    public void SetAllText()
    {
        FlowerCount.text = "" + Flowers.ToString();
        ScoreText.text = "" + Score.ToString();
        LivesText.text = "" + Lives.ToString();

            if (Flowers == 108)
            {
                FlowerCount.text = "";
                ScoreText.text = "";
                LivesText.text = "";
                WinText.text = "LEVEL COMPLETE PRESS ENTER TO COUNTINUE";

            }
            else if (Lives <= 0)
            {
                FlowerCount.text = "";
                ScoreText.text = "";
                LivesText.text = "";
                LoseText.text = "YOU LOST PRESS ENTER TO COUNTINUE ";

            }
    }
}



