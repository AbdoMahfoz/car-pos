using Models.DataModels;
using Services.DTOs;
using System;

namespace BusinessLogic.Interfaces
{
    public interface IAuth
    {
        User GenerateToken(int UserId);
        User Authenticate(UserAuthenticationRequest request);
        void Logout(int UserId);
        bool Validate(int UserId, DateTime TokenIssuedDate);
    }
}