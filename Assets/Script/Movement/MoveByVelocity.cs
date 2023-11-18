using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class MoveByVelocity : MonoBehaviour
{
    Rigidbody2D rb;
    public int facingDir = -1;
    bool isFacingLeft = true;
    public float currentSpeed { get; set; }
    public float permanentSpeed { get; set; }
    [SerializeField] Transform imageTransform; // serializeField image to flip without filp the whole object (include Power Text)
    [SerializeField] GameObject bubblePrefab;

    public void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void Update()
    {
        if (rb.velocity == Vector2.zero)
        {
            bubblePrefab.SetActive(false);
        }
        else bubblePrefab.SetActive(true);
    }
    public void CallMoveByVelocity(Vector2 direction)
    {
        rb.velocity = direction * currentSpeed;
        imageTransform.right = -direction;
        bubblePrefab.transform.right = Quaternion.AngleAxis(90, Vector3.forward) * direction;
        //FlipController(direction);
    }
    public void Flip()
    {
        imageTransform.Rotate(0, 180, 0);
    }
    public void FlipController(Vector2 velocity)
    {
        if (velocity.x < 0 && !isFacingLeft)
        {
            Flip();
            facingDir = -1;
            isFacingLeft = true;
        }
        if (velocity.x > 0 && isFacingLeft)
        {
            Flip();
            facingDir = 1;
            isFacingLeft = false;
        }
    }
    public void IncreaseSpeed(float value)
    {
        currentSpeed += value;
    }
    public void SetSpeed(float value)
    {
        currentSpeed = value;
    }
}
