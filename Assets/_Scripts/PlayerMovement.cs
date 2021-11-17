using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    public float Speed = 10;
    [HideInInspector] public Vector3 DirectionVelocity = new Vector3();

    [Header("Assignments")]
    public Rigidbody Rigidbody;

    // Update is called once per frame
    void Update()
    {
        GetDirection();
    }

    private void FixedUpdate()
    {
        ApplyMovement(DirectionVelocity, Speed);
    }

    public void GetDirection()
    {
        DirectionVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    }

    public void ApplyMovement(Vector3 direction, float speed)
    {
        Rigidbody.velocity = (direction) * speed * Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, 15);
    }
}
