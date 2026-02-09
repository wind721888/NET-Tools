using System;
using System.IO.Pipes;
using System.Text;

class NamedPipeClient
{
    static void Main()
    {
        Console.WriteLine("Named Pipe Client - 启动中...");
        
        try
        {
            using (var pipeClient = new NamedPipeClientStream(".", "TestPipe", PipeDirection.InOut))
            {
                Console.WriteLine("连接到服务器...");
                pipeClient.Connect(5000);
                Console.WriteLine("已连接到服务器!");
                
                while (true)
                {
                    Console.Write("输入消息 (输入 'exit' 退出): ");
                    string message = Console.ReadLine();
                    
                    if (string.IsNullOrEmpty(message))
                        continue;
                    
                    // 发送消息
                    byte[] messageBytes = Encoding.UTF8.GetBytes(message);
                    pipeClient.Write(messageBytes, 0, messageBytes.Length);
                    pipeClient.Flush();
                    
                    if (message.ToLower() == "exit")
                        break;
                    
                    // 读取响应
                    var buffer = new byte[1024];
                    int bytesRead = pipeClient.Read(buffer, 0, buffer.Length);
                    string response = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                    Console.WriteLine($"服务器响应: {response}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"错误: {ex.Message}");
        }
        
        Console.WriteLine("客户端关闭");
    }
}