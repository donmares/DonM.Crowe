using System;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using DonM.Crowe.API.Controllers;
using DonM.Crowe.Application.Services;
using DonM.Crowe.Infrastructure.Repos;
using DonM.Crowe.Infrastructure.Models;

namespace DonM.Crowe.Test.Integration.API
{
    public class GreetingTests
    {
        private GreetingController InitController()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.test.json").Build();
            GreetingRepoFile repo = new GreetingRepoFile(config);
            GreetingService service = new GreetingService(config, repo);
            GreetingController controller = new GreetingController(service);

            return controller;
        }

        private ControllerContext getContext()
        {
            return new ControllerContext();
        }

        private static IEnumerable<Greeting> GetValue(ActionResult<IEnumerable<Greeting>> action)
        {
            if (action.Value != null)
                return action.Value;
            else
                return (IEnumerable<Greeting>)Convert.ChangeType((action.Result as ObjectResult)?.Value, typeof(IEnumerable<Greeting>));
        }


        [Fact]
        public void Get_Success()
        {
            GreetingController controller = InitController();

            ActionResult<IEnumerable<Greeting>> getResponse = controller.Get();

            OkObjectResult okResult = Assert.IsType<OkObjectResult>(getResponse.Result);

            IEnumerable<Greeting> getResult = Assert.IsAssignableFrom<IEnumerable<Greeting>>(okResult.Value);

            Assert.True(getResult.Count() > 0);
            Assert.Contains(getResult, g => g.Name == "Hello World");
        }

        [Fact]
        public void Post_Success()
        {
            GreetingController controller = InitController();

            Greeting greeting = new Greeting() { Name = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss") };
            var postResult = controller.Post(greeting);

            Assert.IsType<OkObjectResult>(postResult);
        }
    }
}
