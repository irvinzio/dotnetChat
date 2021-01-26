using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetChat.Core.Models
{
    public class UserRequest : UserModelBase{    }
    public class UserResponse : UserModelBase
    {
        public Guid UserId { get; set; }
    }
}
