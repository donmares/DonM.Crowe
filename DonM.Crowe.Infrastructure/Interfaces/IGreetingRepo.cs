using System;
using System.Collections.Generic;
using System.Text;
using DonM.Crowe.Infrastructure.Models;

namespace DonM.Crowe.Infrastructure.Interfaces
{
    public interface IGreetingRepo
    {
        IEnumerable<Greeting> GetAll();

        void Insert(Greeting greeting);
    }
}
