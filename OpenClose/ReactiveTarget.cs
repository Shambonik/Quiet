using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{

    public int ObjectId;
    public float deltaPosition = 2.5f;
    Transform parent;
    Color col;
    public int opened = 0;
    float angle;
    float changePosition;
    float posOpen;
    float posClose;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        col = transform.gameObject.GetComponent<Renderer>().material.color;
        angle =transform.eulerAngles.y;
        posClose = transform.localPosition.z;
        posOpen = posClose + deltaPosition;
        changePosition = posOpen * opened - (posClose * (opened - 1));
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.tag != "WardrobeShake"))
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z), 2f * Time.deltaTime);
            transform.localPosition = Vector3.Lerp(transform.localPosition, new Vector3(transform.localPosition.x, transform.localPosition.y, changePosition), 2f * Time.deltaTime);
        }
    }

    public void setOriginalColor()
    {
        transform.gameObject.GetComponent<Renderer>().material.color = col;
    }


    public string React()
    {
        transform.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0.96f, 0.321f, 0);
        if (parent.gameObject.tag == "Wardrobe") 
        { 
            if (ObjectId != 0) 
            { 
                if (Input.GetMouseButtonDown(0))
                {
                    parent.GetComponent<WardrobeSecret>().openCases(ObjectId);
                }
                return ("открыть/закрыть");
            }
            else
            {
                string text = parent.GetComponent<WardrobeSecret>().getTextOpened();
                if ((text == "открыть/закрыть") &&(Input.GetMouseButtonDown(0)))
                {
                    parent.GetComponent<WardrobeSecret>().openSecretCase();
                }
                if(text=="заперто") transform.gameObject.GetComponent<Renderer>().material.color = new Color(1, 0.48f, 0.46f, 0);
                return text;
            }
        }

        if ((transform.tag == "Door") && (Input.GetMouseButtonDown(0)))
        {
            opened = (opened+1)%2;
            angle -= ((ObjectId - 0.5f) * 180) * (opened-0.5f)*2;
            return ("открыть/закрыть");
        }

        if ((transform.tag == "Case") && (Input.GetMouseButtonDown(0)))
        {
            opened = (opened + 1) % 2;
            changePosition = posOpen * opened - (posClose * (opened - 1));
            return ("открыть/закрыть");
        }

        if ((transform.tag == "WardrobeShake") && (Input.GetMouseButtonDown(0)))
        {
            GameObject.Find("Player").GetComponent<PlayerMove1>().enabled = false;
            GameObject.Find("Camera").GetComponent<CameraUpDown>().enabled = false;
            parent.GetComponent<Shaking>().enabled = true;
            return ("трясьти");
        }
        return "";
    }

}
