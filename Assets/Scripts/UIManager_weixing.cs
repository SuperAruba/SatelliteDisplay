using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager_weixing : MonoBehaviour
{
    public GameObject[] Satellites;
    public GameObject Earth;
    public Dictionary<string, Vector3[]> PlacesAndPositions;
    public Dictionary<string, int[]> PlacesAndScales;
    public Vector3[] SatellitePositions;
    
    private Dictionary<string, Vector3> EarthAngles;//记录地点对应的旋转角度
    private GameObject currentSatellite;

    
    private bool startSpin;//选择地点时旋转控制
    private string currentPlace;
    private int index = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        EarthAngles = new Dictionary<string, Vector3>();
        PlacesAndScales = new Dictionary<string, int[]>();
        PlacesAndPositions = new Dictionary<string, Vector3[]>();
        EarthAngles.Add("beijing",new Vector3(0, 265.99f, 0));
        EarthAngles.Add("baxi", new Vector3(0, 111.54f, 0));
        EarthAngles.Add("kailuo", new Vector3(0, 201.57f, 0));
        EarthAngles.Add("huashengdun", new Vector3(0, 77.08f, 0));

        PlacesAndScales.Add("beijing", new int[] { 400, 200, 300 });
        PlacesAndPositions.Add("beijing", new Vector3[] { new Vector3(-61.2f, 37.9f, -119.4f), new Vector3(25.7f, 81.6f, -122.9f), new Vector3(30.5f, 51.98f, -125f) });
        PlacesAndScales.Add("baxi", new int[] { 500, 500, 500 });
        PlacesAndPositions.Add("baxi", new Vector3[] { new Vector3(43.87f, 67.7f, -119.4f), new Vector3(-29.1f, -5.82f, -122.9f), new Vector3(31.4f, -60.24f, -125f) });
        PlacesAndScales.Add("kailuo", new int[] { 500, 400, 500 });
        PlacesAndPositions.Add("kailuo", new Vector3[] { new Vector3(-118.7f, 67.7f, -119.4f), new Vector3(7.4f, 32.81f, -122.9f), new Vector3(-10.6f, 94.9f, -125f) });
        PlacesAndScales.Add("huashengdun", new int[] {300,300,500});
        PlacesAndPositions.Add("huashengdun",new Vector3[] { new Vector3(65, 85.65f, -119.4f), new Vector3(-6.23f, 46.56f, -122.9f), new Vector3(-29f, 89f,-125f) });
    }

    // Update is called once per frame
    void Update()
    {
        if (startSpin)
        {
            Earth.transform.localEulerAngles = Vector3.Lerp(Earth.transform.localEulerAngles, EarthAngles[currentPlace], 0.1f);
        }
        
    }
    public void SpinEarth(string place)
    {
        currentPlace = place;
        startSpin = true;

        InstantiateSatellites();
        //Earth.transform.localEulerAngles = Vector3.Lerp(Earth.transform.localEulerAngles, placePositions[place], 1);
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

    }

    public void HideZoomButton()
    {

    }

    private void InstantiateSatellites()
    {
        if (index < 3)
        {
            currentSatellite = Instantiate(Satellites[index]);
            currentSatellite.GetComponent<AutoMove>().weixing = this;
            currentSatellite.GetComponent<AutoMove>().FixedScale = PlacesAndScales[currentPlace][index];
            currentSatellite.GetComponent<AutoMove>().FixedPosition = PlacesAndPositions[currentPlace][index];
            Debug.Log(index);
            Debug.Log(PlacesAndPositions[currentPlace][index]);
            index++;
        }
        HideZoomButton();
    }
}
