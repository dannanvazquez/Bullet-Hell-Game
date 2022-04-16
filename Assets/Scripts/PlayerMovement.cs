using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject walkableArea;

    void Update()
    {
        transform.position += new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.deltaTime;
        float widthArea = walkableArea.GetComponent<SpriteRenderer>().bounds.size.x;
        float heightArea = walkableArea.GetComponent<SpriteRenderer>().bounds.size.y;
        if (transform.position.x < walkableArea.transform.position.x - (widthArea / 2)) {
            transform.position = new Vector3(walkableArea.transform.position.x - (widthArea / 2), transform.position.y, transform.position.z);
        } else if (transform.position.x > walkableArea.transform.position.x + (widthArea / 2)) {
            transform.position = new Vector3(walkableArea.transform.position.x + (widthArea / 2), transform.position.y, transform.position.z);
        }
        if (transform.position.y > walkableArea.transform.position.y + (heightArea / 2)) {
            transform.position = new Vector3(transform.position.x, walkableArea.transform.position.y + (heightArea / 2), transform.position.z);
        } else if (transform.position.y < walkableArea.transform.position.y - (heightArea / 2)) {
            transform.position = new Vector3(transform.position.x, walkableArea.transform.position.y - (heightArea / 2), transform.position.z);
        }
    }
}
