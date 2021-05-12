using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class  MessageQueue{


    private static MessageQueue instance;
    public static MessageQueue GetInstance // 싱글톤
    {
        get
        {
            if(instance == null)
            {
                instance = new MessageQueue();
                return instance;
            }
            return instance;
        }
    }

    private Queue<string> messagequqe;

    private MessageQueue()
    {
        messagequqe = new Queue<string>();
    }

	public void PushData(string data)
    {
        messagequqe.Enqueue(data);
    }

    public string GetData()
    {
        if (messagequqe.Count > 0)
        {
            return messagequqe.Dequeue();
        }
        else
            return string.Empty;
    }
}
