using System;
using System.Collections.Generic;
using System.Text;
using DonM.Crowe.Infrastructure.Models;


namespace DonM.Crowe.Application.Interfaces
{
    public interface IGreetingService
    {
        IEnumerable<Greeting> GetAll();
        void Save(Greeting greeting);
    }
}
