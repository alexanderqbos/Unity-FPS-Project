using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;

    private float horizontal;
    private float vertical;
    private const float force = 100.0f;

    private Rigidbody rigid;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate() {
        Vector3 movement = new Vector3(horizontal, 0, vertical) * speed * force * Time.deltaTime;

        rigid.velocity = movement;
    }
}
