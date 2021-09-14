using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public UIManager_weixing weixing;
    public Vector3 FixedPosition;
    public int FixedScale;
    private bool isAdsorbed;

    // Update is called once per frame
    void Update()
    {
        if (!isAdsorbed)
        {
            if (Vector3.Distance(transform.position, FixedPosition) < 20)
            {
                Destroy(GetComponent<DragTest>());
                transform.position = Vector3.Lerp(transform.position, FixedPosition, 1);

                transform.GetChild(0).GetComponent<SpectrumController>().enabled = true;
                transform.Find("cloud").gameObject.SetActive(true);
                weixing.DisplayZoomButton();
                isAdsorbed = true;
            }
        }
        if (isAdsorbed)
        {
            if (Mathf.Abs(transform.Find("cloud").localScale.x - FixedScale) < 0.1f)
            {
                weixing.SatelliteOver();
                Destroy(this);
            }
        }
        
    }
}
