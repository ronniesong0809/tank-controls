using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankControls : MonoBehaviour
{
    public float moveSpeed = 15f;
    public bool _isGrounded = true;
    Vector3 _inputVector;
    public Rigidbody rb;

    public float sprintSpeedMultiplier = 1.6f;
    public float jumpForce = 35f;
    //public Transform groundCheckTransform;

    // Update is called once per frame
    void Update()
    {
        _inputVector = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));

        // running
        if (Input.GetKey(KeyCode.LeftShift) && _isGrounded)
        {
            _inputVector.z *= sprintSpeedMultiplier;
        }

        //jumping
        if (Input.GetKeyDown(KeyCode.Space) && _isGrounded)
        {
            rb.AddForce(jumpForce * 10 * transform.up, ForceMode.Acceleration);
        }
    }

    void FixedUpdate()
    {
        Vector3 movement = moveSpeed * 10f * _inputVector.z * Time.fixedDeltaTime * transform.forward + moveSpeed * 10f * _inputVector.x * Time.fixedDeltaTime * transform.right;
        rb.MovePosition(transform.position + movement * Time.fixedDeltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Entered");
        if (collision.gameObject.CompareTag("Terrain"))
        {
            _isGrounded = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Exited");
        if (collision.gameObject.CompareTag("Terrain"))
        {
            _isGrounded = false;
        }
    }
}
