using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class imagelist : MonoBehaviour
{

    public Sprite[] image;
    public Image ima;
    public int imagesum;
    private int i;
    private float timer=1;
   public int timerend=10;
    // Start is called before the first frame update
    void Start()
    {
        imagesum = image.Length;
    }

    // Update is called once per frame
    void Update()
    {
        timer += 1;
        if (timer >timerend)
        {
            i++;
            if (i == image.Length)
            {
                i = 0;
            }
            ima.sprite = image[i];
            timer = 0;
        }
        
       

        //if (true)
        //{

        //}
        //else if (true)
        //{

        //}

    }
}
