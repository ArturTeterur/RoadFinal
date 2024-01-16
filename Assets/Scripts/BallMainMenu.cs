using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMainMenu : MonoBehaviour
{
    public float jumpForce = 5f; // сила прыжка
    public float jumpInterval = 1f; // интервал между прыжками

    private float nextJumpTime;

    void Update()
    {
        if (Time.time > nextJumpTime)
        {
            Jump();
            nextJumpTime = Time.time + jumpInterval;
        }
    }

    void Jump()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

}
