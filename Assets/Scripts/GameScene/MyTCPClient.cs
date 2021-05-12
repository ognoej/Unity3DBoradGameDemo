using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using System;
using System.Text;


class MyTCPClient
{
    string m_ipAddress;
    int m_port;

    TcpClient m_tc = null;
    NetworkStream m_stream = null;

    byte[] m_buf;
    int m_bufSize = 1024;

    public bool IsConnected
    {
        get
        {
            if (m_tc == null || m_stream == null) return false;
            return m_tc.Connected;
        }
    }

    Queue<string> m_msgQueue = new Queue<string>();

    public MyTCPClient(string _ip, int _port, bool _tryConnect = true)
    {
        m_ipAddress = _ip;
        m_port = _port;

        if (_tryConnect == true)
            Connect();
    }
    public MyTCPClient(TcpClient _tc, int _port)
    {
        m_port = _port;
        Connect(_tc);
    }
    ~MyTCPClient()
    {
        Release();
    }

    public void Connect(TcpClient _tc = null)
    {
        if (m_tc != null)
            return;

        m_buf = new byte[m_bufSize];

        AsyncCallback method = null;

        method = _ar =>
        {
            try
            {
                var readSize = m_stream.EndRead(_ar);

                //  메시지 큐에 추가
                var str = Encoding.UTF8.GetString(m_buf, 0, readSize);
                m_msgQueue.Enqueue(str);

                m_stream.BeginRead(m_buf, 0, m_bufSize, method, null);
            }
            catch
            {
                Release();
            }
        };

        if (_tc == null)
        {
            m_tc = new TcpClient(AddressFamily.InterNetwork);

            m_tc.BeginConnect(m_ipAddress, m_port,

                _ar =>
                {
                    m_tc.EndConnect(_ar);

                    if (m_tc.Connected == true)
                    {
                        m_stream = m_tc.GetStream();
                        m_stream.BeginRead(m_buf, 0, m_bufSize, method, null);
                    }

                    else
                    {
                        m_tc.Close();
                        m_tc = null;
                    }

                }, null);
        }
        else
        {
            m_tc = _tc;
            m_stream = m_tc.GetStream();
            m_stream.BeginRead(m_buf, 0, m_bufSize, method, null);
        }
    }
    public void Release()
    {
        Debug.Log("서버 연결 해제");
        //print("서버 연결 해제");

        if (m_tc != null)
        {
            m_stream.Close();
            m_tc.Close();

            m_stream = null;
            m_tc = null;

            m_msgQueue.Clear();
        }
    }
    public bool GetMsg(ref string _msg)
    {
        if (m_msgQueue.Count == 0)
            return false;

        _msg = m_msgQueue.Dequeue();

        return true;
    }
    public void SendMsg(string _msg)
    {
        var buffer = Encoding.UTF8.GetBytes(_msg);
        m_stream.Write(buffer, 0, buffer.Length);
        Debug.Log(_msg + " 보냄");
    }
}

