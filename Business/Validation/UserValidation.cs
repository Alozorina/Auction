﻿using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Validation
{
    public static class UserValidation
    {
        public static bool IsEmailExists(IEnumerable<User> users, string email)
            => users.Any(u => u.Email == email);

        public static bool IsEmailCouldBeUpdated(IEnumerable<User> users, string userEmail, string updateModelEmail)
        {
            if (userEmail != updateModelEmail && !IsEmailExists(users, updateModelEmail))
                return true;

            throw new AuctionException("Invalid email");
        }

        public static void ThrowArgumentExceptionIfUserIsNull(User user, string message = "Wrong Id")
        {
            if (user is null)
                throw new ArgumentException(message);
        }

        public static bool IsModelHasNullProperty(User model)
        {
            var isModelHasNullProperty = model == null
            || model.FirstName == null
            || model.LastName == null
            || model.Email == null
            || model.Password == null;

            if (isModelHasNullProperty)
                throw new AuctionException("One of the required properties is null");

            return isModelHasNullProperty;
        }
    }
}
