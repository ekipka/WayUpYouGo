using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    public Transform target;
    public float cameraFollowSpeed;
    public float cameraContinuosSpeed;
    public Vector3 offset;  // camera distance from the player

    private void LateUpdate()
    {
        Vector3 oldCameraPos = transform.position;
        Vector3 newCameraPos = new Vector3(oldCameraPos.x,
            oldCameraPos.y + cameraContinuosSpeed * Time.deltaTime, oldCameraPos.z);
        
        transform.position = Vector3.Lerp(oldCameraPos, newCameraPos, cameraContinuosSpeed);
    }
}
