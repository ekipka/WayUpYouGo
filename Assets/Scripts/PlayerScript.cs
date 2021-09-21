using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public float moveSpeedHorizontal = 10f;
    public float moveSpeedVertical = 200f;
    public bool isGrounded;
    public bool isTouchingWall;

    private BoxCollider2D playerCollision;
    private Rigidbody2D playerRB;

    private float ScreenWidth;
    private float ScreenHeight;

    private float playerVelocityX;
    private float playerVelocityY;

    // Start is called before the first frame update
    void Start()
    {
        ScreenWidth = Screen.width;
        ScreenHeight = Screen.height;
        playerRB = GetComponent<Rigidbody2D>();
        playerCollision = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        //loop over every touch found
        while (i < Input.touchCount)
        {
            
            Debug.Log(ScreenWidth + "; "+ ScreenHeight + "Position (x;y): " + Input.GetTouch(i).position.x+ "; "+ Input.GetTouch(i).position.y);
            if (Input.GetTouch(i).position.x > ScreenWidth / 2)
            {
                //move right
                RunCharacterAddFocrce(moveSpeedHorizontal, 0);
            }
            if (Input.GetTouch(i).position.x < ScreenWidth / 2)
            {
                //move left
                RunCharacterAddFocrce(-moveSpeedHorizontal, 0);
            }
            /*
            if (Input.GetTouch(i).position.y > ScreenHeight / 2)
            {
                //move left
                RunCharacter(0, 1.0f);
            }
            if (Input.GetTouch(i).position.y < ScreenHeight / 2)
            {
                //move left
                RunCharacter(0,-1.0f);
            }
            */
            ++i;
        }
    }

    void FixedUpdate()
    {
#if UNITY_EDITOR
        //stop player from rotating 
        if(playerRB.transform.rotation.z !=0)
        {
            Quaternion target = Quaternion.Euler(0, 0, 0);
            playerRB.transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * 100.0f);
        }
        if (!isTouchingWall && (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0))
        {
            Debug.Log(playerRB.velocity.y + Input.GetAxis("Vertical"));
            RunCharacterAddFocrce(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

#endif
    }

    private void RunCharacterAddFocrce(float horizontalInput, float verticalInput)
    {
        
        //move player
        if (isGrounded && verticalInput > 0)
        {
            playerRB.AddForce(new Vector2(horizontalInput * moveSpeedHorizontal, 0.5f * moveSpeedVertical));
        }
        else if(!isTouchingWall)
        {
            playerRB.AddForce(new Vector2(horizontalInput * moveSpeedHorizontal, playerRB.velocity.y));
        }
    }

    private void BounceCharacter(float horizontalInput, float verticalInput)
    {
        //bounce player off the wall
        if (isGrounded)
        {
            playerRB.velocity = new Vector2(horizontalInput, 0);
        }
        else
        {
            playerRB.velocity = new Vector2(horizontalInput, horizontalInput/3 * verticalInput );
        }
    }


    
    void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.layer == 6 //platforms, ground layer
            && !isGrounded && playerRB.velocity.y == 0)
        {
            
            isGrounded = true;
        }
        if (collision.gameObject.layer == 7)//right wall
        {
            isTouchingWall = true;
            BounceCharacter(playerVelocityX * (-1), playerVelocityY * (-1));

            
        }
        if (collision.gameObject.layer == 8)//left wall
        {
            BounceCharacter(playerVelocityX * (-1), playerVelocityY);
            isTouchingWall = true;
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        //isGrounded = collision.gameObject.layer == 6 && isGrounded ? false : true;

        if (collision.gameObject.layer == 6
            && isGrounded)
        {
            isGrounded = false;
        }
        if (collision.gameObject.layer == 7)
        {
            isTouchingWall = false;
        }
        if (collision.gameObject.layer == 8)
        {
            isTouchingWall = false;
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        playerVelocityX = playerRB.velocity.x;
        playerVelocityY = playerRB.velocity.y;
    }
}
