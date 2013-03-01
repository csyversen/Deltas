using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Deltas
{
    class Program
    {

        static Queue<KeyValuePair<int, double>> q = new Queue<KeyValuePair<int, double>>();

        static void Main(string[] args)
        {
            Timer t = new Timer(new TimerCallback(EnqueueRandoms), null, 0, 1000);
            Task.Factory.StartNew(DequeueRandoms);

            //Let the threads run and do their thing
            Console.ReadLine();          
        }

        static void EnqueueRandoms(object o)
        {
            Random instrumentId = new Random();
            Random delta = new Random();

            for (int i = 0; i < 15000; i++)
            {

                lock (q)
                {
                    q.Enqueue(new KeyValuePair<int, double>(instrumentId.Next(0, 1000), (delta.NextDouble() - delta.NextDouble())));
                }
            }         
        }

        static void Compare(KeyValuePair<int, double> pair)
        {

            InstrumentSample i = new InstrumentSample();

            if (i.samples.ContainsKey(pair.Key))
            {
                Parallel.ForEach(i.samples[pair.Key], item =>
                {
                    if (pair.Value >= item.Key && pair.Value < item.Value)
                    {
                        Console.WriteLine("Trigger[" + pair.Key + "]: " + item.Key + " <= " + pair.Value + " <= " + item.Value);
                    }
                });
            }
        }

        static void DequeueRandoms()
        {
            
            while (true)
            {
                
                KeyValuePair<int, double> pair = new KeyValuePair<int,double>(-1, -1);

                //just lock the queue while dequeuing so no other thread grabs this same data, after that it can be freed for another thread to use                   
                lock (q)
                {                   
                    if (q.Count > 0)
                    {
                        pair = q.Dequeue();
                    }
                }
                Compare(pair);    
            }
        }
    }
}
