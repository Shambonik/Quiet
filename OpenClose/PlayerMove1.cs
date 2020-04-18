using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PlayerMove1 : MonoBehaviour
{
    public int speed = 5;
    public int speedRotation = 200;
    Vector3 jump = new Vector3(0, 1f, 0);
    public Rigidbody rigidbody;
    float Y;
    bool onEarth = true;

    void Start()
    {
        Y = 0;
    }

    void Update()
    {
        Y += Input.GetAxis("Mouse X") * Time.deltaTime * speedRotation;
        transform.rotation = Quaternion.Euler(0, Y, 0);
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        Vector3 position = transform.position;
        position += transform.forward * speed * Time.deltaTime * moveVertical;
        position += transform.right * speed * Time.deltaTime * moveHorizontal;
        transform.position = position;
        if (Input.GetKeyDown(KeyCode.Space) && onEarth)
        {
            rigidbody.AddForce(jump * 5f, ForceMode.Impulse);
            onEarth = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Earth"))
        {
            onEarth = true;
        }
    }

    /*void OnCollisionEnter(Collision collision)
    {
        Transform temp = collision.gameObject.transform.parent;
        collision.gameObject.transform.parent = null;

        Debug.Log("collision " + (collision.gameObject.transform.position.y + collision.gameObject.transform.localScale.y - (transform.position.y - transform.localScale.y / 2)));
        Debug.Log(transform.position.y);
        if ((collision.gameObject.transform.position.y + collision.gameObject.transform.localScale.y - (transform.position.y - transform.localScale.y / 2)) < (transform.localScale.y / 2))
        {
            rigidbody.AddForce(jump * 5f, ForceMode.Impulse);
        }
        collision.gameObject.transform.parent = temp;
    }*/
}