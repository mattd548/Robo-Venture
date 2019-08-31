using UnityEngine;
using System.Collections;

public class missle : MonoBehaviour
{

    public Transform target;

    public float speed = 5f;
    public float rotateSpeed = 200f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector2 direction = (Vector2)target.position - rb.position;

        direction.Normalize();

        float rotateAmount = Vector3.Cross(direction, transform.up).z;

        rb.angularVelocity = -rotateAmount * rotateSpeed;

        rb.velocity = transform.up * speed;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "limitArea")
        {
            //do something ex: sound effect, visual effect etc
            Destroy(gameObject);
        }
    }

}