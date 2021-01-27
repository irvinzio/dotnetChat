using System;

namespace DotnetChat.Core.Models
{
    public class MessageRequest : MessageModelBase
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
    }
    public class MessageResponse : MessageModelBase
    {
        public Guid MessageId { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserResponse User { get; set; }
}
}
