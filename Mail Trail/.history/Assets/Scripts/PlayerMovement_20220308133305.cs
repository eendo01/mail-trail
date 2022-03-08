using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]

public class PlayerMovement : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rigidBody;
    //private bool FaceLeft = true;
    private Vector3 direction;
    public bool isActive = true;
    private AudioSource pickedupSound;
    public bool popup = true;
    [SerializeField] public float speed;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator>();
        rigidBody = GetComponentInChildren<Rigidbody2D>();
        pickedupSound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float dirX;
        float dirY;

        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");

        direction.Set(dirX, dirY, 0.0f);

        if (GameController.instance.gamePlaying)
        {
            transform.position = transform.position + direction * speed * Time.deltaTime;
        }
        if (dirX < 0)
        {
            animator.SetBool("FaceLeft", true);
        }
        if (dirX > 0)
        {
            animator.SetBool("FaceLeft", false);
        }
        //if ((dirX < 0 && !FaceLeft) || dirX > 0 && FaceLeft)
        //{
        //    playerTurn();
        //}
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = direction * speed;

    }

    //private void playerTurn()
    //{
    //    // switch player facing label
    //    FaceLeft = !FaceLeft;


    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Collectable"))
        {
            pickedupSound.Play();
            GameController.instance.CollectBox();
            Destroy(collision.gameObject);
        }
        if(collision.tag == "water"){
            popup = true;
        }

    }
}
