using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBody : MonoBehaviour
{
    [Header("Movement")]
    public float speed;

    private InputSystem inputSystem;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        inputSystem = GetComponent<InputSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        Vector2 input = inputSystem.GetWASDInput();

        Vector3 goalDirection = new Vector3(input.x,0f,input.y);

        Vector3 goalVelocity = goalDirection * speed;

        Vector3 force = (goalVelocity - rb.velocity) / (.1f + Time.deltaTime);

        rb.AddForce(Vector3.Scale(force, new Vector3(1f, 0f, 1f)), ForceMode.Force);
    }


}
