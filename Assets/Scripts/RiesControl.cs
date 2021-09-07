using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiesControl : MonoBehaviour
{
    public float StartPosionY;
    public float FinPositionY;
    private bool isStarted;
    private float posy;
    private float riseSpeed;
    private RectTransform rectTransform;
    public UIManager uiManager;
    // Start is called before the first frame update
    void Start()
    {
        riseSpeed = 500.0f;
        rectTransform = GetComponent<RectTransform>();
        posy = StartPosionY;
    }

    // Update is called once per frame
    void Update()
    {
        if (isStarted)
        {
            posy += Time.smoothDeltaTime * riseSpeed;
            rectTransform.localPosition = new Vector3(0, posy, 0);
            if (FinPositionY - posy < 0.01)
            {
                RecoveryStartPosion();
            }
        }
    }

    public void StartRise()
    {
        isStarted = true;
    }

    public void RecoveryStartPosion()
    {
        isStarted = false;
        rectTransform.localPosition = new Vector3(0, StartPosionY, 0);
        posy = StartPosionY;
        uiManager.RecoveryStatus();
    }
}
