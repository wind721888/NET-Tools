# 命名管道 (Named Pipes)

## 文件
- `NamedPipeServer.cs` - 服务器端
- `NamedPipeClient.cs` - 客户端

## 运行方式
```bash
# 终端1 - 启动服务器
dotnet run NamedPipeServer.cs

# 终端2 - 启动客户端
dotnet run NamedPipeClient.cs
```

## 特点
- 双向通讯
- 可靠性高
- 支持网络通讯
- 适合大量数据传输