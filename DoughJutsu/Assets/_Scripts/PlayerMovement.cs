using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private GameManager gm;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator anim;
    private SpriteRenderer sr;
    public float moveSpeed = 7f;
    public float jumpForce = 7f;
    [SerializeField] private LayerMask jumpableGround;
    private float dirX;
    private enum AirState { land, jump, fall }
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
        gm = GameManager.instance;
    }

    // Update is called once per frame
    private void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if (Input.GetButtonDown("Jump") && isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationUpdate();
    }

    private void UpdateAnimationUpdate()
    {

        if (dirX > 0)
        {
            anim.SetBool("Run", true);
            sr.flipX = false;
        }
        else if (dirX < 0)
        {
            anim.SetBool("Run", true);
            sr.flipX = true;
        }
        else
        {
            anim.SetBool("Run", false);
        }

        if (rb.velocity.y > .1f)
        {
            anim.SetInteger("Airstate", 1);
        }
        else if (rb.velocity.y < -.1f)
        {
            anim.SetInteger("Airstate", 2);
        }
        else
        {
            anim.SetInteger("Airstate", 0);
        }


    }

    private bool isGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, -Vector2.down, -.1f, jumpableGround);
    }

    private void OnCollisionEnter2D(Collision other)
    {
        if (coll.gameObject.CompareTag("trap"))
        {
            gm.displayLose.SetActive(true);
            Time.timeScale = 0f;
            Invoke("PlayerLoses", 2f);
        }
    }

    private void PlayerLoses()
    {
        SceneManager.LoadScene("ModeMenu");
        Time.timeScale = 1f;
    }
}
