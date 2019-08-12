using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.Configuration;
using DonM.Crowe.Application.Interfaces;
using DonM.Crowe.Infrastructure.Interfaces;
using DonM.Crowe.Infrastructure.Models;

namespace DonM.Crowe.Application.Services
{
    public class GreetingService : IGreetingService
    {
        private IConfiguration _config;
        private IGreetingRepo _repo;

        public GreetingService(IConfiguration config, IGreetingRepo repo)
        {
            _config = config;
            _repo = repo;
        }

        public IEnumerable<Greeting> GetAll()
        {
            List<Greeting> greetingList = new List<Greeting>();
            greetingList.Add(new Greeting() { Name = _config["AppSettings:DefaultGreeting"] });
            IEnumerable<Greeting> greetingRepo = _repo.GetAll();
            if (greetingRepo != null)
                greetingList.AddRange(greetingRepo);

            return greetingList;
        }

        public void Save(Greeting greeting)
        {
            IEnumerable<Greeting> greetingList = GetAll();

            if (greetingList != null && !greetingList.Any(g => g.Name == greeting.Name))
                _repo.Insert(greeting);
        }
    }
}
