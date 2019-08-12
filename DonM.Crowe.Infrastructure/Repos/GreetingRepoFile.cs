using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Extensions.Configuration;
using DonM.Crowe.Infrastructure.Interfaces;
using DonM.Crowe.Infrastructure.Models;


namespace DonM.Crowe.Infrastructure.Repos
{
    public class GreetingRepoFile : IGreetingRepo
    {
        private string _fileName = "";

        public GreetingRepoFile(IConfiguration config)
        {
            _fileName = config["AppSettings:DefaultGreetingFile"];
        }

        IEnumerable<Greeting> IGreetingRepo.GetAll()
        {
            if (!File.Exists(_fileName))
                return null;

            List<Greeting> greetingList = new List<Greeting>();

            using (StreamReader sr = new StreamReader(_fileName))
            {
                while (sr.Peek() >= 0)
                {
                    string line = sr.ReadLine();
                    greetingList.Add(new Greeting() { Name = line });
                }
            }

            return greetingList;
        }

        public void Insert(Greeting greeting)
        {
            using (StreamWriter outFile = new StreamWriter(_fileName, true))
            {
                outFile.WriteLine(greeting.Name);
            }
        }
    }
}
