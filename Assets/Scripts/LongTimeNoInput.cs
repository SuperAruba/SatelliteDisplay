using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongTimeNoInput : MonoBehaviour



{

    public float maxTimeOffset = 10;//检测时间间隔
    private float lasterTime;
    private float nowTime;
    private float offsetTime;
    private void Awake()
    {
        lasterTime = Time.time;
        DontDestroyOnLoad(this);
        }



// Start is called before the first frame update
void Start()
    { 
    
    
    
    }

// Update is called once per frame
void Update()
    {
        nowTime = Time.time;


        if (Input.anyKey)


        {


            Debug.LogError("操作中。。。。");


            lasterTime = nowTime;


        }





        offsetTime = Mathf.Abs(nowTime - lasterTime);


        if (offsetTime > maxTimeOffset)


        {


            Debug.Log("长时间无操作 offsetTime：" + offsetTime);





        }



    }
}
