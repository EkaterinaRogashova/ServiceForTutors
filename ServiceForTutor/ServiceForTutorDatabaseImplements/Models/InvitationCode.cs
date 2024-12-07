using ServiceForTutorContracts.BindingModels;
using ServiceForTutorContracts.ViewModels;
using ServiceForTutorDataModels.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ServiceForTutorDatabaseImplements.Models
{
    public class InvitationCode : IInvitationCodeModel
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public DateTime DateTimeEnd { get; set; }
        [Required]
        public string CodeValue { get; set; } = string.Empty;

        public int Id { get; set; }

        public User User { get; set; }

        public static InvitationCode? Create(InvitationCodeBindingModel model)
        {
            if (model == null)
            {
                return null;
            }
            return new InvitationCode()
            {
                Id = model.Id,
                UserId = model.UserId,
                DateTimeEnd = model.DateTimeEnd,
                CodeValue = model.CodeValue
            };
        }
        public static InvitationCode Create(InvitationCodeViewModel model)
        {
            return new InvitationCode
            {
                Id = model.Id,
                UserId = model.UserId,
                DateTimeEnd = model.DateTimeEnd,
                CodeValue = model.CodeValue
            };
        }

        public InvitationCodeViewModel GetViewModel => new()
        {
            Id = Id,
            UserId = UserId,
            DateTimeEnd = DateTimeEnd,
            CodeValue = CodeValue
        };
    }
}
