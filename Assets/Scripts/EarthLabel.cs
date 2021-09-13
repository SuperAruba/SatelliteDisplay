using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthLabel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.forward = new Vector3(transform.position.x - Camera.main.transform.position.x, 0, transform.position.z - Camera.main.transform.position.z);
    }
}
