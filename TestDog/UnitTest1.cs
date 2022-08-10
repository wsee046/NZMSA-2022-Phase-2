using MyBackendProject.Controllers;
using MyBackendProject.Models;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;

namespace TestDog
{

    public class Tests
    {
        private DogController controller;
        private HttpClient client;
        private string[] queries;
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void TestGetRandomDog()
        {
            var dogs = new List<Dog>
            {
                new Dog {
                id = 123,
                name = "Chihuahua"
            },
                new Dog
                {
                    id = 124,
                    name = "Shiba inu"
                }
            };

            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            var url = "http://good.uri";
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(dogs), Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            httpClientFactoryMock.CreateClient().Returns(fakeHttpClient);

            // Act
            var service = new DogController(httpClientFactoryMock);
            var result = service.GetRandomDog();

            // Assert
            Assert.True(result != null);

        }

        [Test]
        public void TestGetVoteById()
        {
            
            
            var dogs = new List<TestVote>
            {
                new TestVote {
                image_id = "12345",
                value = 0,
                id = 123
            },
                new TestVote
                {
                     image_id = "123456",
                     value = 1,
                     id = 122
                }
            };

            var httpClientFactoryMock = Substitute.For<IHttpClientFactory>();
            var id = 123;
            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(dogs), Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);
            httpClientFactoryMock.CreateClient().Returns(fakeHttpClient);

            // Act
            var service = new DogController(httpClientFactoryMock);
            var res = service.GetVoteById(id);
            
            // Assert
            Assert.True(res != null);
            
        }
    }
}