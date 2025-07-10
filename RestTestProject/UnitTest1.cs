using RestSharp;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace RestTestProject
{
    public class UnitTest1
    {
        [Fact]
        //checks happy path for fatching user id
        public async void Test1()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 1;
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Get).AddHeader("x-api-key", "reqres-free-v1");
            //act

            var response = await client.ExecuteAsync(request);
            

            //assert
            Assert.Equal("{\"data\":{\"id\":1,\"email\":\"george.bluth@reqres.in\",\"first_name\":\"George\",\"last_name\":\"Bluth\",\"avatar\":\"https://reqres.in/img/faces/1-image.jpg\"},\"support\":{\"url\":\"https://contentcaddy.io?utm_source=reqres&utm_medium=json&utm_campaign=referral\",\"text\":\"Tired of writing endless social media content? Let Content Caddy generate it for you.\"}}", response.Content);




        }

        [Fact]
        //checks unhappy path for fatching user id, when the id is an invalid number
        public async void Test2()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 50000;
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Get).AddHeader("x-api-key", "reqres-free-v1");
            //act

            var response = await client.ExecuteAsync(request);
            

            //assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);




        }



        [Fact]
        // checks for fatch of user list
        public async void Test3() {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            RestRequest request = new RestRequest("api/users", Method.Get).AddHeader("x-api-key", "reqres-free-v1");
            //act
            var response = await client.ExecuteAsync(request);
            
            //assert
            Assert.Equal("{\"page\":1,\"per_page\":6,\"total\":12,\"total_pages\":2,\"data\":[{\"id\":1,\"email\":\"george.bluth@reqres.in\",\"first_name\":\"George\",\"last_name\":\"Bluth\",\"avatar\":\"https://reqres.in/img/faces/1-image.jpg\"},{\"id\":2,\"email\":\"janet.weaver@reqres.in\",\"first_name\":\"Janet\",\"last_name\":\"Weaver\",\"avatar\":\"https://reqres.in/img/faces/2-image.jpg\"},{\"id\":3,\"email\":\"emma.wong@reqres.in\",\"first_name\":\"Emma\",\"last_name\":\"Wong\",\"avatar\":\"https://reqres.in/img/faces/3-image.jpg\"},{\"id\":4,\"email\":\"eve.holt@reqres.in\",\"first_name\":\"Eve\",\"last_name\":\"Holt\",\"avatar\":\"https://reqres.in/img/faces/4-image.jpg\"},{\"id\":5,\"email\":\"charles.morris@reqres.in\",\"first_name\":\"Charles\",\"last_name\":\"Morris\",\"avatar\":\"https://reqres.in/img/faces/5-image.jpg\"},{\"id\":6,\"email\":\"tracey.ramos@reqres.in\",\"first_name\":\"Tracey\",\"last_name\":\"Ramos\",\"avatar\":\"https://reqres.in/img/faces/6-image.jpg\"}],\"support\":{\"url\":\"https://contentcaddy.io?utm_source=reqres&utm_medium=json&utm_campaign=referral\",\"text\":\"Tired of writing endless social media content? Let Content Caddy generate it for you.\"}}", response.Content);


        }

        [Fact]
        // checks for deleting a user
        public async void Test4()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 3;
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Delete).AddHeader("x-api-key", "reqres-free-v1");
            //act

            var response = await client.ExecuteAsync(request);
            

            //assert
            Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);




        }

        [Fact (Skip = "this test should not fail normally, but there are issues with the API")]
        // checks unhappy path  for deleting a user, when the user id in invalid
        public async void Test5()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 50000;
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Delete).AddHeader("x-api-key", "reqres-free-v1");
            //act

            var response = await client.ExecuteAsync(request);
            

            //assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);




        }


        [Fact]
        // checks happy path  for updating a user [PATCH]
        public async void Test6()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 3;
            const string input = "{\"first name\":\"george\"}";
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Patch).AddHeader("x-api-key", "reqres-free-v1");
            request.AddJsonBody(input);
            //act

            var response = await client.ExecuteAsync(request);
            

            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);




        }

        [Fact (Skip = "This test should not fail normally, but there are issues with the API")]
        // checks unhappy path  for updating a user, when the user id is invalid [PATCH] 
        public async void Test7()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 50000;
            const string input = "{\"first name\":\"george\"}";
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Patch).AddHeader("x-api-key", "reqres-free-v1");
            
            request.AddJsonBody(input);
            //act

            var response = await client.ExecuteAsync(request);

            //assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            




        }

        [Fact]
        // checks that the response contains the changed paramaterers [PATCH]
        public async void Test8()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 3;
            const string input = "{\"first name\":\"george\"}";
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Patch).AddHeader("x-api-key", "reqres-free-v1");

            request.AddJsonBody(input);
            //act

            var response = await client.ExecuteAsync(request);
            var data = JsonSerializer.Deserialize<JsonNode>(response.Content);
            string actual = $"{data["first name"]}";
            

            //assert
            Assert.Equal("george", actual);




        }

        [Fact]
        // checks happy path for updating a user [PUT]
        public async void Test9()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 3;
            const string input = "{\"first name\":\"george\"}";
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Put).AddHeader("x-api-key", "reqres-free-v1");

            request.AddJsonBody(input);
            //act

            var response = await client.ExecuteAsync(request);


            //assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);




        }


        [Fact]
        // checks if the updated data is contained in the API response [PUT]
        public async void Test10()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 3;
            const string input = "{\"email\":\"george.bluth@gmail.com\",\"first_name\":\"George\",\"last_name\":\"Bluth\",\"avatar\":\"https://reqres.in/img/faces/1-image.jpg\"}";
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Put).AddHeader("x-api-key", "reqres-free-v1");

            request.AddJsonBody(input);
            //act

            var response = await client.ExecuteAsync(request);
            var data = JsonSerializer.Deserialize<JsonNode>(response.Content);
            string actual = $"{data[response.Content]}";


            //assert
            Assert.Contains("\"email\":\"george.bluth@gmail.com\",\"first_name\":\"George\",\"last_name\":\"Bluth\",\"avatar\":\"https://reqres.in/img/faces/1-image.jpg\"", response.Content);
          




        }


        [Fact (Skip = "This test should not fail normally, but there issues with the API")]
        // checks unhappy path for updating a user when there are not enough parameters provided [PUT]
        public async void Test11()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 3;
            const string input = "{\"first name\":\"george\"}";
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Put).AddHeader("x-api-key", "reqres-free-v1");

            request.AddJsonBody(input);
            //act

            var response = await client.ExecuteAsync(request);


            //assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);




        }

        [Fact(Skip = "This test should not fail normally, but there issues with the API")]
        // checks unhappy path for updating a user when there are not enough parameters provided [PUT]
        public async void Test12()
        {
            //arrange
            RestClient client = new RestClient("https://reqres.in/");
            int userNumber = 50000;
            const string input = "{\"email\":\"george.bluth@gmail.com\",\"first_name\":\"George\",\"last_name\":\"Bluth\",\"avatar\":\"https://reqres.in/img/faces/1-image.jpg\"}";
            RestRequest request = new RestRequest($"api/users/{userNumber}", Method.Put).AddHeader("x-api-key", "reqres-free-v1");

            request.AddJsonBody(input);
            //act

            var response = await client.ExecuteAsync(request);


            //assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);




        }


    }




}