using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetChat.Data.Entities
{
    public class Message
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid MessageId { get; set; }
        public Guid Text { get; set; }
        public User UserId { get; set; }
    }
}
