using ServiceForTutor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceForTutorDataModels.Models
{
    public interface IUserModel: IId
    {
        string Email { get; set; }
        string Password { get; set; }
        string Surname { get; set; }
        string Name { get; set; }
        string? LastName { get; set; }
        string StatusActivity { get; set; }
        string Role { get; set; }
    }
}
