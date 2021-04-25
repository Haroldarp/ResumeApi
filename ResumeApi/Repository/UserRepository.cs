using ResumeApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResumeApi.Repository
{
    public class UserRepository
    {
        public static List<User> Users = new List<User>() { new User("Admin", "12345")};

    }
}
