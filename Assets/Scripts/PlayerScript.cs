using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    

    public float moveSpeedHorizontal = 1f;
    public float moveSpeedVertical = 1f;
    private BoxCollider2D playerCollision;

    private Rigidbody2D playerRB;
    private float ScreenWidth;
    private float ScreenHeight;

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
                RunCharacter(moveSpeedHorizontal, 0);
            }
            if (Input.GetTouch(i).position.x < ScreenWidth / 2)
            {
                //move left
                RunCharacter(-moveSpeedHorizontal, 0);
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
        Debug.Log(playerCollision.IsTouchingLayers(7));
        if(playerCollision.IsTouchingLayers(7)==true)
        RunCharacter(Input.GetAxis("Horizontal")* moveSpeedHorizontal * Time.deltaTime, Input.GetAxis("Vertical")* moveSpeedVertical * Time.deltaTime);
#endif
    }

    private void RunCharacter(float horizontalInput, float verticalInput)
    {
        //move player
        //playerRB.AddForce(new Vector2(horizontalInput * Time.deltaTime, verticalInput * Time.deltaTime));//* moveSpeed * Time.deltaTime
        playerRB.velocity = new Vector2(horizontalInput * Time.deltaTime, verticalInput * Time.deltaTime);
    }
}
