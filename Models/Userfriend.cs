using System;
using System.Collections.Generic;

namespace EncryptCLoud.Models
{
    public partial class Userfriend
    {
        public long Id { get; set; }
        public string UserId { get; set; }
        public string FriendId { get; set; }

        public virtual AspNetUsers Friend { get; set; }
        public virtual AspNetUsers User { get; set; }
    }
}
