using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class StartImage : MonoBehaviour
{
    private string imgPath;
    private int index;
    private string localPath;
    private bool isLoaded;
    private List<Texture2D> texture2Ds;
    WWW www;
    Texture2D tex;
    // Start is called before the first frame update
    void Start()
    {
        texture2Ds = new List<Texture2D>();
        localPath = Application.streamingAssetsPath + "/定位小能手/";
        imgPath = localPath + "定位小能手_" + string.Format("{0:00000}", index) + ".jpg";
        StartCoroutine(LoadImage());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnEnable()
    {
        StartCoroutine(StartAction());
    }

    IEnumerator StartAction()
    {
        while (isLoaded)
        {
            for (int i = 0; i < texture2Ds.Count; i++)
            {
                GetComponent<RawImage>().texture = texture2Ds[i];
                yield return 1;
            }
        }
    }
    IEnumerator LoadImage()
    {
        if (Directory.Exists(localPath))
        {
            //int count = Directory.GetFiles(localPath, ".jpg").Length;
            //Debug.Log(count);
            int count = 251;

            while (index < count)
            {
                imgPath = localPath + "定位小能手_" + string.Format("{0:00000}", index) + ".jpg";
                www = new WWW(imgPath);
                tex = www.texture;

                yield return www;
                if (www.isDone)
                {
                    texture2Ds.Add(tex);
                    index++;
                }
            }
            isLoaded = true;
            StartCoroutine(StartAction());
        }
    }
    private void OnApplicationQuit()
    {
        isLoaded = false;
    }
}
