using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace CV_clone
{
    public class Queue<T>
    {
        List<T> queueList;
        Timer timer;

        public Queue()
        {
            queueList = new List<T>();
            timer = new Timer(1500);
        }

        public List<T> Content
        {
            get { return queueList; }
        }

        public bool IsEmpty
        {
            get { return queueList.Count > 0; }
        }

        public void AddItem(T item)
        {
            queueList.Add(item);
            timer = new Timer(1500);
        }

        public T ReleaseFirst()
        {
           return queueList[0];
        }

        public void Clear()
        {
            queueList.Clear();
        }

        public void Update(GameTime time)
        {
            timer.Update(time);
            if (timer.Done)
            {
                timer = new Timer(1500);
                Reset();
            }
            if (queueList.Count == 30)
            {
                int max = 20;
                queueList.RemoveRange(0, max);
            }
        }

        public void Reset()
        {
            for (int i = 0; i < queueList.Count; i++)
            {
                queueList.RemoveAt(i);
            }
        }
    }
}
