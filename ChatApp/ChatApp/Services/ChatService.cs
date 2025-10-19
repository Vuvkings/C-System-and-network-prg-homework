using ChatApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public class ChatService : IDisposable
    {
        private const string MulticastGroup = "239.0.0.1";
        private const int Port = 11000;

        private UdpClient _udpClient;
        private IPEndPoint _multicastEndpoint;
        private string _currentLogin;
        private bool _isRunning = true;
        private Timer _pingTimer;

        public event Action<ChatMessage> MessageReceived;
        public event Action<string> PingReceived;

        public ChatService(string login)
        {
            _currentLogin = login;
            _multicastEndpoint = new IPEndPoint(IPAddress.Parse(MulticastGroup), Port);

            _udpClient = new UdpClient();
            _udpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _udpClient.Client.Bind(new IPEndPoint(IPAddress.Any, Port));
            _udpClient.JoinMulticastGroup(IPAddress.Parse(MulticastGroup));
            // Включаем loopback (чтобы сообщения от одного клиента доходили до других на той же машине)
            _udpClient.MulticastLoopback = true;

            Task.Run(ListenForMessages);
            //чтобы пользователь появлялся мгновенно при входе (без 5-секундного ожидания первого ping)
            SendPing(null);

            _pingTimer = new Timer(SendPing, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        }

        private void SendPing(object state)
        {
            if (!_isRunning) return;
            var ping = new ChatMessage { Type = "ping", From = _currentLogin };
            SendMessage(ping);
        }

        public void SendPublicMessage(string text)
        {
            var msg = new ChatMessage { Type = "text", From = _currentLogin, Text = text };
            SendMessage(msg);
        }

        public void SendPrivateMessage(string to, string text)
        {
            var msg = new ChatMessage { Type = "text", From = _currentLogin, To = to, Text = text };
            SendMessage(msg);
        }

        public void SendLogout()
        {
            var msg = new ChatMessage { Type = "logout", From = _currentLogin };
            SendMessage(msg);
        }

        private void SendMessage(ChatMessage msg)
        {
            try
            {
                var json = JsonConvert.SerializeObject(msg);
                var bytes = Encoding.UTF8.GetBytes(json);
                _udpClient.Send(bytes, bytes.Length, _multicastEndpoint);
            }
            catch (ObjectDisposedException) { }
            catch { /* игнорируем ошибки отправки */ }
        }

        private async void ListenForMessages()
        {
            while (_isRunning)
            {
                try
                {
                    var result = await _udpClient.ReceiveAsync();
                    var json = Encoding.UTF8.GetString(result.Buffer);
                    var msg = JsonConvert.DeserializeObject<ChatMessage>(json);

                    if (msg == null) continue;

                    if (msg.Type == "ping")
                    {
                        if (!string.IsNullOrEmpty(msg.From))
                        {
                            PingReceived?.Invoke(msg.From); // <-- Уведомляем UI
                        }
                        continue;
                    }

                    if (msg.Type == "logout")
                    {
                        // Можно добавить обработку выхода, но для простоты полагаемся на таймаут
                        continue;
                    }

                    // Фильтрация текстовых сообщений
                    if (msg.To == "ALL" || msg.To == _currentLogin || msg.From == _currentLogin)
                    {
                        MessageReceived?.Invoke(msg);
                    }
                }
                catch (ObjectDisposedException)
                {
                    break;
                }
                catch
                {
                    // Игнорируем ошибки
                }
            }
        }

        public void Dispose()
        {
            _isRunning = false;
            _pingTimer?.Dispose();
            SendLogout(); // Уведомляем других, что уходим
            _udpClient?.Dispose();
        }
    }
}
