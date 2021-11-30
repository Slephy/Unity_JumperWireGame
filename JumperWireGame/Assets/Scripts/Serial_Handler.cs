using UnityEngine;
using System.Collections;
using System.IO.Ports;
using System.Threading;

public class Serial_Handler : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

    public string portName = "COM3";
    public int baudRate    = 19200;

    private SerialPort serialPort_;
    private Thread thread_;
    private bool isRunning_ = false;

    private string message_;
    private bool isNewMessageReceived_ = false;

    void Awake()
    {
        Debug.Log("Awake");
        Open();
    }

    void Update()
    {
        if (isNewMessageReceived_) {
            OnDataReceived(message_);
            isNewMessageReceived_ = false;
        }
    }

    void OnDestroy()
    {
        Close();
    }

    private void Open()
    {
        Debug.Log("Open1");
        serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
        Debug.Log("Open2");
        serialPort_.Open();

        isRunning_ = true;

        thread_ = new Thread(Read);
        thread_.Start();
    }

    private void Close()
    {
        Debug.Log("Close1");
        isRunning_ = false;

        if (thread_ != null && thread_.IsAlive) {
            thread_.Join();
        }
        Debug.Log("Close2");

        if (serialPort_ != null && serialPort_.IsOpen) {
            Debug.Log("Close3");
            serialPort_.Close();
            Debug.Log("Close4");
            serialPort_.Dispose();
            Debug.Log("Close5");
        }
        Debug.Log("Close6");
    }

    private void Read()
    {
        while (isRunning_ && serialPort_ != null && serialPort_.IsOpen) {
            try {
                if (serialPort_.BytesToRead > 0) {
                    message_ = serialPort_.ReadLine();
                    isNewMessageReceived_ = true;
                }
            } catch (System.Exception e) {
                Debug.LogWarning(e.Message);
            }
        }
    }

    public void Write(string message)
    {
        // Debug.Log("Write");
        try {
            serialPort_.Write(message);
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }
}