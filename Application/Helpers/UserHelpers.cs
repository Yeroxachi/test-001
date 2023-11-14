﻿using System.Security.Cryptography;
using System.Text;

namespace Application.Helpers;

public static class UserHelpers
{
    public static string Hash(string rawData)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(rawData));
        var builder = new StringBuilder();
        foreach (var t in bytes)
        {
            builder.Append(t.ToString("x2"));
        }

        return builder.ToString();

    }
}