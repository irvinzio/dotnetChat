using System;
using System.Collections.Generic;
using System.Text;

namespace DotnetChat.Core.Models
{
    public abstract class UserModelBase
    {
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}
