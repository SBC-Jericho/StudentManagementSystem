//using BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientChatMessageService;
//using BlazorWasmDotNet8AspNetCoreHosted.Client.ClientService.ClientUserService;
//using BlazorWasmDotNet8AspNetCoreHosted.Shared.Models;
//using Microsoft.AspNetCore.Components;
//using Microsoft.AspNetCore.SignalR.Client;
//using Microsoft.JSInterop;

//namespace BlazorWasmDotNet8AspNetCoreHosted.Client.Pages.Chat
//{
//    public partial class Chat
//    {
//        private readonly IClientChatMessageService _clientChatMessageService;
//        private readonly IJSRuntime _jSRuntime;
//        private readonly NavigationManager _navigationManager;
//        private readonly IClientUserService _clientUserService;

//        public Chat()
//        {
            
//        }
//        public Chat(IClientChatMessageService ClientChatMessageService, IJSRuntime jSRuntime, NavigationManager NavigationManager, IClientUserService ClientUserService)
//        {
//            _clientChatMessageService = ClientChatMessageService;
//            _jSRuntime = jSRuntime;
//            _navigationManager = NavigationManager;
//            _clientUserService = ClientUserService;
//        }
//         public HubConnection hubConnection { get; set; }
//        [Parameter] public string CurrentMessage { get; set; }
//        [Parameter] public string CurrentUserId { get; set; }
//        [Parameter] public string CurrentUserEmail { get; set; }
//        private List<ChatMessage> messages = new List<ChatMessage>();
//         public async Task SubmitAsync()
//        {
//            if (!string.IsNullOrEmpty(CurrentMessage) && !string.IsNullOrEmpty(ContactId.ToString()))
//            {
//                //Save Message to DB
//                var chatHistory = new ChatMessage()
//                {
//                    Message = CurrentMessage,
//                    ToUserId = ContactId.ToString(),
//                    CreatedDate = DateTime.Now

//                };
//                await _clientChatMessageService.SaveMessage(chatHistory);
//                chatHistory.FromUserId = CurrentUserId;
//                await hubConnection.SendAsync("SendMessage", chatHistory, CurrentUserEmail);
//                CurrentMessage = string.Empty;
//            }
//        }
//        //protected override async Task OnAfterRenderAsync(bool firstRender)
//        //{
//        //    await _jSRuntime.InvokeAsync<string>("ScrollToBottom", "chatContainer");
//        //}
//        protected override async Task OnInitializedAsync()
//        {
            
//                hubConnection = new HubConnectionBuilder().WithUrl(_navigationManager.ToAbsoluteUri("/chathub")).Build();
           

//                hubConnection.On<ChatMessage, string>("ReceiveMessage", async (message, userName) =>
//            {
//                if ((ContactId.ToString() == message.ToUserId && CurrentUserId == message.FromUserId) || (ContactId.ToString() == message.FromUserId && CurrentUserId == message.ToUserId))
//                {

//                    if ((ContactId.ToString() == message.ToUserId && CurrentUserId == message.FromUserId))
//                    {
//                        messages.Add(new ChatMessage { Message = message.Message, CreatedDate = message.CreatedDate, Users = new User() { Email = CurrentUserEmail } });
//                        await hubConnection.SendAsync("ChatNotificationAsync", $"New Message From {userName}", ContactId, CurrentUserId);
//                    }
//                    else if ((ContactId.ToString() == message.FromUserId && CurrentUserId == message.ToUserId))
//                    {
//                        messages.Add(new ChatMessage { Message = message.Message, CreatedDate = message.CreatedDate, Users = new User() { Email = ContactEmail } });
//                    }
//                    //await _jSRuntime.InvokeAsync<string>("ScrollToBottom", "chatContainer");
//                }
//                StateHasChanged();
//            });
//            await hubConnection.StartAsync();
//            await GetUsersAsync();

//            CurrentUserId = await _clientUserService.GetSingleUserId();
//            CurrentUserEmail = await _clientUserService.GetSingleUserName();
//            if (!string.IsNullOrEmpty(ContactId.ToString()))
//            {
//                await LoadUserChat(ContactId);
//            }
//        }
//        public List<User> ChatUsers = new List<User>();

//        [Parameter] public string ContactEmail { get; set; }
//        [Parameter] public int ContactId { get; set; }
//            async Task LoadUserChat(int userId)
//            {
//                var contact = await _clientChatMessageService.GetSingleUser(userId);
//                ContactId = contact.Id;
//                ContactEmail = contact.Email;
//                _navigationManager.NavigateTo($"chat/{ContactId}");
//                messages = new List<ChatMessage>();
//                messages = await _clientChatMessageService.GetConversation(ContactId);
//            }
//            private async Task GetUsersAsync()
//            {
//                ChatUsers = await _clientChatMessageService.GetAllUsers();
//            }
//    }
//}
