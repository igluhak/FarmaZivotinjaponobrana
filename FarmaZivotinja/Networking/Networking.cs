using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using FarmaZivotinja.Core;
using FarmaZivotinja.Data;

namespace FarmaZivotinja.Networking
{
    public class FarmServer
    {
        private readonly Farma _farma;
        private TcpListener? _listener;
        public FarmServer(Farma farma) { _farma = farma; }

        public async Task StartAsync(int port, CancellationToken ct)
        {
            _listener = new TcpListener(IPAddress.Loopback, port);
            _listener.Start();
            while (!ct.IsCancellationRequested)
            {
                if (_listener.Pending())
                {
                    var client = await _listener.AcceptTcpClientAsync(ct);
                    _ = HandleClientAsync(client, ct);
                }
                await Task.Delay(100, ct);
            }
        }

        private async Task HandleClientAsync(TcpClient client, CancellationToken ct)
        {
            using var c = client;
            var stream = c.GetStream();
            var dto = DtoMapper.IzFarma(_farma);
            var json = JsonSerializer.Serialize(dto, new JsonSerializerOptions { WriteIndented = true });
            var bytes = Encoding.UTF8.GetBytes(json);
            await stream.WriteAsync(bytes, 0, bytes.Length, ct);
        }
    }

    public class FarmClient
    {
        public async Task<string> GetStateAsync(int port, CancellationToken ct)
        {
            using var client = new TcpClient();
            await client.ConnectAsync(IPAddress.Loopback, port, ct);
            var stream = client.GetStream();
            var buffer = new byte[8192];
            int read = await stream.ReadAsync(buffer, 0, buffer.Length, ct);
            return Encoding.UTF8.GetString(buffer, 0, read);
        }
    }
}
