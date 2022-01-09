using UnityEngine;
using System.Collections;
using System.IO;
using System.IO.Ports;
using System.Threading;

public class Serial_Handler : MonoBehaviour
{
    public delegate void SerialDataReceivedEventHandler(string message);
    public event SerialDataReceivedEventHandler OnDataReceived;

    private string portName;
    private int baudRate = 19200;

    private SerialPort serialPort_;
    private Thread thread_;
    private bool isRunning_ = false;

    private string message_;
    private bool isNewMessageReceived_ = false;

    public bool isSerialActive = false; // 自発的にシリアル通信をしてきているか？


    void Awake(){
        DetectPortName();
        Open();
    }

    void Update()
    {
        if (isNewMessageReceived_) {
            isSerialActive = true;
            OnDataReceived(message_);
            isNewMessageReceived_ = false;
        }
    }

    void OnDestroy()
    {
        Close();
    }

    private void DetectPortName(){
        try{
            var serialName = gameObject.GetComponent<Serial_name>();
            this.portName = serialName.portName;
        }
        catch{
            this.portName = "COM3";
        }
    }

    private void Open()
    {
        try{
            serialPort_ = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One);
            serialPort_.Open();

            isRunning_ = true;

            thread_ = new Thread(Read);
            thread_.Start();
        }
        catch(IOException){
            // Debug.LogException(ex);
        }
    }

    private void Close()
    {
        isRunning_ = false;

        if (thread_ != null && thread_.IsAlive) {
            thread_.Join();
        }

        if (serialPort_ != null && serialPort_.IsOpen) {
            serialPort_.Close();
            serialPort_.Dispose();
        }
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
        try {
            serialPort_.Write(message);
        } catch (System.Exception e) {
            Debug.LogWarning(e.Message);
        }
    }

    public bool isOpen(){
        return (serialPort_ != null && serialPort_.IsOpen);
    }
}