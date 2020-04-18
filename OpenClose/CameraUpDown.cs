﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraUpDown : MonoBehaviour
{
    Camera camera;
    public int speedRotation = 200;
    float X;
    float Y;
    GameObject lastHit = null;
    string text = "";

    void Start()
    {
        camera = GetComponent<Camera>();
        X = 0;
        Y = 0;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        //transform.gameObject.GetComponent<Renderer>().material.color = col;
        X -= Input.GetAxis("Mouse Y") * Time.deltaTime * speedRotation;
        Y += Input.GetAxis("Mouse X") * Time.deltaTime * speedRotation;
        transform.rotation = Quaternion.Euler(X, Y, 0);
        GameObject hitObject = null;
        RaycastHit hit;
        if ((Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20f)))
        {
            hitObject = hit.transform.gameObject;
            
            ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
            if (target != null)
            {
                text = target.React();
                if ((lastHit != null) && (lastHit != hitObject))
                {
                    lastHit.GetComponent<ReactiveTarget>().setOriginalColor();
                    lastHit = null;
                }
                lastHit = hitObject;
            }
            else
            {
                if (lastHit != null)
                {
                    lastHit.GetComponent<ReactiveTarget>().setOriginalColor();
                    lastHit = null;
                }
                text = "";
            }
        }
        else
        {
            if (lastHit != null)
            {
                lastHit.GetComponent<ReactiveTarget>().setOriginalColor();
                lastHit = null;
            }
            text = "";
            text = "";
        }
    }

    

    void OnGUI()
    {
        int size = 12;
        float posX = camera.pixelWidth / 2 - size / 4;
        float posY = camera.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
        size = 100;
        posX = camera.pixelWidth / 2 - size / 4;
        posY = camera.pixelHeight / 8 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), text);
    }

    public void textChange(string t)
    {
        text = t;
    }
}
