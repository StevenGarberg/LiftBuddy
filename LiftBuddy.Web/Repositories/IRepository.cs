using System.Collections.Generic;
using LiftBuddy.Models;

namespace LiftBuddy.Web.Repositories
{
    public interface IRepository<T>
    {
        IList<Workout> GetAll();
    }
}