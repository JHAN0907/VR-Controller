using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    public float gravityScale = 1.0f;
    public static float globalGravity = -9.81f;

    Rigidbody rigidbody;

    private void OnEnable()
    {
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.useGravity = false;
    }

    private void FixedUpdate()
    {
        Vector3 gravity = globalGravity * gravityScale * Vector3.up;
        rigidbody.AddForce(gravity, ForceMode.Acceleration);
    }
}
