using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Float : MonoBehaviour
{
    private Vector3 startPos;
    [SerializeField] private float speed = 0.5f;
    [SerializeField] private bool isVertical = true;
    private Rigidbody rb;

    private void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        var theta = Time.time * speed;
        var offset = isVertical ? Mathf.Cos(theta) * Vector3.up : Mathf.Cos(theta) * Vector3.right;

        rb.MovePosition(startPos + offset);
        //transform.position = startPos + offset;
    }
}
