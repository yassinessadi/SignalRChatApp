using Microsoft.AspNetCore.SignalR.Client;

namespace ChatApp
{
    public partial class MainPage : ContentPage
    {
        HubConnection Connection;
        public MainPage()
        {
            InitializeComponent();
            Connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7287/chat")
                .WithAutomaticReconnect()
                .Build();
            Connection.On<string, string>("ReciveMessage", (user, message) =>
            {
                var data = $"{user}:{message}";
                Messages.Text += $"{data}";
            });
            Task.Run(() =>
            {
                Dispatcher.DispatchAsync(async () =>
                {
                    await Connection.StartAsync();
                });
            });
        }

        private async void sendMessage_Clicked(object sender, EventArgs e)
        {
            //await Connection.InvokeCoreAsync("SendMessage", args: new string[] { UsernameInput.Text, Messages.Text });

            await Connection.SendAsync("SendMessage", UsernameInput.Text, MessageInput.Text);
            MessageInput.Text = String.Empty;
        }
    }
}