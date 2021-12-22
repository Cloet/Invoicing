using System;
using System.Collections.Generic;
using System.Text;
using Invoicing.Domain.Model.Common;

namespace Invoicing.Domain.Model
{
    public class User : SQLModelBase<User>
    {

        public string Username { get; set; }

        public string HashedPassword { get; set; }

        public static User RegisterUser(string username, string hashedPassword)
        {
            var user = new User()
            {
                Username = username,
                HashedPassword = hashedPassword
            };

            return user;
        }

    }
}
