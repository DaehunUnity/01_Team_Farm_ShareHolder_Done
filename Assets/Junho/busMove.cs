using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class busMove : MonoBehaviour
{
    public float speed;

    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1, 0, 0) * speed * Time.deltaTime;
        if(transform.position.x >= 356f)
        {
            transform.position = new Vector3(-681f, 2.5f, 131f);
        }
    }

}
