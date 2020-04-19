using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour
{
    float mouseX;
    float score;
    float got = 0;
    // Start is called before the first frame update
    void Start()
    {
        mouseX = Input.GetAxis("Mouse X");
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        print(score);
        if (score >= 100) got = 1;
        
        if ((Input.GetAxis("Mouse X") != mouseX)&&(got==0))
        {
            score += 0.1f;
            transform.rotation =  Quaternion.Euler((float)Math.Sin(score)*20, transform.eulerAngles.y, transform.eulerAngles.z);
        }
        else
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, transform.eulerAngles.y, transform.eulerAngles.z), 5f * Time.deltaTime);
            if (transform.eulerAngles.x < 0.5f)
            {
                score = 0;
                if (got != 0)
                {
                    got = 0;
                    score = 0;
                    GameObject.Find("Player").GetComponent<PlayerMove1>().enabled = true;
                    GameObject.Find("Camera").GetComponent<CameraUpDown>().enabled = true;
                    // GameObject.Find("Player").GetComponent<PlayerScript>().getItem(item);
                    GetComponent<Shaking>().enabled = false;
                }
            }
            if(score != 0)
            {
                score -= 0.02f;
            }
        }
    }
}
