using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowMove : MonoBehaviour
{
    private float _speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.gameObject.transform.position += new Vector3(0.0f, -1.0f, 0.0f);
    }
}
