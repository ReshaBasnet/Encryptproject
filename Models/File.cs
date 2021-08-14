using System;
using System.Collections.Generic;

namespace EncryptCLoud.Models
{
    public partial class File
    {
        public File()
        {
            Sharedfile = new HashSet<Sharedfile>();
        }

        public long Id { get; set; }
        public string Path { get; set; }
        public string UserId { get; set; }
        public byte[] UploadDate { get; set; }

        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<Sharedfile> Sharedfile { get; set; }
    }
}
