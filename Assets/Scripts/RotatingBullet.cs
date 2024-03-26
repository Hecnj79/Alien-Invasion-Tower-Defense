using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBullet : MonoBehaviour
{
    [SerializeField] private int bulletDamage = 1;
    public float rotationSpeed = 200f;

    void Update()
    {
        transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), rotationSpeed * Time.deltaTime);
    }

    public void SetRotationSpeed(float newSpeed)
    {
        rotationSpeed += newSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            collision.gameObject.GetComponent<Health>().TakeDamage(bulletDamage);
        }
    }
}
