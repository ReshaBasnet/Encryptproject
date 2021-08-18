using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EncryptCLoud.Models
{
    public partial class Sharedfile
    {
        [Key]
        public long Id { get; set; }
        public string FriendId { get; set; }
        public long ImageId { get; set; }

        public virtual AspNetUsers Friend { get; set; }
        public virtual File Image { get; set; }
    }
}
