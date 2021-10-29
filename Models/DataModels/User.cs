using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.DataModels
{
    public class User : BaseModel
    {
        #region Auth
        [Required] public string UserName { get; set; }
        [Required] public string Password { get; set; }
        public bool LoggedIn { get; set; }
        public DateTime? LastLogOut { get; set; }
        [NotMapped] public string Token { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        #endregion
        
        [Required]
        public string FullName { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Required]
        public string CompanyName { get; set; }
        [Required]
        public string CompanyAddress { get; set; }
    }
}