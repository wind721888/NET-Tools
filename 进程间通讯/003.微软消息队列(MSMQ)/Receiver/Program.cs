// Receiver Program.cs (if in separate project or copied logic)
using System;
using System.Messaging;
using System.Runtime.Remoting.Messaging;

namespace MSMQReceiver
{
    class Program
    {
        private const string QUEUE_PATH = @".\private$\MyVS2022ComplexQueue"; // 与发送端相同的路径

        static void Main(string[] args)
        {
            MessageQueue queue = new MessageQueue(QUEUE_PATH);
            Console.WriteLine($"正在监听队列：{QUEUE_PATH}. 按 Ctrl+C 退出接收。");

            try
            {
                while (true)
                {
                    // 设置 Formatter 以处理 string 和 Person
                    queue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string), typeof(Person) });

                    Console.WriteLine("\n等待接收消息...");
                    Message receivedMessage = queue.Receive(TimeSpan.FromSeconds(30));

                    if (receivedMessage != null)
                    {
                        Console.WriteLine($"--- 收到消息 ---");
                        Console.WriteLine($"Label: {receivedMessage.Label}");

                        if (receivedMessage.Body is string strBody)
                        {
                            Console.WriteLine($"Body (String): {strBody}");
                        }
                        else if (receivedMessage.Body is Person personBody)
                        {
                            Console.WriteLine($"Body (Person): {personBody}");
                        }
                        else
                        {
                            Console.WriteLine($"Body (Unknown Type): {receivedMessage.Body?.GetType().FullName} - Value: {receivedMessage.Body}");
                        }
                        Console.WriteLine("----------------");
                    }
                    else
                    {
                        Console.WriteLine("在指定时间内未收到消息。继续等待...");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"接收过程中发生错误: {e.Message}");
            }
        }
    }
}