using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour {
    [SerializeField]
    private GameObject _SimpleBulletPrefab;

    private float _shootSpeed = 0.2f;
    private int _health = 13;

    private Vector3 borders;

    void Start() {
        //GetComponent<BoxCollider2D>().size;
        borders = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.1f));

        StartCoroutine(ShootRoutine());
    }

    public void Damage(int damage) {
        //_health -= damage;

        Debug.Log("Got damage: " + damage + " Current health: " + _health);

        if (_health <= 0) {
            Destroy(gameObject);
        }
    }

    IEnumerator ShootRoutine() {
        while (true) {
            Instantiate(_SimpleBulletPrefab, transform.position + new Vector3(0.044f, 1.2f), Quaternion.identity);
            yield return new WaitForSeconds(_shootSpeed);
        }
    }
    

    // Player controll

    // private Vector3 screenPoint;
    private Vector3 offset;

    void OnMouseDown() {
        //screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1 * (Camera.main.transform.position.z)));
    }

    void OnMouseDrag() {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1 * (Camera.main.transform.position.z));
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;

        if (isTouchingBorders(0, cursorPosition.y) == false) {
            transform.position = new Vector3(transform.position.x, cursorPosition.y, transform.position.z);
        }

        if (isTouchingBorders(cursorPosition.x, 0) == false) {
            transform.position = new Vector3(cursorPosition.x, transform.position.y, transform.position.z);
        }
    }

    bool isTouchingBorders(float x, float y) {
        if (x < borders.x || x > -borders.x) {
            return true;
        } else if (y < borders.y || y > -borders.y) {
            return true;
        } else {
            return false;
        }
    }

    IEnumerator CheckBordersRoutine() {
        var borders = Camera.main.ViewportToWorldPoint(new Vector3(0.15f, 0.1f));

        while (true) {
            if (transform.position.x < borders.x) {
                transform.position = new Vector3(borders.x, transform.position.y, transform.position.z);
            }
            if (transform.position.x > -borders.x) {
                transform.position = new Vector3(-borders.x, transform.position.y, transform.position.z);
            }
            if (transform.position.y < borders.y) {
                transform.position = new Vector3(transform.position.x, borders.y, transform.position.z);
            }
            if (transform.position.y > -borders.y) {
                transform.position = new Vector3(transform.position.x, -borders.y, transform.position.z);
            }

            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
