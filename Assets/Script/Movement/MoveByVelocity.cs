using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class MoveByVelocity : MonoBehaviour
{
    Rigidbody2D rb;
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
        if (direction.x > 0) imageTransform.localScale = new Vector3(1, -1, 1); // neu di chuyen qua ben phai thi flip sprite lai
        else imageTransform.localScale = new Vector3(1, 1, 1); // neu ben trai di reset ve ban dau
        imageTransform.right = -direction;
        bubblePrefab.transform.right = Quaternion.AngleAxis(90, Vector3.forward) * direction;
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
