using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Rigidbody2D target;
    public Transform cameraMask;
    public float cameraFollowSpeed;
    public float cameraContinuosSpeed;
    public Vector3 offset;  // camera distance from the player

    private bool startMovingCamera;

    void Start()
    {
        startMovingCamera = false;
    }

    private void FixedUpdate()
    {
        float targetVelocity = target.velocity.y;
        
        
        Vector3 oldCameraPos = transform.position;
        if(target.transform.position.y > cameraMask.position.y)
        {
            startMovingCamera = true;
            Vector3 newCameraPos = new Vector3(oldCameraPos.x,
            oldCameraPos.y + targetVelocity * Time.deltaTime, oldCameraPos.z);

            transform.position = Vector3.Lerp(oldCameraPos, newCameraPos, targetVelocity);
        }
        else if(startMovingCamera == true)
        {
            
            Vector3 newCameraPos = new Vector3(oldCameraPos.x,
            oldCameraPos.y + cameraContinuosSpeed * Time.deltaTime, oldCameraPos.z);

            transform.position = Vector3.Lerp(oldCameraPos, newCameraPos, cameraContinuosSpeed);
        }
        
    }
}
