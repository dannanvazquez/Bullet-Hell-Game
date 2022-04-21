using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerPatternSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    public float rotateSpeed;
    private bool isRotating = false;

    private void Start() {
        StartCoroutine(SpawnPetals());
    }

    private void Update() {
        if (isRotating) {
            transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        }
    }

    IEnumerator SpawnPetals() {
        for (float i = 0; i < 6; i++) {
            for(int j = 0; j < 4; j++) {
                var lb = Instantiate(bullet, new Vector3((i*2f) + transform.position.x, 3 * Mathf.Sin((i + .5f) / 1.75f) + transform.position.y, 0), Quaternion.identity, transform);
                var lbb = lb.GetComponent<Bullet>();
                lbb.speed = 0;
                var rb = Instantiate(bullet, new Vector3((i*2f) + transform.position.x, 3 * Mathf.Sin((-i - .5f) / 1.75f) + transform.position.y, 0), Quaternion.identity, transform);
                var rbb = rb.GetComponent<Bullet>();
                rbb.speed = 0;
                transform.Rotate(0, 0, 90);
            }
            yield return new WaitForSeconds(0.5f);
        }
        isRotating = true;
        yield return null;
    }
}
