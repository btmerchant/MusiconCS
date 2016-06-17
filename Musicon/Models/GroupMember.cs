using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Musicon.Models
{
    public class GroupMember
    {
        public virtual int GroupMemberId { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; }
        [Required]
        public virtual Group Group { get; set; }
    }
}