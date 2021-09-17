using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager_weixing : MonoBehaviour
{
    public GameObject[] SatellitePrefabs;
    public Transform Satellites;
    public GameObject NextPlaceButton;
    public GameObject Earth;
    public GameObject ZoomButton;
    public GameObject PlaceButton;
    public Dictionary<string, Vector3[]> PlacesAndPositions;
    public Dictionary<string, int[]> PlacesAndScales;
    public Vector3[] SatellitePositions;
    public GameObject SuccessText;
    public GameObject FailText;
    public GameObject MainBG;
    private float TotalTime = 60f;
    public Text GameCountTimeText;
    private Dictionary<string, Vector3> EarthAngles;//记录地点对应的旋转角度
    private GameObject currentSatellite;

    private string[] places;
    private bool startSpin;//选择地点时旋转控制
    private bool isStarted;//游戏时限计数器得判断
    private bool isEntered;//操作计数器得判断
    private string currentPlace;
    private int satelliteIndex = 0;
    private int placeIndex = 0;


    public float maxTimeOffset = 10;//检测时间间隔
    private float lasterTime;
    private float nowTime;
    private float offsetTime;
    // Start is called before the first frame update
    void Start()
    {
        places = new string[] {"beijing", "baxiliya", "kailuo", "huashengdun" };
        EarthAngles = new Dictionary<string, Vector3>();
        PlacesAndScales = new Dictionary<string, int[]>();
        PlacesAndPositions = new Dictionary<string, Vector3[]>();
        EarthAngles.Add("beijing",new Vector3(0, 265.99f, 0));
        EarthAngles.Add("baxiliya", new Vector3(0, 111.54f, 0));
        EarthAngles.Add("kailuo", new Vector3(0, 201.57f, 0));
        EarthAngles.Add("huashengdun", new Vector3(0, 77.08f, 0));

        PlacesAndScales.Add("beijing", new int[] { 400, 200, 300 });
        PlacesAndPositions.Add("beijing", new Vector3[] { new Vector3(-61.2f, 37.9f, -119.4f), new Vector3(25.7f, 81.6f, -122.9f), new Vector3(30.5f, 51.98f, -125f) });
        PlacesAndScales.Add("baxiliya", new int[] { 500, 500, 500 });
        PlacesAndPositions.Add("baxiliya", new Vector3[] { new Vector3(43.87f, 67.7f, -119.4f), new Vector3(-29.1f, -5.82f, -122.9f), new Vector3(31.4f, -60.24f, -125f) });
        PlacesAndScales.Add("kailuo", new int[] { 500, 400, 500 });
        PlacesAndPositions.Add("kailuo", new Vector3[] { new Vector3(-118.7f, 67.7f, -119.4f), new Vector3(7.4f, 32.81f, -122.9f), new Vector3(-10.6f, 94.9f, -125f) });
        PlacesAndScales.Add("huashengdun", new int[] {300,300,500});
        PlacesAndPositions.Add("huashengdun",new Vector3[] { new Vector3(65, 85.65f, -119.4f), new Vector3(-6.23f, 46.56f, -122.9f), new Vector3(-29f, 89f,-125f) });  
    }
    public void StartGame()
    {
        lasterTime = Time.time;
        MainBG.SetActive(false);
        SpinEarth(places[placeIndex]);
        isStarted = true;
        isEntered = true;
        StartCoroutine(CountTime());
    }
    // Update is called once per frame
    void Update()
    {
        if (startSpin)
        {
            Earth.transform.localEulerAngles = Vector3.Lerp(Earth.transform.localEulerAngles, EarthAngles[currentPlace], 0.1f);
        }
        if (isEntered)
        {
            nowTime = Time.time;
            if (Input.anyKey)
            {
                lasterTime = nowTime;
            }
            offsetTime = Mathf.Abs(nowTime - lasterTime);
            if (offsetTime > maxTimeOffset)
            {
                ReturnSence();
            }
        }
    }
    private void ResetSence()
    {
        isStarted = false;
        NextPlaceButton.SetActive(false);
        HidePlaceButton();
        HideZoomButton();
        SuccessText.SetActive(false);
        FailText.SetActive(false);
        for (int i = 1; i < Earth.transform.childCount; i++)
        {
            Earth.transform.GetChild(i).gameObject.SetActive(false);
        }
        for (int i = 0; i < Satellites.childCount; i++)
        {
            Destroy(Satellites.GetChild(i).gameObject);
        }
        TotalTime = 60f;
        GameCountTimeText.text = TotalTime.ToString();
        placeIndex = 0;
        satelliteIndex = 0;
        startSpin = false;
    }
    IEnumerator CountTime()
    {
        while (TotalTime >= 0 && isStarted)
        {
            GameCountTimeText.text = TotalTime.ToString();
            yield return new WaitForSeconds(1);
            TotalTime--;
        }
        if (TotalTime <= 0)
        {
            Failed();
        }    
    }
    public void SpinEarth(string place)
    {
        currentPlace = place;
        startSpin = true;

        InstantiateSatellites();
        HidePlaceButton();
    }

    public void SatelliteOver()
    {
        InstantiateSatellites();
    }
    public void ZoomIn()
    {
        currentSatellite.transform.Find("cloud").localScale = new Vector3(currentSatellite.transform.Find("cloud").localScale.x + 50, currentSatellite.transform.Find("cloud").localScale.y + 50, currentSatellite.transform.Find("cloud").localScale.z + 50);
    }

    public void ZoomOut()
    {
        currentSatellite.transform.Find("cloud").localScale = new Vector3(currentSatellite.transform.Find("cloud").localScale.x - 50, currentSatellite.transform.Find("cloud").localScale.y - 50, currentSatellite.transform.Find("cloud").localScale.z - 50);
    }
    public void DisplayZoomButton()
    {
        ZoomButton.SetActive(true);
    }

    public void HideZoomButton()
    {
        ZoomButton.SetActive(false);
    }

    public void DisplayPlaceButton()
    {
        PlaceButton.SetActive(true);
    }
    public void HidePlaceButton()
    {
        PlaceButton.SetActive(false);
    }
    private void InstantiateSatellites()
    {
        if (satelliteIndex < 3)
        {
            currentSatellite = Instantiate(SatellitePrefabs[satelliteIndex], Satellites);
            currentSatellite.GetComponent<AutoMove>().weixing = this;
            currentSatellite.GetComponent<AutoMove>().FixedScale = PlacesAndScales[currentPlace][satelliteIndex];
            currentSatellite.GetComponent<AutoMove>().FixedPosition = PlacesAndPositions[currentPlace][satelliteIndex];
            satelliteIndex++;
        }
        else
        {
            Earth.transform.Find(places[placeIndex]).gameObject.SetActive(true);
            if (placeIndex < places.Length - 1)
            {
                NextPlaceButton.SetActive(true);
            }
            else
            {
                Succeeded();
            }
            satelliteIndex = 0;
        }
        HideZoomButton();
    }

    public void OnePlaceOver()
    {
        NextPlaceButton.SetActive(false);
        for (int i = 0; i < Satellites.childCount; i++)
        {
            Destroy(Satellites.GetChild(i).gameObject);          
        }
        placeIndex++;
        SpinEarth(places[placeIndex]);     
    }

    public void Succeeded()
    {
        isStarted = false;
        SuccessText.SetActive(true);
    }
    public void Failed()
    {
        isStarted = false;
        FailText.SetActive(true);
    }

    public void Restart()
    {
        ResetSence();
        StartGame();
    }
    public void ReturnSence()
    {
        isEntered = false;
        MainBG.SetActive(true);
        ResetSence();     
    }
}
