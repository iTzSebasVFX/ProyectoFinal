using System;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProyectoFinal.Models;
namespace SignalRChat
{
	public class ChatHub : Hub
	{

		private readonly ApplicationDbContext _context;
		public ChatHub(ApplicationDbContext context)
		{
			_context = context;
		}
		public async Task SendMessage(Int128 room, string user, string message)
		{
			Console.WriteLine("Andamos Aqui");
			if (string.IsNullOrWhiteSpace(message)) return;

			// 💾 Guardar en BD
			var chatMessage = new ChatModel
			{
				room = room.ToString(),
				user = user,
				message = message,
				CreatedAt = DateTime.UtcNow
			};

			_context.Mensajes.Add(chatMessage);
			await _context.SaveChangesAsync();

			// 📢 Enviar a todos en el grupo (menos el que lo envió)
			await Clients.Group(room.ToString()).SendAsync("ReceiveMessage", user, message, chatMessage.CreatedAt);
		}

		public async Task AddGroup(string room, string idUser)
		{
			if (idUser == null)
			{
				await Clients.Caller.SendAsync("AccessDenied", "No autenticado");
				return;
			}
			var acceso = _context.UsuariosRoom.Any(ur => ur.IdRoom == int.Parse(room) && ur.IdUsuario == int.Parse(idUser));

			if (!acceso)
			{
				await Clients.Caller.SendAsync("AccessDenied", "No tienes acceso a este chat");
				return;
			}
			else
			{
				//Añadimos a la persona a una sala
				await Groups.AddToGroupAsync(Context.ConnectionId, room);

				// 🔁 Obtener historial de mensajes
				var mensajes = await _context.Mensajes
					.Where(m => m.room == room)
					.OrderBy(m => m.CreatedAt)
					.ToListAsync();

				// 🔁 Enviar historial solo al usuario que se conectó
				await Clients.Caller.SendAsync("ReceiveMessageHistory", mensajes);
			}
		}
	}
}