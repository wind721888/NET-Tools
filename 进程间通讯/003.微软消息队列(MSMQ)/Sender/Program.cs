// Program.cs (Sender Part)
using System;
using System.Messaging; // 引用 MSMQ 命名空间

namespace MSMQSender
{
    class Program
    {
        private const string QUEUE_PATH = @".\private$\MyVS2022ComplexQueue";

        static void Main(string[] args)
        {
            // 创建或连接队列
            MessageQueue queue = EnsureQueueExists(QUEUE_PATH);

            Console.WriteLine("开始发送消息...");

            // --- 1. 发送字符串 ---
            SendString(queue);

            // --- 2. 发送复杂对象 ---
            SendPersonObject(queue);

            Console.WriteLine("\n所有消息已发送完毕。按任意键退出发送端...");
            Console.ReadKey();
        }

        private static MessageQueue EnsureQueueExists(string path)
        {
            if (!MessageQueue.Exists(path))
            {
                Console.WriteLine($"队列不存在，正在创建：{path}");
                return MessageQueue.Create(path);
            }
            else
            {
                Console.WriteLine($"连接到现有队列：{path}");
                return new MessageQueue(path);
            }
        }

        private static void SendString(MessageQueue queue)
        {
            string messageBody = "Hello from VS2022 Sender! This is a string message.";
            var message = new Message(messageBody);
            message.Label = "String Message";

            queue.Send(message);
            Console.WriteLine($"已发送字符串消息: '{messageBody}'");
        }

        private static void SendPersonObject(MessageQueue queue)
        {
            var person = new Person("张三", 30, new DateTime(1993, 5, 15));
            var message = new Message(person); // 将对象直接放入 Body
            message.Label = "Person Object Message";

            queue.Send(message);
            Console.WriteLine($"已发送 Person 对象消息: {person}");
        }
    }
}