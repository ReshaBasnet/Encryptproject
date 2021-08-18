using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace EncryptCLoud.Models
{
    public partial class AspNetUsers : IdentityUser
    {
        public AspNetUsers()
        {
            File = new HashSet<File>();
            Sharedfile = new HashSet<Sharedfile>();
            UserfriendFriend = new HashSet<Userfriend>();
            UserfriendUser = new HashSet<Userfriend>();
        }

        public virtual ICollection<File> File { get; set; }
        public virtual ICollection<Sharedfile> Sharedfile { get; set; }
        public virtual ICollection<Userfriend> UserfriendFriend { get; set; }
        public virtual ICollection<Userfriend> UserfriendUser { get; set; }
    }
}
