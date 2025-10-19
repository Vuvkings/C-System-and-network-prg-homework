using ChatApp.Models;
using ChatApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace ChatApp
{
    /// <summary>
    /// Логика взаимодействия для ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        private ChatService _chatService;
        private string _currentUser;
        private readonly Dictionary<string, DateTime> _lastPingTime = new Dictionary<string, DateTime>();
        private readonly DispatcherTimer _cleanupTimer;

        public ChatWindow(string login)
        {
            InitializeComponent();

            _currentUser = login;
            Title = $"Чат — {login}";

            _chatService = new ChatService(login);
            _chatService.MessageReceived += OnMessageReceived;
            _chatService.PingReceived += OnPingReceived;

            LoadOnlineUsers();

            _cleanupTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(7) };
            _cleanupTimer.Tick += OnCleanupTimerTick;
            _cleanupTimer.Start();
        }

        private void OnPingReceived(string userLogin)
        {
            if (userLogin != _currentUser)
            {
                _lastPingTime[userLogin] = DateTime.Now;
                Dispatcher.Invoke(LoadOnlineUsers); // Обновляем UI
            }
        }

        private void OnCleanupTimerTick(object sender, EventArgs e)
        {
            var cutoff = DateTime.Now.AddSeconds(-12); // Если не было ping >12 сек — offline
            var toRemove = _lastPingTime
                .Where(kvp => kvp.Value < cutoff)
                .Select(kvp => kvp.Key)
                .ToList();

            foreach (var user in toRemove)
            {
                _lastPingTime.Remove(user);
            }

            Dispatcher.Invoke(LoadOnlineUsers);
        }

        private void LoadOnlineUsers()
        {
            RecipientComboBox.Items.Clear();
            RecipientComboBox.Items.Add("Все");

            foreach (var user in _lastPingTime.Keys)
            {
                RecipientComboBox.Items.Add(user);
            }

            RecipientComboBox.SelectedIndex = 0;
        }

        private void OnMessageReceived(ChatMessage msg)
        {
            string prefix = msg.To == "ALL" ? "[Общий]" : $"[Приватно от {msg.From}]";
            string display = $"{msg.Time:T} {prefix} {msg.From}:\n{msg.Text}";
            Dispatcher.Invoke(() => MessagesListBox.Items.Add(display));
        }

        private void OnSendMessageClick(object sender, RoutedEventArgs e)
        {
            var text = MessageInputTextBox.Text.Trim();
            if (string.IsNullOrWhiteSpace(text)) return;

            var recipient = RecipientComboBox.SelectedItem?.ToString();
            if (recipient == "Все")
            {
                _chatService.SendPublicMessage(text);
            }
            else if (!string.IsNullOrEmpty(recipient))
            {
                _chatService.SendPrivateMessage(recipient, text);
            }

            MessageInputTextBox.Clear();
        }

        protected override void OnClosed(EventArgs e)
        {
            UserService.SetUserOffline(_currentUser);
            _chatService?.Dispose();
            base.OnClosed(e);
        }

        private void MessageInputTextBox_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                if (Keyboard.Modifiers == ModifierKeys.Shift)
                {
                    // Shift+Enter → вставляем новую строку, не отправляем сообщение
                    var textBox = sender as TextBox;
                    int caretIndex = textBox.CaretIndex;
                    textBox.Text = textBox.Text.Insert(caretIndex, Environment.NewLine);
                    textBox.CaretIndex = caretIndex + Environment.NewLine.Length;
                    e.Handled = true; // Предотвращаем стандартное поведение
                }
                else
                {
                    // Обычный Enter → отправляем сообщение
                    OnSendMessageClick(sender, e);
                    e.Handled = true;
                }
            }
        }
    }
}
