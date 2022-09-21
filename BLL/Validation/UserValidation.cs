﻿using DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using BC = BCrypt.Net.BCrypt;

namespace BLL.Validation
{
    public static class UserValidation
    {
        public static bool IsEmailExists(IEnumerable<User> users, string email)
            => users.Any(u => u.Email == email);

        public static bool IsEmailCouldBeUpdated(IEnumerable<User> users, string userEmail, string updateModelEmail)
            => userEmail != updateModelEmail && IsEmailExists(users, updateModelEmail);

        public static bool IsClientPasswordMatches(string textPasswordFromClient, string currentHashPasswordInDb)
            => BC.Verify(textPasswordFromClient, currentHashPasswordInDb);

        public static bool IsModelHasNullProperty(User model)
            => model == null
            || model.FirstName == null
            || model.LastName == null
            || model.Email == null
            || model.Password == null;
    }
}
