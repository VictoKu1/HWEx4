using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float timeToDisapear; 
    GameObject player;
    Animator anim;
    bool isAlive = true;
    //*Called before the first frame update, determines the target which is the player and its walking and destruction animation .
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponentInChildren<Animator>();
    }

    //*Giving players coordinates to the enemy so he will follow him.
    void Update()
    {
        if (player != null && isAlive)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position,speed*Time.deltaTime);
        }
    }
    //*When players bullet touches the enemy it changes its animation and sets its off.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            anim.SetTrigger("Boom1");
            isAlive = false;
            Destroy(gameObject, timeToDisapear);
        }
    }

}
