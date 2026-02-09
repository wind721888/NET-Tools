# 内存映射文件 (Memory-Mapped Files)

## 文件
- `MemoryMapWriter.cs` - 写入进程
- `MemoryMapReader.cs` - 读取进程

## 运行方式
```bash
# 终端1 - 启动写入器
dotnet run MemoryMapWriter.cs

# 终端2 - 启动读取器
dotnet run MemoryMapReader.cs
```

## 特点
- 高性能
- 共享内存访问
- 适合频繁读写
- 同一台机器内使用