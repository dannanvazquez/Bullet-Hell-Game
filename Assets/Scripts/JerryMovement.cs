using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerryMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject walkableArea;
    private Vector3 targetLocation;

    private void Start() {
        targetLocation = transform.position;
    }

    void Update() {
        float widthArea = walkableArea.GetComponent<SpriteRenderer>().bounds.size.x;
        float heightArea = walkableArea.GetComponent<SpriteRenderer>().bounds.size.y;
        if (targetLocation.x - transform.position.x <= 0.1f && targetLocation.y - transform.position.y <= 0.1f) {
            targetLocation.x = Random.Range(walkableArea.transform.position.x - (widthArea / 2), walkableArea.transform.position.x + (widthArea /2));
            targetLocation.y = Random.Range(walkableArea.transform.position.y - (heightArea / 2), walkableArea.transform.position.y + (heightArea / 2));
        }
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
    }
}
