using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReactiveTarget : MonoBehaviour
{

    public int ObjectId;
    Transform parent;
    Color col;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent;
        col = transform.gameObject.GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
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


        if ((parent.gameObject.tag == "Cupboard") && (Input.GetMouseButtonDown(0)))
        {
            parent.GetComponent<CupboardOpenClose>().openClose(ObjectId, transform.gameObject);
        }
        return ("открыть/закрыть");
    }

}
