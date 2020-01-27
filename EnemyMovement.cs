using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float latestDirectionChangeTime;
    public readonly float directionChangeTime = 1f;
    public float characterVelocity = 4f;
    public Vector2 movementDirection;
    public Vector2 movementPerSecond;
    public Rigidbody2D rb;
    public Vector2 originalPos;
    public PlayerMovement playerScript;
    public GameObject Player;
    public int MinDistance, MaxDistance;
    public bool EnemyActive;
    public Collider2D m_Collider;




    void Start()
    {
        EnemyActive = true;
        rb = GetComponent<Rigidbody2D>();
        m_Collider = GetComponent<Collider2D>();
        latestDirectionChangeTime = 0f;
        MinDistance = 5;
        MaxDistance = 10;
        calcuateNewMovementVector();
        CheckDirection();
        originalPos = rb.transform.position;
        
    }

    void calcuateNewMovementVector()
    {
        
        //create a random direction vector with the magnitude of 1, later multiply it with the velocity of the enemy
        movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)).normalized;
        movementPerSecond = movementDirection * characterVelocity;
    }

    void Update()
    {
        CheckDirection();
        //if the changeTime was reached, calculate a new movement vector
        if (Time.time - latestDirectionChangeTime > directionChangeTime)
        {
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();
        }

        //move enemy: 
        if (EnemyActive == true)
        {
            if (Vector2.Distance(rb.transform.position, Player.transform.position) >= MinDistance)
            {
                rb.transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime),
                rb.transform.position.y + (movementPerSecond.y * Time.deltaTime));
            }
            if (Vector2.Distance(rb.transform.position, Player.transform.position) <= MinDistance)
            {
                rb.transform.position = Vector2.MoveTowards(rb.transform.position, Player.transform.position, characterVelocity* Time.deltaTime);
            }
        }
        if (EnemyActive == false)
        {           
            rb.transform.position = Vector2.MoveTowards(rb.transform.position, originalPos, 7*Time.deltaTime);
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

        if ((collision.collider.tag == "Player"))
        {
            m_Collider.enabled = !m_Collider.enabled;
            EnemyActive = false;
            Invoke("IsActive", 5);
        }

    }
    private void IsActive()
    {
        EnemyActive = true;
        m_Collider.enabled = !m_Collider.enabled;
    }

    void CheckDirection()
    {
        if (movementDirection.y > 0)
        {

            transform.localScale = new Vector3(1, 1, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (movementDirection.x > 0)
        {

            transform.localScale = new Vector3(1, 1, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 270);

        }

        if (movementDirection.y < 0)
        {

            transform.localScale = new Vector3(1, -1, 0);
            rb.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (movementDirection.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 0);
            transform.localRotation = Quaternion.Euler(0, 0, 90);
        }
    }

}