using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EncryptCLoud.Models
{
    public partial class File
    {
        public File()
        {
            Sharedfile = new HashSet<Sharedfile>();
        }

        [Key]
        public long Id { get; set; }
        public string Path { get; set; }
        public string UserId { get; set; }
        public byte[] UploadDate { get; set; }

        public virtual AspNetUsers User { get; set; }
        public virtual ICollection<Sharedfile> Sharedfile { get; set; }
        [NotMapped]
        public IFormFile uploads { get; set; }
    }
}
