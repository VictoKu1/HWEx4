using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BasicMovement : MonoBehaviour
{
    public Animator animator;

    public GameOverScreen GameOverScreen;
    public static bool gameIsPaused;
    public float moveSpeed;
    public Rigidbody2D rb;
    private Vector3 moveDirection;
    [SerializeField] Slider HP;
    [SerializeField] int damageFromJelly;
    void Start()
    {
        HP.value = 100;
    }
    public void GameOver()
    {
        PauseGame();
        GameOverScreen.Setup();
    }
    //* Updates every frame.
    void Update()
    {
        ProcessInputs();
    }
    //* Updates every fixed amount of frames.
    void FixedUpdate()
    {
        Move();
    }
    //* Calculating the changes for the position and swaps the image of the players image by X axis respectivly.
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector3(moveX, moveY,0.0f);
        animator.SetFloat("Horizontal", moveDirection.x);
        animator.SetFloat("Vertical", moveDirection.y);
        animator.SetFloat("Magnitude", moveDirection.magnitude);
        //* (.normalized) was added so the movement in 2 axis simultaneously wont double the speed . 
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
        
    }
    void PauseGame()
    {
        if (gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
    //*Calculates the velocity of the the player using its position and Unity added speed.
    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            if (HP.value <= damageFromJelly)
            {
                GameOver();
            }
            HP.value -= damageFromJelly;
        }
    }
}
