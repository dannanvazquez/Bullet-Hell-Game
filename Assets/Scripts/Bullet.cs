using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector2 velocity;
    public float speed;
    public float rotation;

    private void Start() {
        transform.rotation = Quaternion.Euler(0, 0, rotation);
    }

    private void Update() {
        //DestroyBullet();
        transform.Translate(velocity * speed * Time.deltaTime);
    }

    private void DestroyBullet() {
        Vector3 viewPos = Camera.main.WorldToViewportPoint(transform.position);
        if (viewPos.x <= 0 || viewPos.x >= 1 || viewPos.y <= 0 || viewPos.y >= 1) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (this.tag == "PlayerBullet" && collision.tag == "Enemy") {
            Destroy(gameObject);
        }
    }
}
