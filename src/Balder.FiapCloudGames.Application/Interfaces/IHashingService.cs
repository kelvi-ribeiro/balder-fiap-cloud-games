﻿namespace Balder.FiapCloudGames.Application.Interfaces
{
    public interface IHashingService
    {
        string HashPassword(string password);
        bool VerifyPassword(string password, string hashedPassword);
    }
}