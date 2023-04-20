using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PersonajeController : MonoBehaviour
{
    //public FootController footController;
    Rigidbody2D rb;
    Animator animator;
    private int currentAnimation = 1;
    SpriteRenderer sr;
    public Cabeza1Controller controller;
    public DisparoController disparoController;
    //bala
    public Transform firePoint;
    public GameObject bullet;

    public GameObject Disparo;
    public float velocityX = 0.1f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        currentAnimation = 1;
        var velocityY = rb.velocity.y;
        rb.velocity = new Vector2(0, velocityY);

        //derecha
        if (Input.GetKey(KeyCode.RightArrow))
        {
            currentAnimation = 2;
            rb.velocity = new Vector2(13, velocityY);
            sr.flipX = false;
        }
        //izquierda
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            currentAnimation = 2;
            rb.velocity = new Vector2(-13, velocityY);
            sr.flipX = true;

        }
        //al saltar el zombie corre mas
        if (Input.GetKey(KeyCode.RightArrow) && controller.zombieJump())
        {
            currentAnimation = 2;
            rb.velocity = new Vector2(30, velocityY);
            sr.flipX = false;
        }
        //correr
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.X))
        {
            currentAnimation = 2;
            rb.velocity = new Vector2(20, velocityY);
            sr.flipX = false;

        }
        //al saltar el zombie corre mas
        if (Input.GetKey(KeyCode.LeftArrow) && controller.zombieJump())
        {
            currentAnimation = 2;
            rb.velocity = new Vector2(-30, velocityY);
            sr.flipX = true;
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.X))
        {
            currentAnimation = 2;
            rb.velocity = new Vector2(-20, velocityY);
            sr.flipX = true;

        }
        //deslizar
        if (Input.GetKey(KeyCode.D))
        {
            bool verificar = sr.flipX;

            if (verificar == true && Input.GetKey(KeyCode.D))
            {
                currentAnimation = 4;
                rb.velocity = new Vector2(-5, velocityY);
            }
            if (verificar == false && Input.GetKey(KeyCode.D))
            {
                currentAnimation = 4;
                rb.velocity = new Vector2(5, velocityY);
            }
        }
        //ataque
        if (Input.GetKey(KeyCode.B))
        {
            currentAnimation = 5;
            rb.velocity = new Vector2(0, velocityY);

        }
        //TIRAR
        if (Input.GetKey(KeyCode.T))
        {
            currentAnimation = 3;
            rb.velocity = new Vector2(0, velocityY);
        }
        //muerte
        if (Input.GetKey(KeyCode.M))
        {
            currentAnimation = 6;
            rb.velocity = new Vector2(0, velocityY);
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            // Dispara hacia la izquierda
            var currentPosition = transform.position;
            var position = new Vector3(currentPosition.x - 5, currentPosition.y, 10);
            var balaGO = Instantiate(bullet, position, Quaternion.identity);
            var controller = balaGO.GetComponent<DisparoController>();
            controller.velocity1 = -10f;
            sr.flipX = true;
        }
        else if (Input.GetKeyUp(KeyCode.P))
        {
            // Dispara hacia la derecha
            var currentPosition = transform.position;
            var position = new Vector3(currentPosition.x + 5, currentPosition.y, 10);
            var balaGO = Instantiate(bullet, position, Quaternion.identity);
            var controller = balaGO.GetComponent<DisparoController>();
            controller.velocity1 = 10f;
            sr.flipX = false;
        }
        animator.SetInteger("Estado", currentAnimation);
    }
}
