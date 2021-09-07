using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Text;

public class UIManager : MonoBehaviour
{
    public string portName = "COM2";//串口号
    public int baudRate = 9600;//波特率
    public Parity parity = Parity.None;//效验位
    public int dataBits = 8;//数据位
    public StopBits stopBits = StopBits.One;//停止位
    public GameObject StartButton;
    public GameObject OverButton;
    SerialPort sp = null;
    public List<byte> listReceive = new List<byte>();
    char[] strchar = new char[100];//接收的字符信息转换为字符数组信息
    string str;
    private bool changeFlag;

    void Start()
    {
        OpenPort();
    }
    public void OpenPort()
    {

        //创建串口
        sp = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
        sp.ReadTimeout = 400;
        try
        {
            sp.Open();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
            Debug.Log(portName);
            Debug.Log(baudRate);
            Debug.Log(parity);
            Debug.Log(dataBits);
            Debug.Log(stopBits);
        }
    }

    void OnApplicationQuit()
    {
        ClosePort();
    }

    public void ClosePort()
    {
        try
        {
            sp.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }

    private void Update()
    {
        //PrintData();

    }

    //打印数据
    void PrintData()
    {
        for (int i = 0; i < listReceive.Count; i++)
        {
            strchar[i] = (char)(listReceive[i]);
            str = new string(strchar);
        }
        Debug.Log(str);

    }

    //接收数据
    void DataReceiveFunction()
    {
        byte[] buffer = new byte[1024];
        int bytes = 0;
        while (true)
        {
            if (sp != null && sp.IsOpen)
            {
                try
                {
                    bytes = sp.Read(buffer, 0, buffer.Length);//接收字节
                    if (bytes == 0)
                    {
                        continue;
                    }
                    else
                    {
                        string strbytes = Encoding.Default.GetString(buffer);
                        //接受的数据
                        Debug.Log(strbytes);
                    }
                }
                catch (Exception ex)
                {
                    if (ex.GetType() != typeof(ThreadAbortException))
                    {
                    }
                }
            }
            Thread.Sleep(10);
        }
    }

    //发送数据
    public void WriteData(string dataStr)
    {
        Debug.Log(dataStr);
        if (sp.IsOpen)
        {
            sp.Write(dataStr);

        }
    }

    public void StartGame()
    {
        StartButton.gameObject.SetActive(false);
        OverButton.gameObject.SetActive(true);
        WriteData("A");
    }
    public void RecoveryStatus()
    {
        StartButton.gameObject.SetActive(true);
        OverButton.gameObject.SetActive(false);
        WriteData("B");
    }
    public void Quit()
    {
        Application.Quit();
    }
}