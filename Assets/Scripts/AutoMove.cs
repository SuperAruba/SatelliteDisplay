using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public Vector3 FixedPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position,FixedPosition) < 20)
        {
            transform.position = Vector3.Lerp(transform.position, FixedPosition, 1);
        }
        
    }
}
