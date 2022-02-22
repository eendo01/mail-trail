using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Vector2 direction;
    private AudioSource pickedupSound;

    //private float curSpriteDirection;
    [SerializeField] private float speed;
    //public SpriteRenderer spriteRenderer;
    //public Sprite[] spriteArray; // [left, right]

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponentInChildren<Rigidbody2D>();
        //curSpriteDirection = 1; // direction set to right
        //spriteRenderer.sprite = spriteArray[1];
    }

    // Update is called once per frame
    void Update()
    {
        float dirX;
        float dirY;

        dirX = Input.GetAxis("Horizontal");
        dirY = Input.GetAxis("Vertical");

        direction.Set(dirX, dirY);

        direction.Normalize();
        //if (dirX < 0 && curSpriteDirection == 1)
        //{

        //}
    }

    private void FixedUpdate()
    {
        rigidBody.velocity = direction * speed;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Collectable"))
        {    
            GameController.instance.CollectBox();
            Destroy(collision.gameObject);
        }
        pickedupSound.Play();
    }
}
