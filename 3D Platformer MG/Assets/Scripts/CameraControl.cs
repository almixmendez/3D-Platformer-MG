using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    Vector2 mouseMira;
    Vector2 smoothMovement;

    public float sensibility = 5.0f;
    public float smoothness = 2.0f;

    GameObject Player;

    private void Start()
    {
        Player = this.transform.parent.gameObject;
        if (Player == null)
        {
            Debug.LogError("Player reference is null. Make sure the camera is a child of the player GameObject.");
        }
    }

    private void Update()
    {
        var mouseDirection = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        mouseDirection = Vector2.Scale(mouseDirection, new Vector2(sensibility * smoothness, sensibility * smoothness));

        smoothMovement.x = Mathf.Lerp(smoothMovement.x, mouseDirection.x, 1f / smoothness);
        smoothMovement.y = Mathf.Lerp(smoothMovement.y, mouseDirection.y, 1f / smoothness);

        mouseMira += smoothMovement;
        mouseMira.y = Mathf.Clamp(mouseMira.y, -90f, 90);
        transform.localRotation = Quaternion.AngleAxis(-mouseMira.y, Vector3.right);
        Player.transform.localRotation = Quaternion.AngleAxis(mouseMira.x, Player.transform.up);
    }
}
