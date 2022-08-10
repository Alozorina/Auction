using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Auction.Tests
{
    public class UserControllerTests
    {
        private CustomWebApplicationFactory _factory;
        private HttpClient _client;
        private const string RequestUri = "api/user/";

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}