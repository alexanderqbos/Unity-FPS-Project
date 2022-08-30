using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSInput : MonoBehaviour
{
    private float speed = 9.0f;
    private float gravity = -9.8f;

    private float pushForce = 5.0f;

    [SerializeField]
    private CharacterController cc;

    void Update()
    {
        float deltaX = Input.GetAxis("Horizontal");
        float deltaY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(deltaX, 0, deltaY);

        movement = Vector3.ClampMagnitude(movement, 1.0f);

        movement *= speed;

        movement.y = gravity;
        
        movement *= Time.deltaTime;

        movement = transform.TransformDirection(movement);

        cc.Move(movement);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        Rigidbody rb = hit.collider.attachedRigidbody;

        if(rb != null && !rb.isKinematic)
        {
            rb.velocity = hit.moveDirection * pushForce;
        }
    }
}
