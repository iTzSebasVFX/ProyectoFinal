using System;
using System.Web;
using Microsoft.AspNetCore.SignalR;
namespace SignalRChat
{
	public class ChatHub : Hub
	{

        public async Task SendMessage(int room, string user, string message)
		{
			//Enviamos un mensaje de forma asincronica a un grupo determinado
			await Clients.Group(room.ToString()).SendAsync("ReceiveMessage", user, message);
		}	

		public async Task AddGroup(string room)
		{
			//Añadimos a la persona a una sala
			await Groups.AddToGroupAsync(Context.ConnectionId, room);
		}
	}
}