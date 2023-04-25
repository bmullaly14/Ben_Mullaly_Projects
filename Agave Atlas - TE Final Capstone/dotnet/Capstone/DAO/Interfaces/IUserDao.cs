﻿using Capstone.Models;

namespace Capstone.DAO.Interfaces
{
    public interface IUserDao
    {
        User GetUser(string username);
        User AddUser(string username, string password, string role);
        User GetUserByID(int userID);
    }
}
