using System;
using System.IO.MemoryMappedFiles;
using System.Text;
using System.Threading;

class MemoryMapReader
{
    static void Main()
    {
        Console.WriteLine("Memory Mapped File Reader - 启动中...");
        
        const string mapName = "SharedMemory";
        const int capacity = 1024;
        
        try
        {
            using (var mmf = MemoryMappedFile.OpenExisting(mapName))
            using (var accessor = mmf.CreateViewAccessor())
            {
                Console.WriteLine("连接到共享内存成功，开始监听数据...");
                
                int lastLength = 0;
                
                while (true)
                {
                    // 读取数据长度
                    int dataLength = accessor.ReadInt32(0);
                    
                    if (dataLength > 0 && dataLength != lastLength)
                    {
                        // 读取实际数据
                        byte[] data = new byte[dataLength];
                        accessor.ReadArray(4, data, 0, dataLength);
                        
                        string message = Encoding.UTF8.GetString(data);
                        Console.WriteLine($"读取到新数据: {message}");
                        
                        lastLength = dataLength;
                        
                        if (message.ToLower() == "exit")
                            break;
                    }
                    
                    Thread.Sleep(500); // 每500ms检查一次
                }
            }
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("找不到共享内存，请先启动写入器");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"错误: {ex.Message}");
        }
        
        Console.WriteLine("读取器关闭");
    }
}