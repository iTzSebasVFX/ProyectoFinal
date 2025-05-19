using System.ComponentModel.DataAnnotations;
using Mysqlx;

namespace ProyectoFinal.Models
{
    public class ChatModel
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string? room { get; set; }

        [Required]
        public string? user { get; set; }

        [Required]
        public string? message { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    };
}