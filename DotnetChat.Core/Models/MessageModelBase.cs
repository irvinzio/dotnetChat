using System;

namespace DotnetChat.Core.Models
{
    public abstract class MessageModelBase
    {
        public Guid Text { get; set; }
        public Guid UserId { get; set; }
    }
}
