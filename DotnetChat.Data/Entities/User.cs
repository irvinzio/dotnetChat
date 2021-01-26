using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DotnetChat.Data.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid UserId { get; set; }
        [Required]
        [StringLength(100)]
        public string Email { get; set; }
        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        public IEnumerable<Message> Messages { get; set; }
    }
}
