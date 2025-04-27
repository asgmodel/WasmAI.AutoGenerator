using AutoGenerator.Repositories.Base;
using LAHJAAPI.Data;
using LAHJAAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;

namespace ApiCore.Repositories.Base
{
    /// <summary>
    /// BaseRepository class for ShareRepository.
    /// </summary>
    public sealed class BaseRepository<T> : TBaseRepository<ApplicationUser, IdentityRole, string, T>, IBaseRepository<T> where T : class
    {
        public BaseRepository(DataContext db, ILogger logger) : base(db, logger)
        {
        }
    }
}