using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement_L5_V3 : MonoBehaviour
{

    public float moveSpeed = 1.0f;
    private Vector3 originalPosition;
    private Vector3 camRight, camUp; 

    private Vector2 mouseDelta;

    public float maxDistance = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        originalPosition = transform.position;

        Camera mainCamera = Camera.main;

        camRight = mainCamera.transform.right;
        camUp = mainCamera.transform.up; 
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 deltaVec2 = getMouseDelta();

        Vector3 movementVec2 = deltaVec2.x * camRight + deltaVec2.y * camUp;
        Vector3 newPosition = transform.position + movementVec2;
        Vector3 offset = newPosition - originalPosition;
        newPosition = originalPosition + Vector3.ClampMagnitude(offset, maxDistance);

        transform.position = newPosition;
    }

    Vector2 getMouseDelta()
    {
        float deltaX = Input.GetAxis("Mouse X");
        float deltaY = Input.GetAxis("Mouse Y");

        mouseDelta = new Vector2(deltaX, deltaY);

        // You can use mouseDelta.x and mouseDelta.y for your calculations here

        return mouseDelta * moveSpeed;
    }
}
