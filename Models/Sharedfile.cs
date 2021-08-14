using System;
using System.Collections.Generic;

namespace EncryptCLoud.Models
{
    public partial class Sharedfile
    {
        public long Id { get; set; }
        public string FriendId { get; set; }
        public long ImageId { get; set; }

        public virtual AspNetUsers Friend { get; set; }
        public virtual File Image { get; set; }
    }
}
