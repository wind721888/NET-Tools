using System;
using System.IO.MemoryMappedFiles;
using System.Text;

class MemoryMapWriter
{
    static void Main()
    {
        Console.WriteLine("Memory Mapped File Writer - 启动中...");
        
        const string mapName = "SharedMemory";
        const int capacity = 1024;
        
        using (var mmf = MemoryMappedFile.CreateOrOpen(mapName, capacity))
        using (var accessor = mmf.CreateViewAccessor())
        {
            Console.WriteLine("共享内存已创建，开始写入数据...");
            
            while (true)
            {
                Console.Write("输入要写入共享内存的数据 (输入 'exit' 退出): ");
                string input = Console.ReadLine();
                
                if (string.IsNullOrEmpty(input))
                    continue;
                    
                if (input.ToLower() == "exit")
                    break;
                
                // 将字符串写入共享内存
                byte[] data = Encoding.UTF8.GetBytes(input);
                if (data.Length > capacity - 4) // 留4字节存储长度
                {
                    Console.WriteLine($"数据太长，最大长度: {capacity - 4}");
                    continue;
                }
                
                // 先写入数据长度
                accessor.Write(0, data.Length);
                // 写入实际数据
                accessor.WriteArray(4, data, 0, data.Length);
                
                Console.WriteLine($"已写入: {input}");
            }
        }
        
        Console.WriteLine("写入器关闭");
    }
}