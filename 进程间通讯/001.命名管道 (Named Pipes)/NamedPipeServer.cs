using System;
using System.IO.Pipes;
using System.Text;

class NamedPipeServer
{
    static void Main()
    {
        Console.WriteLine("Named Pipe Server - 启动中...");
        
        using (var pipeServer = new NamedPipeServerStream("TestPipe", PipeDirection.InOut, 1, PipeTransmissionMode.Byte))
        {
            Console.WriteLine("等待客户端连接...");
            pipeServer.WaitForConnection();
            Console.WriteLine("客户端已连接!");
            
            while (true)
            {
                // 读取客户端消息
                var buffer = new byte[1024];
                int bytesRead = pipeServer.Read(buffer, 0, buffer.Length);
                string message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                
                Console.WriteLine($"收到: {message}");
                
                if (message.ToLower() == "exit")
                    break;
                
                // 发送响应
                string response = $"服务器回复: {message}";
                byte[] responseBytes = Encoding.UTF8.GetBytes(response);
                pipeServer.Write(responseBytes, 0, responseBytes.Length);
                pipeServer.Flush();
            }
        }
        
        Console.WriteLine("服务器关闭");
    }
}