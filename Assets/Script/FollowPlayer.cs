using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform player; // player position
    public Vector3 startingCameraOffset; // starting camera offset
    public Vector3 zoomOutCameraOffset; // out of bound camera offset
    public float zoomOutHeight = 6f; // height at which the camera zooms out
    public float zoomOutSpeed = 5f; // camera zoom out speed
    public float zoomInSpeed = 10f; // camera zoom in speed

    // Update is called once per frame
    void LateUpdate()
    {
        if (player.transform.position.y > zoomOutHeight)
        {
            Vector3 zoomOutPosition = player.transform.position + startingCameraOffset + zoomOutCameraOffset;
            transform.position = Vector3.Lerp(transform.position, zoomOutPosition, zoomOutSpeed * Time.deltaTime);
        }
        else if (player.transform.position.y < zoomOutHeight)
        {
            Vector3 zoomInPosition = new Vector3(player.transform.position.x + startingCameraOffset.x, startingCameraOffset.y, startingCameraOffset.z);
            transform.position = Vector3.Lerp(transform.position, zoomInPosition, zoomInSpeed * Time.deltaTime);
        }
    }
}